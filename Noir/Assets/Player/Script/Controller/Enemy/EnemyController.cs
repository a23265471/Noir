using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public enum EnemyState
    {
        Movement,
        Attack,
        Damage,
        GetDown,
        GetUp,
    }
    private enum Movestate
    {
        Idle,
        Move,
    }
    private enum AttackState
    {
        Attack1,
        Attack2,
        Attack3,
        RangAttack,
        DashAttack,
    }
    private EnemyState enemyState;
    private Movestate moveState;
    private AttackState attackState;

    private Animator EnemyAnimator;
    public static EnemyController enemyController;
    public bool EnemyCanDamage;

    private NavMeshAgent EnemyNav;
    //-----------------------Move-----------------   
    public float PlayerChaseDis;//EneyData
    private float PlayerDis;
    public bool CanChase;
    private float EnemyMove_parameter;
    public float EnemyMaxSpeed;
    public float MoveAccelertion;//EneyData
    public float StopAccelertion;//EneyData   
    public float BufferDis;
    private float EnemySpeed;

    //-----------------------Move-----------------   

    //--------------------Coroutine---------------
    private IEnumerator ResetStateCoroutine;


    //--------------------Coroutine---------------
    
    private void Awake()
    {
        EnemyAnimator = GetComponent<Animator>();
        enemyController = this;
        EnemyNav = GetComponent<NavMeshAgent>();
       // Player = GameObject.FindGameObjectWithTag("Player").gameObject;
    }

    void Start()
    {
        CanChase = true;
        enemyState = EnemyState.Movement;
        ResetStateCoroutine = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyState == EnemyState.Movement)
        {
            EnemyMove();
        }
        Debug.Log(enemyState);
    }

    private void EnemyMove()
    {
        EnemyNav.SetDestination(PlayerController.playerController.transform.position);

        if (EnemyNav.velocity!=new Vector3(0, 0, 0))
        {
            if (moveState == Movestate.Idle)
            {
                BufferDis = 0.5f;
                EnemyNav.acceleration = Mathf.Pow(EnemyNav.speed, 2) / (2 * BufferDis);
            }
            moveState = Movestate.Move;           
        }
        else
        {
            if (moveState == Movestate.Move)
            {
                BufferDis = 0;
                EnemyNav.acceleration = Mathf.Pow(EnemyNav.speed, 2) / (2 * BufferDis);
            }
            moveState = Movestate.Idle;           
        }       

        Debug.Log(EnemyNav.remainingDistance);
        EnemyMoveState();
    }

    private void EnemyMoveState()
    {
        switch (moveState)
        {
            case Movestate.Idle:
                EnemyMove_parameter = Mathf.Lerp(EnemyMove_parameter, 0, StopAccelertion);
                break;
            case Movestate.Move:
                EnemyMove_parameter = Mathf.Lerp(EnemyMove_parameter, 1, MoveAccelertion);
                break;
        }
        EnemyMove_parameter = Mathf.Clamp(EnemyMove_parameter, 0, 1);
        EnemyAnimator.SetFloat("MoveState", EnemyMove_parameter);
    }
    //--------------------------Aniamtion Event------------------------------------------
    public void EnemyDamage()
    {
        enemyState = EnemyState.Damage;
        if (ResetStateCoroutine != null)
        {
            Debug.Log("bb");
            StopCoroutine(ResetStateCoroutine);
        }
    }

    public void EnemyChangToIdle(float WaitTime)
    {
        ResetStateCoroutine = ResetState(WaitTime);
        StartCoroutine(ResetStateCoroutine);      
    }

    IEnumerator ResetState(float WaitTime)
    {
        yield return new WaitForSeconds(WaitTime);
        Debug.Log("aa");
        enemyState = EnemyState.Movement;
        moveState = Movestate.Idle;
    }
    //--------------------------Aniamtion Event------------------------------------------
    //---------------------------Collider------------------------------------------------
    private void OnTriggerEnter(Collider other)//判斷是否被攻擊
    {   
        if (other.tag == "PlayerAttack_Big")
        {
            EnemyAnimator.SetTrigger("Damage_Big");
            transform.LookAt(PlayerController.playerController.transform.position);
        }

        else if (other.tag == "PlayerAttack_Small")
        {
            EnemyAnimator.SetTrigger("Damage_Small");
            transform.LookAt(PlayerController.playerController.transform.position);
            
        }
        else if(other.tag == "PlayerLongAttack")
        {
            EnemyAnimator.SetTrigger("Damage_Small");
            transform.LookAt(PlayerController.playerController.transform.position);
        }                         
        //EnemyCanDamage = false;
    }
   

}
