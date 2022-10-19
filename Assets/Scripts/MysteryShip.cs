using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryShip : MonoBehaviour
{
    private GameManager gameManager;

    private Vector3 direction = Vector2.right;
    public float speed = 11.0f;
    public float disappearenceFreq = 30.0f;

    
    void Start()
    {
        InvokeRepeating(nameof(DisappearanceLogic), this.disappearenceFreq, this.disappearenceFreq);
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void DisappearanceLogic()
    {
        if (this.gameObject.activeSelf)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Lazer"))
        {
            gameManager.UpdateScore(400);
            this.gameObject.SetActive(false);
        }
    }


    void Update()
    {
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);


        if (this.gameObject.activeInHierarchy)
        {
            this.transform.position += direction * this.speed * Time.deltaTime;

            if ((direction == Vector3.right) && (this.transform.position.x >= (rightEdge.x - 1.0f)))
            {
                direction.x *= -1.0f;
            }
            else if (direction == Vector3.left && this.transform.position.x <= (leftEdge.x + 1.0f))
            {
                direction.x *= -1.0f;
            }
        }
    }
}
