using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 5f;
    private Vector2 Dir;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
       
    }
    private void FixedUpdate()
    {
        rb.velocity = Dir * speed;
    }
    public void Spawn(Vector2 Dir)
    {
        this.Dir = Dir;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground" || collision.gameObject.tag == "sword" || collision.gameObject.tag == "health" || collision.gameObject.tag == "wall")
        {
            Destroy(gameObject);
        }

    }
}
