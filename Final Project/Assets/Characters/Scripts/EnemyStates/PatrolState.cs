using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IEnemyState
{
    private Enemy enemy;
    private float patrolTimer;
    private float patrolDur = 10f;

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        Patroling();
        enemy.Movement();
    }

    public void Exit()
    {
        
    }

    public void OnTriggerEnter(Collider2D other)
    {
       
    }
    private void Patroling()
    {
       

        patrolTimer += Time.deltaTime;

        if (patrolTimer >= patrolDur)
        {
            enemy.ChangeEnemyState(new IdleState());
        }
    }

}
