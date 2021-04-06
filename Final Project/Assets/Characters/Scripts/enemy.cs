using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public int enemyHealth = 100;
    SpriteRenderer enemySprite;
    public int enemy_hp;
    public Health_UI HP;
    public List<Transform> dots;
    public Transform Player;
    public int point = 0;
    int pointValue = 1;
    public float speed = 1f;
    public float playerRange = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        enemySprite = GetComponent<SpriteRenderer>();
        enemy_hp = enemyHealth;
        HP.maxHP(enemyHealth);
    }

    // Update is called once per frame
    void Update()
    {
        GoToNext();

       
       
    }
    
    public void GoToNext()
    {
        Transform goal = dots[point];
        if (goal.transform.position.x > transform.position.x)
        {
            //enemySprite.flipX = false;
            transform.localScale = new Vector3(-0.33f, 0.36f, 0);
        }
        else
        {
            //enemySprite.flipX = true;
            transform.localScale = new Vector3(0.33f, 0.36f, 0);
        }

        transform.position = Vector2.MoveTowards(transform.position,goal.position,speed* Time.deltaTime);

        if(Vector2.Distance(transform.position,goal.position) < 0.2f)
        {
            if(point == dots.Count - 1)
            {
                pointValue = -1;
            }

            if(point == 0)
            {
                pointValue = 1;
            }
            point += pointValue;
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
        HP.setHP(enemy_hp);
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
        Debug.Log("enemy died");
        Destroy(gameObject);
    }


}
