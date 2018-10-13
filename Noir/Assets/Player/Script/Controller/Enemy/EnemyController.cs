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
        Defualt,
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
    private float BufferDis;
    private float EnemySpeed;


    //-----------------------Move-----------------   
    //----------------------Attack----------------
    public bool PlayerisDamage;
    private GameObject Attack_R_Collider;
    private GameObject Attack_L_Collider;
  /*  private GameObject AttackDown_R_Collider;
    private GameObject AttackSmall_L_Collider;
    private GameObject AttackBig_L_Collider;
    private GameObject AttackDown_L_Collider;*/

    //----------------------Attack----------------
    //--------------------Coroutine---------------
    private IEnumerator ResetStateCoroutine;


    //--------------------Coroutine---------------
    
    private void Awake()
    {
        EnemyAnimator = GetComponent<Animator>();
        enemyController = this;
        EnemyNav = GetComponent<NavMeshAgent>();
        Attack_R_Collider = GameObject.Find("EnemyAttack_R_Collider");
        Attack_L_Collider = GameObject.Find("EnemyAttack_L_Collider");
       

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
        if (EnemyNav.remainingDistance <= EnemyNav.stoppingDistance && enemyState == EnemyState.Movement && attackState == AttackState.Defualt) 
        {
            Attack(1);
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


    private void Attack(int AttackState_Number)
    {

        switch (AttackState_Number)
        {   
            case (int)AttackState.Attack1:
                attackState = AttackState.Attack1;
                EnemyAnimator.SetTrigger("Attack1");
                Attack_L_Collider.tag = "EnemyAttack_Small";
                Attack_R_Collider.tag = "EnemyAttack_Small";
                break;
            case (int)AttackState.Attack2:
                if (PlayerisDamage)
                {
                    EnemyAnimator.SetTrigger("Attack2");
                    PlayerisDamage = false;
                }
               
                break;
            case (int)AttackState.Attack3:
                if (PlayerisDamage)
                {
                    EnemyAnimator.SetTrigger("Attack3");
                    PlayerisDamage = false;
                }                
                break;
        }           
      
    }

    //--------------------------Aniamtion Event------------------------------------------
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
    public void EnemyDamage()
    {
        enemyState = EnemyState.Damage;
        if (ResetStateCoroutine != null)
        {
            Debug.Log("bb");
            StopCoroutine(ResetStateCoroutine);
        }
    }

    public void AttackCollider_Open(int AttackState_Number)
    {
        switch (AttackState_Number)
        {
            case (int)AttackState.Attack1:
                Attack_R_Collider.SetActive(true);
                break;
            case (int)AttackState.Attack2:
                Attack_L_Collider.SetActive(true);
                break;
            case (int)AttackState.Attack3:
                Attack_R_Collider.SetActive(true);
                Attack_L_Collider.SetActive(true);
                break;
        }       
    }
    public void AttackCollider_Close()
    {
        Attack_L_Collider.SetActive(false);
        Attack_R_Collider.SetActive(false);
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
