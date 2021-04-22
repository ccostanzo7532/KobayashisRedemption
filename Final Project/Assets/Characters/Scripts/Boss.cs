using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
  
{
    private Animator bossAnim;
     public Health_UI bossHp;
    public int boss_max_Health = 600;
    public int boss_current_Health;
    public Transform playerTran;
    private bool isRotated;
    public Collider2D swordCollider;
    public AudioSource hit;
    public AudioSource sword;

 
// Start is called before the first frame update
void Start()
    {
        bossAnim = this.GetComponent<Animator>();
        boss_current_Health = boss_max_Health;
        bossHp.maxHP(boss_max_Health);
        playerTran = Player.Instance.transform;
        
    }

    public void SwingSword()
    {
        swordCollider.enabled = true;
        sword.Play();
    }
    public void FollowPlayer()
    {
        if(transform.position.x > playerTran.position.x && isRotated)
        {
            transform.Rotate(0f, 180f, 0f);
            isRotated = false;
        }
        else if(transform.position.x < playerTran.position.x && !isRotated)
         {
            transform.Rotate(0f, 180f, 0f);
            isRotated = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "player_Sword")
        {
            BossHit(50);
           

        }
    }

    
    

     public void BossHit(int damage)
    {
        boss_current_Health -= damage;
        bossHp.setHP(boss_current_Health);
        bossAnim.SetTrigger("Hit");
        hit.Play();

        if(boss_current_Health <= 300)
        {
            bossAnim.SetBool("isEnraged",true);
        }
        if(boss_current_Health <= 0)
        {
            bossAnim.SetTrigger("Die");
        }
    }

}
