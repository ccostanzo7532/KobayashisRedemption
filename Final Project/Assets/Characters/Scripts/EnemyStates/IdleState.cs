using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState :IEnemyState
{
    private Enemy enemy;

    private float idleTimer;
    private float idleDur = 5f;
    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        Debug.Log("idle");
        Idle();
    }

    public void Exit()
    {
       
    }

    public void OnTriggerEnter(Collider2D other)
    {
       
    }

    private void Idle()
    {
        enemy.MyAnim.SetFloat("speed", 0);

        idleTimer += Time.deltaTime;

        if(idleTimer >= idleDur)
        {
            enemy.ChangeEnemyState(new PatrolState());
        }
    }
    
}
