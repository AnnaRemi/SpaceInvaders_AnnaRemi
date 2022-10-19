using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    private GameManager gameManager;

    public Projectile lazerPrefab;
      
    public float speed = 7.0f;

    private bool lazerActive;

    private void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.position += Vector3.left * this.speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.position += Vector3.right * this.speed * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            ShootLazer(); 
        }
    }

    private void ShootLazer()
    {
        if (!lazerActive)
        {
            Projectile projectile = Instantiate(this.lazerPrefab, this.transform.position, Quaternion.identity);
            projectile.destroyed += LazerDestroyed;
            lazerActive = true;
        } 
    }

    private void LazerDestroyed() //we call this when destroyed is invoked
    {
        lazerActive = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Missile"))
        {
            gameManager.UpdateLives(1);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Invader"))
        {
            gameManager.KillPlayer();
        }
    }
}
