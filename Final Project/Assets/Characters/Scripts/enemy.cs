using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    private Rigidbody2D erb;
    private bool movingR;
    public int enemyHealth = 100;
    public int enemy_hp;
    new float speed = 2f;
    Vector2 movement;


    public Transform startPos;
    public Transform endPos;
    public Transform Player;
   
    public float playerRange = 0.5f;

    // Start is called before the first frame update
    public override void  Start()
    {
        erb = this.GetComponent<Rigidbody2D>();
        enemy_hp = enemyHealth;
        Health.maxHP(enemyHealth);
    }

    // Update is called once per frame
    void Update()
    {


        Debug.Log(erb.velocity);
       
    }
    private void FixedUpdate()
    {
        
        Movement();
        
    }

    public void Movement()
    {
        
        
        if ( movingR )
        {
            erb.velocity += new Vector2(speed,erb.velocity.y);
            //FlipCharacter();
            
        }
        else if ( !movingR )
        {
            erb.velocity -= new Vector2(speed,erb.velocity.y);
            //FlipCharacter();
           
        }

        if(erb.position.x >= endPos.position.x && lookingRight)
        {
            movingR = false;
            
            FlipCharacter();
        }
        else if(erb.position.x <= startPos.position.x && !lookingRight)
        {
            movingR = true;
            
            FlipCharacter();
        }

        if(erb.velocity.magnitude > speed)
        {
            erb.velocity = erb.velocity.normalized * speed;
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "projectile")
        {
            Destroy(collision.gameObject);
            fireballDmg();
        }
    }
    public void TakeDamage(int damage)
    {
        enemy_hp -= damage;
        Health.setHP(enemy_hp);
        if (enemy_hp <= 0)
        {
            Die();
        }

    }
    public void fireballDmg()
    {
        TakeDamage(30);
    }
    void Die()
    {
        
        Destroy(gameObject);
    }


}
