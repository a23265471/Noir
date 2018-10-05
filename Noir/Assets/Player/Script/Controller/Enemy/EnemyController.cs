using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,
        Move,
        Attack,
        Damage,
        GetDown,
        GetUp,
    }
    private EnemyState enemyState;

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
    private float EnemySpeed;

    //-----------------------Move-----------------   
    // Use this for initialization
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
    }

    // Update is called once per frame
    void Update()
    {

        EnemyMove();


    }

    private void EnemyMove()
    {
        PlayerDis = Vector3.Distance(transform.position, PlayerController.playerController.transform.position);

        
       if (PlayerDis <= EnemyNav.stoppingDistance)
        {
            enemyState = EnemyState.Idle;
           
        }
       else if (CanChase && PlayerDis <= PlayerChaseDis)
        {
            
            EnemyNav.SetDestination(PlayerController.playerController.transform.position);
            enemyState = EnemyState.Move;
        }
        // Debug.Log(EnemyNav.isStopped);
        // Debug.Log(enemyState);
        EnemyMoveState();
    }
    private void EnemyMoveState()
    {
        switch (enemyState)
        {
            case EnemyState.Idle:
                EnemyMove_parameter = Mathf.Lerp(EnemyMove_parameter, 0, StopAccelertion);
                break;
            case EnemyState.Move:
                EnemyMove_parameter = Mathf.Lerp(EnemyMove_parameter, 1, MoveAccelertion);
                break;
 
        }

        EnemyMove_parameter = Mathf.Clamp(EnemyMove_parameter, 0, 1);
        EnemyAnimator.SetFloat("MoveState", EnemyMove_parameter);
    }



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
