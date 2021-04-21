using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int nextLevel;
    public string nextLevel_String;

    public bool intToLoadNext = false;

    public AudioSource bg_Music;
    public AudioSource boss_music;
    // Start is called before the first frame update

    void Start()
    {
        bg_Music = GameObject.Find("BGMusic").GetComponent<AudioSource>();
        bg_Music.Play();
       
        DontDestroyOnLoad(bg_Music.gameObject);

    }

    // Update is called once per frame
    void Update()
    {
       
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
