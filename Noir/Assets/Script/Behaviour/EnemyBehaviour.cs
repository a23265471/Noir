using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyBehaviour : MonoBehaviour {

    public enum EnemyState
    {
        Move = 0x01,
        Attack = 0x02,
        Damage = 0x04,
        Dead = 0x08,

        CanAttack = Move | Attack,

    }

    public EnemyState enemyState;


    private Animator animator;
    private NavMeshAgent navMeshAgent;

    public EnemyData enemyData;
    private EnemyData.MoveInfo enemyMoveInfo;

    private float playerDis;
    private bool isMove;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        enemyMoveInfo = enemyData.enemyInfo.moveInfo;
    }

    void Start ()
    {
        enemyState = EnemyState.Move;

	}
	
	void Update ()
    {
        playerDis = Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, transform.position);

        Move();

    }

    public void Move()
    {
        if (playerDis <= enemyMoveInfo.ChaseDis)
        {

            Debug.Log(playerDis);

            if (playerDis <= navMeshAgent.stoppingDistance + 2 && playerDis > navMeshAgent.stoppingDistance)
            {
                navMeshAgent.acceleration = 0.2f;


            }
            else if (playerDis <= navMeshAgent.stoppingDistance)
            {
                navMeshAgent.acceleration = 0;
                navMeshAgent.velocity = new Vector3(0, 0, 0);

            }
            else
            {
                navMeshAgent.acceleration = 6;

            }
            navMeshAgent.SetDestination(GameObject.FindGameObjectWithTag("Player").transform.position);

        }
        
    }



}
