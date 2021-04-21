using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    public Collider2D P_Attack;
    public Collider2D P_Attack2;
    
    // Start is called before the first frame update
    void Start()
    {
        P_Attack = GameObject.Find("Player").transform.GetChild(0).GetComponent<Collider2D>();
        P_Attack2 = GameObject.Find("Player").transform.GetChild(2).GetComponent<Collider2D>();

        Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), P_Attack, true);
        Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), P_Attack2, true);
    }


   
        
    
}
