using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public Vector3 direction;
    public System.Action destroyed; //event
    
    // Update is called once per frame
    private void Update()
    {
        this.transform.position += this.direction * this.speed * Time.deltaTime; 
    }

    private void OnTriggerEnter2D(Collider2D other) //auto called when collided
    {
        if (this.destroyed != null)
        {
            this.destroyed.Invoke(); //is used to inform player that we hit something and can shoot again
        }
        
        Destroy(this.gameObject);
    }
}
