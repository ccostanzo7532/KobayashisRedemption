using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollisions : MonoBehaviour
{
    
    public Collider2D other;
   

    private void Awake()
    {
        Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), other, true);
        
    }
   
    
}
