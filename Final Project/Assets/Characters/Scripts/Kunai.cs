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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "ground" || other.gameObject.tag == "sword" || other.gameObject.tag == "health" || other.gameObject.tag == "wall")
        {
            Destroy(gameObject);
        }

    }
}
