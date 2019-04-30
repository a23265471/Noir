using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyBehaviour : MonoBehaviour {

    public enum EnemyState
    {
        Move = 0x01,
        Attack = 0x02,
        Skill = 0x04,
        Damage = 0x08,
        Dead = 0x10,

        CanAttack = Move | Attack,

    }

    public EnemyState enemyState;


    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private AttackSystem attackSystem;

    public Transform ShootingStartPos;
   
    private bool isMove;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        attackSystem = GetComponent<AttackSystem>();

    }

    void Start ()
    {
        enemyState = EnemyState.Move;

	}
	
	void Update ()
    {

    }

    public void Move(float acceleration)
    {
        navMeshAgent.acceleration = acceleration;
        navMeshAgent.SetDestination(GameObject.FindGameObjectWithTag("Player").transform.position);
    }

    public void SlowDownSpeed(float acceleration)
    {
        navMeshAgent.acceleration = acceleration;
    }

    public void StopMove()
    {
        navMeshAgent.acceleration = 0;
        navMeshAgent.velocity = new Vector3(0, 0, 0);
    }

    public void Attack(string animatorTrigger)
    {
        attackSystem.Attack(animatorTrigger);
        attackSystem.GetShtooingTargetPos = ShootingTargetPos;
    }

    private Vector3 ShootingTargetPos()
    {
        return ShootingStartPos.position + ShootingStartPos.rotation * new Vector3(0, 0, attackSystem.currentAttackInfo.shootingInfo.MaxDistance);

    }

    public void Damage()
    {
        animator.SetTrigger("Damage");
    }

    public void Dead()
    {
        animator.SetTrigger("Dead");
    }

    public void SwitchState(int Enemystate)
    {
        switch (Enemystate)
        {
            case (int)EnemyState.Move:
                enemyState = EnemyState.Move;


                break;

            case (int)EnemyState.Attack:
                enemyState = EnemyState.Attack;


                break;

            case (int)EnemyState.Damage:
                enemyState = EnemyState.Damage;


                break;

            case (int)EnemyState.Dead:
                enemyState = EnemyState.Dead;


                break;
        }


    }
         


}
