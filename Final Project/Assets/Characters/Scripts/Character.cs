using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Character : MonoBehaviour

{
    public Health_UI Health;
    
    public Animator MyAnim{ get; private set; }

    protected float speed;
    [SerializeField]
    protected int health;
    public bool Damaged { get; set; }
    public abstract bool isDead { get; }
    protected bool lookingRight;

    public bool Attacking { get; set; }
    public List<string> damageTag;
    public EdgeCollider2D swordCollider;
    
   

    public EdgeCollider2D SwordCollider
    {
        get
        {
            return swordCollider;
        }
    }
   
    // Start is called before the first frame update
    public virtual void Start()
    {
        MyAnim = this.GetComponent<Animator>();
        
    }

    public void SwordAttack()
    {
        SwordCollider.enabled = true;
    }

   

    public abstract IEnumerator TakeDamage();

    public abstract void Die();
    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "player_Sword")
        {
            damageTag.Add("player_Sword");
            StartCoroutine(TakeDamage());
        }
        else if(other.tag == "projectile")
        {
            damageTag.Add("projectile");
            StartCoroutine(TakeDamage());
        }
        else if(other.tag == "player_SpecialSword")
        {
            damageTag.Add("player_SpecialSword");
            StartCoroutine(TakeDamage());
        }
        else if (other.tag == "enemy1_Sword")
        {
            damageTag.Add("enemy1_Sword");
            StartCoroutine(TakeDamage());
        }
        else if (other.tag == "enemy2_Sword")
        {
            damageTag.Add("enemy2_Sword");
            StartCoroutine(TakeDamage());
        }
        else if (other.tag == "Kunai")
        {
            damageTag.Add("Kunai");
            StartCoroutine(TakeDamage());
        }
        else if(other.tag == "Spike")
        {
            damageTag.Add("Spike");
            StartCoroutine(TakeDamage());
        }
    }
    public void FlipCharacter()
    {
        lookingRight = !lookingRight;

        transform.Rotate(0f, 180f, 0f);
    }
}
