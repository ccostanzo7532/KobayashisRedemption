using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public Transform player;
    Vector3 offset;
    
    
    
    
    // Update is called once per frame
    void Update()
    {
        offset = new Vector3(0, 0, -5);
        this.transform.position = player.position + offset;

    }
}
