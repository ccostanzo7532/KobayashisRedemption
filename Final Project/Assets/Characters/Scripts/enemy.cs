using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
   
    private IEnemyState currentState;
    public GameObject Target { get; set; }
    private Rigidbody2D erb;
    private bool movingR;
    public Transform kunaiSpot;
    public GameObject kunai_pf;
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

    public override bool isDead
    {
        get
        {
            return enemy_hp <= 0;
        }
    }

    public Transform startPos;
    public Transform endPos;
    
   
    

    // Start is called before the first frame update
    public override void  Start()
    {
        base.Start();
        ChangeEnemyState(new IdleState());
        erb = this.GetComponent<Rigidbody2D>();
        enemy_hp = health;
        Health.maxHP(enemy_hp);
       
        Player.Instance.Died += new PlayerDeadEvent(StopAttacking);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            if (!Damaged)
            {
                currentState.Execute();
            }
           
            LookAtPlayer();
        }
        
       
       
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

    public void ThrowKunai()
    {
        GameObject Kunai = Instantiate(kunai_pf, kunaiSpot.position, kunaiSpot.rotation);
    }
   public void StopAttacking()
    {
        Target = null;
        ChangeEnemyState(new PatrolState());

    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        currentState.OnTriggerEnter(other);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "projectile")
        {
            Destroy(collision.gameObject);
           
        }
    }
    
    
   

    public override IEnumerator TakeDamage()
    {
        if(damageTag.Contains("player_Sword"))
        {
            enemy_hp -= 50;
            damageTag.Remove("player_Sword");
          
        }
       else if (damageTag.Contains("projectile"))
        {
            enemy_hp -= 30;
            damageTag.Remove("projectile");
            Debug.Log("fire");
           
        }
        else if (damageTag.Contains("player_SpecialSword"))
        {
            enemy_hp -= 100;
            damageTag.Remove("player_SpecialSword");
        }

        
        
        Health.setHP(enemy_hp);


        if (!isDead)
        {
            MyAnim.SetTrigger("Damage");
         
        }
        else
        {
            MyAnim.SetTrigger("Die");
           
            yield return null;
        }
    }

    public override void Die()
    {
        Destroy(gameObject);
    }
}
