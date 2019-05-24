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
        if (enemyBehaviour.disWithPlayer <= enemyBehaviour.enemyData.enemyInfo.attackDisInfo[0].AttackDis)
        {
            Attack();
        }
    }
    
    private void Move()
    {
      
    }

    private void Attack()
    {
        enemyBehaviour.Attack("Attack");

    }

}
