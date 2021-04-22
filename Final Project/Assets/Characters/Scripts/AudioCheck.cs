using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCheck : MonoBehaviour
{
    public GameObject audio;
    // Start is called before the first frame update
    void Start()
    {
        if (FindObjectOfType<AudioManager>())
        {
            return;
        }
        else
        {
            Instantiate(audio, transform.position, transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
