﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState :IEnemyState
{
    private Enemy enemy;

    private float idleTimer;
    private float idleDur;
    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
        idleDur = UnityEngine.Random.Range(1, 10);
    }

    public void Execute()
    {
        
        Idle();
        if(enemy.Target != null)
        {
            enemy.ChangeEnemyState(new PatrolState());
        }

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
