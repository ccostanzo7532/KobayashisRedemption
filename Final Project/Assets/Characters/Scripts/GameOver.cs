using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    public void MainMenuButton()
    {
        Destroy(GameObject.Find("Player").gameObject);
        Destroy(FindObjectOfType<AudioManager>().gameObject);
        Destroy(GameObject.Find("Boss").gameObject);
       
        SceneManager.LoadScene("MainMenu");
    }
}
