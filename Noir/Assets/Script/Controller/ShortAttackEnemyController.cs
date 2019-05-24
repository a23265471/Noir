using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShortAttackEnemyController : MonoBehaviour {

   
    private EnemyBehaviour enemyBehaviour;
    // float disWithPlayer;


    private void Awake()
    {
        enemyBehaviour = GetComponent<EnemyBehaviour>();
    }

    void Start ()
    {
		
	}
	
	void Update ()
    {
       

        Move();
        if (enemyBehaviour.disWithPlayer <= enemyBehaviour.navMeshAgent.stoppingDistance)
        {
            Attack();
        }
    }

    private void Move()
    {
        enemyBehaviour.Move();
    }
    private void Attack()
    {
        enemyBehaviour.Attack("Attack1");
    }

   
}
