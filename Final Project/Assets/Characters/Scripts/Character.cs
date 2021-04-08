using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour

{
    public Health_UI Health;
    
    public Animator MyAnim{ get; private set; }

    protected float speed;
    protected bool lookingRight;
    // Start is called before the first frame update
    public virtual void Start()
    {
        MyAnim = this.GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void FlipCharacter()
    {
        lookingRight = !lookingRight;

        transform.Rotate(0f, 180f, 0f);
    }
}
