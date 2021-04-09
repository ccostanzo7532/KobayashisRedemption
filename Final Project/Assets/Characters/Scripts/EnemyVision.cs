using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    [SerializeField] private Enemy enemy;

    

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            enemy.Target = other.gameObject;
            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            enemy.Target = null;
            
        }
    }
}
