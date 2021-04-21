using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void PlayerDeadEvent();
public class Player : Character
{
   
    public event PlayerDeadEvent Died;

    private static Player instance;

    public  static Player Instance
    {
        get
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<Player>();
            }
            return instance;
        }
    }

    public Rigidbody2D myrb;
  
    public Transform playerStartPos;
    public SpriteRenderer myRend;
    public float jumpHeight = 10f;
   
    private BoxCollider2D box2D;
    private bool DoubleJump;
    public LayerMask platformLayer;
    

    public Transform fireballSpot;
    public GameObject fireball;
    bool canUseFireball;

   
    public int player_hp;
    private bool invincible = false;
    [SerializeField]
    private float invincibleTimer = 3;

    public AudioSource jump_Audio;
    public AudioSource doubleJump_Audio;
    public AudioSource Sword1_Audio;
    public AudioSource item_audio;
    public AudioSource playerDamage_audio;
    public AudioSource Sword2_Audio;
  

    public GameObject fb_pf;
    public GameObject inventory;
    public GameObject fb;
    public GameObject sword;
    public GameObject sword_pf;
    bool canUseSword;
    public GameObject hpItem;
    public GameObject hpItem_pf;
    bool canUseHP;

    public EdgeCollider2D specialSwordCollider;

    public EdgeCollider2D SpecialSwordCollider
    {
        get
        {
            return specialSwordCollider;
        }
    }



    public override bool isDead
    {
        get
        {
            if(player_hp <= 0)
            {
                WhenDead();
            }
           
            return player_hp <= 0;
            
        }
    }



    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        playerDamage_audio = GameObject.Find("PlayerDamage").GetComponent<AudioSource>();
        Sword2_Audio = GameObject.Find("SwordAttack2").GetComponent<AudioSource>();

        item_audio = GameObject.Find("ItemSfx").GetComponent<AudioSource>();
    }



    // Start is called before the first frame update
    public override void Start()
    {

       
      
        myrb = this.GetComponent<Rigidbody2D>();
        box2D = this.GetComponent<BoxCollider2D>();
        base.Start();
        myRend = this.GetComponent<SpriteRenderer>();
        player_hp = health;
        playerStartPos = GameObject.FindGameObjectWithTag("Respawn").transform;
       
        Health.maxHP(health);

    }

    private void FixedUpdate()
    {
        

        if (!Damaged && !isDead)
        {
            if (!Attacking)
            {
                float run = Input.GetAxisRaw("Horizontal");
                Movement();

                MyAnim.SetFloat("speed", Mathf.Abs(run));
                MyAnim.SetFloat("vSpeed", myrb.velocity.y);
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
                    jump_Audio.Play();

                }
            }
            

        }
        
        

    }

    // Update is called once per frame
    void Update()
    {
        if (!Damaged && !isDead)
        {
            UseHealthItem();
            MyAnim.SetBool("Jumping", OnGround());


            if (Input.GetKeyDown(KeyCode.Space) && DoubleJump && !OnGround())
            {

                myrb.velocity = Vector2.up * jumpHeight;
                DoubleJump = false;
                jump_Audio.Play();

            }



            if (Input.GetMouseButtonDown(0) && OnGround())
            {

                swordAttack();
                Sword1_Audio.Play();
            }


            if (Input.GetKeyDown(KeyCode.F) && canUseFireball)
            {
                MyAnim.SetTrigger("Fireball");
                GameObject fire = Instantiate(fireball, fireballSpot.position, fireballSpot.rotation);
                

                destroyFireball(fb);
                canUseFireball = false;



            }

            else if (Input.GetMouseButton(1) && canUseSword)
            {
                heavyAttack();
                Sword2_Audio.Play();
                destroySword(sword);
                canUseSword = false;
            }

        }



    }

    public void SpecialSwordAttack()
    {
        SpecialSwordCollider.enabled = true;
    }

    public void WhenDead()
    {
        if(Died != null)
        {
            Died();
        }
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if(other.tag == "Kunai")
        {
            Destroy(other.gameObject);
            damageTag.Add("Kunai");
            StartCoroutine(TakeDamage());
        }
        else if(other.tag == "Level_End")
        {
            Debug.Log("Loading next Level....");
        }
        else if (other.tag == "troll")
        {
            damageTag.Add("troll");
            StartCoroutine(TakeDamage());
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
       
        
        if (collision.gameObject.tag == "fb")
        {
            Destroy(collision.gameObject);
            addFireball();
            item_audio.Play();
         
        }
        else if(collision.gameObject.tag == "sword")
        {
            Destroy(collision.gameObject);
            addSword();
            item_audio.Play();
        }
        else if (collision.gameObject.tag == "health")
        {
            Destroy(collision.gameObject);
            addHpItem();
            item_audio.Play();
        }
       
    }

    private void OnLevelWasLoaded(int level)
    {
        FindPlayerStartPos();
        if (playerStartPos == null)
        {
            playerStartPos = GameObject.Find("StartPos").transform;
        }
    }

    void FindPlayerStartPos()
    {
        transform.position = GameObject.FindWithTag("Respawn").transform.position;
    }
    public void Movement()
    {
        float run = Input.GetAxisRaw("Horizontal");

        myrb.velocity = new Vector2(run ,0) * speed + new Vector2(0,myrb.velocity.y);
      
    }
    

    public bool OnGround()
    {
        float extraH = 0.4f;
        RaycastHit2D hitinfo = Physics2D.BoxCast(box2D.bounds.center, box2D.bounds.size, 0f, Vector2.down, extraH, platformLayer);
        return hitinfo.collider != null;
        
    }
    
   
    public void UseHealthItem()
    {
        if(player_hp < 100 && canUseHP && Input.GetKeyDown(KeyCode.LeftShift))
        {
            destroyHpItem(hpItem);
            player_hp += 45;
            Health.setHP(player_hp);
            canUseHP = false;
            if(player_hp > 100)
            {
                player_hp = health;
                Health.setHP(player_hp);
            }
        }
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
        MyAnim.SetTrigger("Attack");

        
    }
    public void heavyAttack()
    {
        MyAnim.SetTrigger("HeavyAttack");
        
    }
   
    private IEnumerator InvincibleFlash()
    {
        while (invincible)
        {
            myRend.enabled = false;
            yield return new WaitForSeconds(0.1f);

            myRend.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public override IEnumerator TakeDamage()
    {
        if (!invincible)
        {
            if (damageTag.Contains("enemy1_Sword"))
            {
                player_hp -= 20;
                damageTag.Remove("enemy1_Sword");
            }
            else if (damageTag.Contains("enemy2_Sword"))
            {
                player_hp -= 35;
                damageTag.Remove("enemy2_Sword");
            }
            else if (damageTag.Contains("Kunai"))
            {
                player_hp -= 30;
                damageTag.Remove("Kunai");
            }
            else if (damageTag.Contains("Spike"))
            {
                player_hp -= 15;
                damageTag.Remove("Spike");
            }
            else if (damageTag.Contains("troll"))
            {
                player_hp -= 100;
                damageTag.Remove("troll");
            }
            playerDamage_audio.Play();
            Health.setHP(player_hp);

            if (!isDead)
            {
                MyAnim.SetTrigger("TakeDamage");
                invincible = true;
                StartCoroutine(InvincibleFlash());
                yield return new WaitForSeconds(invincibleTimer);
                invincible = false;
            }
            else
            {
                MyAnim.SetTrigger("Die");
               
            }
        }
       
    }

    public override void Die()
    {
       
        myrb.velocity = Vector2.zero;
        MyAnim.SetTrigger("Idle");
        player_hp = health;
        Health.maxHP(health);
        Health.setHP(health);
        transform.position = playerStartPos.position;
    }
}
