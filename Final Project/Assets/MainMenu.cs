using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource music;
    public AudioSource click;
    public AudioSource select;


    public void Start()
    {
        click = GameObject.Find("Click").GetComponent<AudioSource>();
        select = GameObject.Find("Select").GetComponent<AudioSource>();
        music = this.GetComponent<AudioSource>();
        music.Play();
    }
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void Click()
    {
        
        click.Play();
    }
    public void Select()
    {
       
        select.Play();
    }
} 
