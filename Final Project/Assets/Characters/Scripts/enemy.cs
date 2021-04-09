using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    private IEnemyState currentState;
    public GameObject Target { get; set; }
    private Rigidbody2D erb;
    private bool movingR;
    public int enemyHealth = 100;
    public int enemy_hp;
    new float speed = 2f;
    [SerializeField] private float AttackRange = 1f;
    public bool InAttackRange
    {
        get
        {
            if(Target != null)
            {
                return Vector2.Distance(transform.position, Target.transform.position) <= AttackRange;
            }
            return false;
        }
    }


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
        LookAtPlayer();
       
       
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
        if (!Attacking)
        {
            MyAnim.SetFloat("speed", Mathf.Abs(speed));

            Debug.Log("moving");


            if (movingR)
            {
                erb.velocity += Vector2.right * speed;

            }
            else if (!movingR)
            {
                erb.velocity = Vector2.left * speed ;

            }
         
             

            if (transform.position.x >= endPos.position.x && lookingRight )
            {
                movingR = false;
                FlipCharacter();
                
            }
            else if (transform.position.x <= startPos.position.x && !lookingRight)
            {
                movingR = true;
                FlipCharacter();
                
            }

            if (erb.velocity.magnitude > speed)
            {
                erb.velocity = erb.velocity.normalized * speed;
            }
            

        }

    }

   

    private void LookAtPlayer()
    {
       
        if(Target != null) 
        {
            float xDir = Target.transform.position.x - transform.position.x;

            if(xDir < 0 && lookingRight)
            {
                movingR = false;
                FlipCharacter();
            }
            else if(xDir > 0 && !lookingRight)
            {
                movingR = true;
                FlipCharacter();
            }
        }
       
    }
    
   

    private void OnTriggerEnter2D(Collider2D other)
    {
        currentState.OnTriggerEnter(other);
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
