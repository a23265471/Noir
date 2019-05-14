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
        if (enemyBehaviour.enemyState != EnemyBehaviour.EnemyState.Dead)
        {
            if (enemyBehaviour.disWithPlayer <= enemyBehaviour.enemyData.enemyInfo.moveInfo.ChaseDis && !enemyBehaviour.HP.UIOpened)
            {
                enemyBehaviour.HP.OpenUI();
            }
            else if (enemyBehaviour.disWithPlayer > enemyBehaviour.enemyData.enemyInfo.moveInfo.ChaseDis)
            {
                enemyBehaviour.HP.CloseUI();
            }
        }
       
        if (enemyBehaviour.HP.UIOpened)
        {
            enemyBehaviour.hp_UI.transform.position = MainCamera_New.mainCamera.camera.WorldToScreenPoint(transform.position + enemyBehaviour.Pos_UI);

        }

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
