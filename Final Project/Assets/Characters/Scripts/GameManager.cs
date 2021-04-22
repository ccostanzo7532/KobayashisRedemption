using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameOver go;
    public GameOver win;
    public AudioSource gameOver;
    public Boss boss;
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        PlayerDied();
        BossDead();
    }
    public void PlayerDied()
    {
        if (Player.Instance.isDead)
        {
            go.gameObject.SetActive(true);
            Time.timeScale = 0;
           
        }
    }
    public void BossDead()
    {
        if(boss.boss_current_Health <= 0)
        {
            win.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
