using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class LongAttackEnemy : MonoBehaviour
{
    public static LongAttackEnemy longAttackEnemy;
    private EnemyBehaviour enemyBehaviour;
    private NavMeshAgent navMeshAgent;
    public EnemyData enemyData;
    public EnemyData.MoveInfo enemyMoveInfo;
   // private EnemyData.AttackDisInfo attackDisInfo;

    public float playerDis;

    private void Awake()
    {
        enemyBehaviour = GetComponent<EnemyBehaviour>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyMoveInfo = enemyData.enemyInfo.moveInfo;
       // attackDisInfo = enemyData.enemyInfo.attackDisInfo;
        longAttackEnemy = this;
    }

    private void Update()
    {
        playerDis = Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, transform.position);


      /*  if (playerDis <= attackDisInfo.LongAttackDis && PlayerBehaviour.playerBehaviour.playerState != PlayerBehaviour.PlayerState.Dead)  
        {
            //Attack("Attack");
        }
        */


    }
    
    private void Move()
    {
       // enemyBehaviour.Move(enemyMoveInfo.Acceleration);



    }

    private void Attack(string triggerAnimatorState)
    {
        enemyBehaviour.Attack(triggerAnimatorState);

    }

}
