using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private Rigidbody2D myrb;
   
    public Transform playerStartPos;

    public float jumpHeight = 10f;
    new float speed = 3.5f;
    private BoxCollider2D box2D;
    private bool DoubleJump = false;
    public LayerMask platformLayer;
    

    public Transform fireballSpot;
    public GameObject fireball;
    bool canUseFireball;

    public int health = 100;
    public int player_hp;
   

    public GameObject fb_pf;
    public GameObject inventory;
    public GameObject fb;
    public GameObject sword;
    public GameObject sword_pf;
    bool canUseSword;
    public GameObject hpItem;
    public GameObject hpItem_pf;
    bool canUseHP;

    public Transform attackArea;
    public float attackRange = 0.5f;
    public LayerMask enemy_layer;

    
    


    // Start is called before the first frame update
   public override void Start()
    {
        myrb = this.GetComponent<Rigidbody2D>();
        box2D = this.GetComponent<BoxCollider2D>();
        base.Start();
     
        player_hp = health;

        Health.maxHP(health);

    }

    private void FixedUpdate()
    {
        float run = Input.GetAxisRaw("Horizontal");
        Movement();
        UseHealthItem();
        myAnim.SetFloat("speed", Mathf.Abs(run));
        myAnim.SetFloat("vSpeed", myrb.velocity.y);
       

        if (run < 0 && !lookingRight)
        {


            FlipCharacter();

        }
        else if (run > 0 && lookingRight)
        {

            FlipCharacter();


        }



        if (Input.GetAxisRaw("Jump") > 0 && OnGround())

        {

            myrb.velocity = Vector2.up * jumpHeight;
            DoubleJump = true;


        }
        if (Input.GetKeyDown(KeyCode.Space) && DoubleJump && !OnGround())
        {

            myrb.velocity = Vector2.up * jumpHeight;
            DoubleJump = false;

        }

    }

    // Update is called once per frame
    void Update()
    {
        myAnim.SetBool("Jumping", OnGround());


        if (player_hp <= 0)
        {
            Respawn();
        }



         if (Input.GetMouseButtonDown(0))
        {

            swordAttack();
            myrb.velocity = Vector2.zero;
            

        }
        

        if (Input.GetKeyDown(KeyCode.F) && canUseFireball)
        {
            myAnim.SetTrigger("Fireball");
            GameObject fire = Instantiate(fireball, fireballSpot.position, fireballSpot.rotation);
           
            destroyFireball(fb);
            canUseFireball = false;



        }

        else if(Input.GetMouseButton(1) && canUseSword)
        {
            heavyAttack();
            destroySword(sword);
            canUseSword = false;
        }

       
        
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
       
         if(collision.gameObject.tag == "enemy")
        {
            TakeDamge(20);
           
        }
        else if (collision.gameObject.tag == "heavy")
        {
            TakeDamge(35);

        }
        else if (collision.gameObject.tag == "fb")
        {
            Destroy(collision.gameObject);
            addFireball();
         
        }
        else if(collision.gameObject.tag == "sword")
        {
            Destroy(collision.gameObject);
            addSword();
        }
        else if (collision.gameObject.tag == "health")
        {
            Destroy(collision.gameObject);
            addHpItem();
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "enemy" || collision.gameObject.tag == "heavy")
        {
            myAnim.SetBool("TakingDamage", false);
        }
    }
    
    public void Movement()
    {
        float run = Input.GetAxisRaw("Horizontal");

        myrb.velocity = new Vector2(run ,0)*speed + new Vector2(0,myrb.velocity.y);
      
    }
    

    public bool OnGround()
    {
        float extraH = 0.4f;
        RaycastHit2D hitinfo = Physics2D.BoxCast(box2D.bounds.center, box2D.bounds.size, 0f, Vector2.down, extraH, platformLayer);
        return hitinfo.collider != null;
        
    }
    
    public void TakeDamge(int damage)
    {
        player_hp -= damage;
        Health.setHP(player_hp);
        myAnim.SetBool("TakingDamage", true);
        
    }
    public void UseHealthItem()
    {
        if(player_hp < 100 && canUseHP && Input.GetKeyDown(KeyCode.LeftShift))
        {
            destroyHpItem(hpItem);
            player_hp += 45;
            Health.setHP(player_hp);
            canUseHP = false;
        }
    }
    public void Respawn()
    {

        this.transform.position = playerStartPos.position;
        myrb.velocity = Vector2.zero;
        if (!lookingRight)
        {
            FlipCharacter();
        }
        
        player_hp = health;
        Health.setHP(health);

      
    }
    public void addFireball()
    {
        fb = Instantiate(fb_pf);
        fb.transform.SetParent(inventory.transform);
        canUseFireball = true;


    }
    public void addSword()
    {
        sword = Instantiate(sword_pf);
        sword.transform.SetParent(inventory.transform);
        canUseSword = true;
    }
    public void addHpItem()
    {
        hpItem = Instantiate(hpItem_pf);
        hpItem.transform.SetParent(inventory.transform);
        canUseHP = true;
    }

    public void destroyFireball(GameObject fb)
    {
        
        Destroy(fb);

    }
    public void destroySword(GameObject sword)
    {

        Destroy(sword);

    }
    public void destroyHpItem(GameObject hpItem)
    {

        Destroy(hpItem);

    }
    public void swordAttack()
    {
        myAnim.SetTrigger("Attack");
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackArea.position, attackRange, enemy_layer);
        foreach(Collider2D enemy in enemies)
        {
            Debug.Log("hit");
            enemy.GetComponent<Enemy>().TakeDamage(50);
            
        }
    }
    public void heavyAttack()
    {
        myAnim.SetTrigger("HeavyAttack");
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackArea.position, attackRange, enemy_layer);
        foreach (Collider2D enemy in enemies)
        {
            Debug.Log("hit");
            enemy.GetComponent<Enemy>().TakeDamage(100);
            
        }
    }
   
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackArea.position, attackRange);
        
    }

}
