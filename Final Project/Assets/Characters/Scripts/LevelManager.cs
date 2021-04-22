using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int nextLevel;
    public string nextLevel_String;
   
    public bool intToLoadNext = false;

    public AudioClip newTrack;
    private AudioManager am;

   
    // Start is called before the first frame update

    void Start()
    {
        am = FindObjectOfType<AudioManager>();
        if(newTrack!= null)
         am.ChangeMusic(newTrack);
    }

  

   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject levelLoader = collision.gameObject;

        if (collision.gameObject.name == "Player")
        {
            LoadNext();
        }
    }

    void LoadNext()
    {
        if (intToLoadNext)
        {
            SceneManager.LoadScene(nextLevel);
        }
        else
        {
            SceneManager.LoadScene(nextLevel_String);
        }
    }
}
