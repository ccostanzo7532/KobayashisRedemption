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
        if (damageTag.Contains(other.tag))
        {
            StartCoroutine(TakeDamage());
        }
    }
    public void FlipCharacter()
    {
        lookingRight = !lookingRight;

        transform.Rotate(0f, 180f, 0f);
    }
}
