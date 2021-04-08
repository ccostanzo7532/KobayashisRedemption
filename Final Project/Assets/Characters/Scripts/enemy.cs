using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    private IEnemyState currentState;
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
        base.Start();
        ChangeEnemyState(new IdleState());
        erb = this.GetComponent<Rigidbody2D>();
        enemy_hp = enemyHealth;
        Health.maxHP(enemyHealth);
    }

    // Update is called once per frame
    void Update()
    {

        currentState.Execute();
       
       
    }
    private void FixedUpdate()
    {
       
       
        
    }

    public void ChangeEnemyState(IEnemyState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }
        currentState = newState;

        currentState.Enter(this);
    }

    public void Movement()
    {
        MyAnim.SetFloat("speed", Mathf.Abs(speed));
        
        if ( movingR )
        {
            erb.velocity += new Vector2(speed,erb.velocity.y);
            
            
        }
        else if ( !movingR )
        {
            erb.velocity -= new Vector2(speed,erb.velocity.y);
           
           
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
