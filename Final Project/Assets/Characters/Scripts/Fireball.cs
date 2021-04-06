using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb;

    public void Start()
    {
        rb.velocity = transform.right * speed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground" || collision.gameObject.tag == "sword" || collision.gameObject.tag == "health" || collision.gameObject.tag == "wall")
        {
            Destroy(gameObject);
        }
        
    }
}