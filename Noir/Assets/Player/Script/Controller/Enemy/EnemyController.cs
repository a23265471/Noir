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

        
       if (PlayerDis <= EnemyNav.stoppingDistance && !EnemyNav.isStopped)
        {
            enemyState = EnemyState.Idle;
            EnemyNav.isStopped = true;
        }
       else if (CanChase && PlayerDis <= PlayerChaseDis)
        {
            EnemyNav.isStopped = false;
            EnemyNav.SetDestination(PlayerController.playerController.transform.position);
            enemyState = EnemyState.Move;
        }
        Debug.Log(EnemyNav.isStopped);
        Debug.Log(enemyState);
    }




    private void OnTriggerEnter(Collider other)//判斷是否被攻擊
    {   
        if (other.tag == "PlayerAttack_Big")
        {
            EnemyAnimator.SetTrigger("Damage_Big");
        }

        else if (other.tag == "PlayerAttack_Small")
        {
            EnemyAnimator.SetTrigger("Damage_Small");                
        }
            
            transform.LookAt(PlayerController.playerController.transform.position);
        
        
        //EnemyCanDamage = false;
    }
   

}
