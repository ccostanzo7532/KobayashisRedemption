using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableSword : MonoBehaviour
{
    [SerializeField]
    private string CharacterTag = "Player";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == CharacterTag)
        {
            GetComponent<Collider2D>().enabled = false;
        }
    }
}
