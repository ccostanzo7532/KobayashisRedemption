using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour

{
    public Health_UI Health;
    protected Animator myAnim;

    protected float speed = 3.5f;
    protected bool lookingRight;
    // Start is called before the first frame update
    public virtual void Start()
    {
        myAnim = this.GetComponent<Animator>();
        
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
