using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvadersGrid : MonoBehaviour
{
    private GameManager gameManager;

    public int rows = 5;
    public int columns = 11;
    public Invader[] prefabs;
    public AnimationCurve speed; //float -> animation curve because speed changes due to amt of invaders left

    public int amtKilled { get; private set; }
    public int totalInvaders => this.rows * this.columns;
    public float percentKilled => (float)this.amtKilled / (float)this.totalInvaders;
    public int amtAlive => this.totalInvaders - this.amtKilled;

    public float missileAttackFreq = 1.0f;
    public Projectile missilePrefab; 

    private Vector3 direction = Vector2.right;

    public void Awake()
    {
        for (int row = 0; row < this.rows; row++)
        {
            float width = 2.0f * (this.columns - 1);
            float height = 2.0f * (this.rows - 1);

            Vector3 offset = new Vector2(-width / 2, -height / 2);
            Vector3 rowPosition = new Vector3(offset.x, offset.y + row * 2.0f , 0.0f);

            for (int col = 0; col < this.columns; col++)
            {  
                Invader invader = Instantiate(this.prefabs[row], this.transform);
                invader.killed += InvaderKilled;
                Vector3 position = rowPosition;
                position.x += col * 2.0f; //2.0f is space between invaders
                invader.transform.localPosition = position;
            }
        } 
    }


    public void Start()
    {
        InvokeRepeating(nameof(MissileAttack), this.missileAttackFreq, this.missileAttackFreq);
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    private void MissileAttack()
    {
        foreach (Transform invader in this.transform)
        {
            if (!invader.gameObject.activeInHierarchy)
            {
                continue;
            }
            //when killing invaders the chance of shooting is higher
            //if all are alive the chance of shooting is 1 of 55
            if (Random.value < (1.0f / (float)this.amtAlive)) 
            {
                Instantiate(this.missilePrefab, invader.position, Quaternion.identity);
                break;//guantees that only one missile will be shot at once
            }
        }
    }


    void Update()
    {
        this.transform.position += direction * this.speed.Evaluate(this.percentKilled) * Time.deltaTime;

        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        foreach (Transform invader in this.transform)
        {
            if (!invader.gameObject.activeInHierarchy)
            {
                continue;
            }
            if ((direction == Vector3.right) && (invader.position.x >= (rightEdge.x - 1.0f)))
            {
                AdvanceRow();
            }
            else if (direction == Vector3.left && invader.position.x <= (leftEdge.x + 1.0f))
            {
                AdvanceRow();
            }
        }
    }

    private void AdvanceRow()
    {
        direction.x *= -1.0f;
        Vector3 position = this.transform.position;
        position.y -= 1.0f;
        this.transform.position = position; 
    }

    private void InvaderKilled()
    {
        this.amtKilled++;
        if (this.amtAlive <= 0)
        {
            gameManager.PlayerWon();
        }
    }
}
