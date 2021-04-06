using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_controller : MonoBehaviour
{
    [SerializeField] private Rigidbody2D myrb;
    public Animator myPlayer;
    public Transform playerPos;
    private SpriteRenderer myRend;
    public float jumpHeight = 50f;
    public float speed = 5f;
    private bool DoubleJump = false;
    private bool onGround;

    public Transform fireballSpot;
    public GameObject fireball;
    bool canUseFireball;

    public int health = 100;
    public int player_hp;
    public Health_UI HP;

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

    public bool lookingRight;
    


    // Start is called before the first frame update
    void Start()
    {
        myrb = this.GetComponent<Rigidbody2D>();
        myPlayer = this.GetComponent<Animator>();
        myRend = this.GetComponent<SpriteRenderer>();
        player_hp = health;
        HP.maxHP(health);
        
    }



    // Update is called once per frame
    void Update()
    {
        float run = Input.GetAxisRaw("Horizontal");
        Movement();
        UseHealthItem();
        myPlayer.SetFloat("speed", Mathf.Abs(run));
    
       
       
        if(player_hp <= 0)
        {
            Respawn();
        }

        if (run < 0 && !lookingRight )
        {

          
           // myPlayer.SetInteger("Dir", 2);

            //myRend.flipX = true;
            FlipCharacter();
           



        }
        else if (run > 0 && lookingRight)
        {

            
           // myPlayer.SetInteger("Dir", 2);
            
           // myRend.flipX = false;
            FlipCharacter();




        }
        else if (Input.GetAxisRaw("Fire1") > 0)
        {

            swordAttack();
            myPlayer.SetBool("attack",false);

        }
        

        if (Input.GetKeyDown(KeyCode.F) && canUseFireball)
        {
            myPlayer.SetInteger("Dir", 4);
            GameObject fire = Instantiate(fireball, fireballSpot.position, fireballSpot.rotation);
           
            destroyFireball(fb);
            canUseFireball = false;



        }

        else if(Input.GetAxisRaw("Fire2") > 0 && canUseSword)
        {
            heavyAttack();
            destroySword(sword);
            canUseSword = false;
        }

        if (Input.GetAxisRaw("Jump") > 0 && onGround)

        {
            myPlayer.SetInteger("Dir", 0);
            myrb.velocity = new Vector2(run*speed, jumpHeight);
            DoubleJump = true;
            onGround = false;

        }
        else if (Input.GetKeyDown(KeyCode.Space) && DoubleJump)
        {
            myrb.velocity = new Vector2(run*speed, jumpHeight);
            DoubleJump = false;
            
        }
        
       
        
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            onGround = true;

        }
        else if(collision.gameObject.tag == "enemy")
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
            myPlayer.SetBool("TakingDamage", false);
        }
    }
    
    public void Movement()
    {
        float run = Input.GetAxisRaw("Horizontal");

        myrb.velocity = new Vector2(run * speed, myrb.velocity.y);
      
    }
    
    public void TakeDamge(int damage)
    {
        player_hp -= damage;
        HP.setHP(player_hp);
        myPlayer.SetBool("TakingDamage", true);
        
    }
    public void UseHealthItem()
    {
        if(player_hp < 100 && canUseHP && Input.GetKeyDown(KeyCode.LeftShift))
        {
            destroyHpItem(hpItem);
            player_hp += 45;
            HP.setHP(player_hp);
            canUseHP = false;
        }
    }
    public void Respawn()
    {
        
            playerPos.position = new Vector3(-10.23f, -4.26f, 0);
            player_hp = health;
            HP.setHP(health);

      
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
        myPlayer.SetInteger("Dir", 1);
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackArea.position, attackRange, enemy_layer);
        foreach(Collider2D enemy in enemies)
        {
            Debug.Log("hit");
            enemy.GetComponent<enemy>().TakeDamage(5);
            
        }
    }
    public void heavyAttack()
    {
        myPlayer.SetInteger("Dir", 5);
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackArea.position, attackRange, enemy_layer);
        foreach (Collider2D enemy in enemies)
        {
            Debug.Log("hit");
            enemy.GetComponent<enemy>().TakeDamage(50);
            
        }
    }
    public void FlipCharacter()
    {
        lookingRight = !lookingRight;

        transform.Rotate(0f, 180f, 0f);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackArea.position, attackRange);
        
    }

}
