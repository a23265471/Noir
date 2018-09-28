using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private Animator EnemyAnimator;
    public static EnemyController enemyController;
    public bool EnemyCanDamage;

    private NavMeshAgent Nav;
    private Transform Player;

    // Use this for initialization
    private void Awake()
    {
        EnemyAnimator = GetComponent<Animator>();
        enemyController = this;
        //Nav = GetComponent<NavMeshAgent>();
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       // Nav.SetDestination(Player.position);
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
    /*private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerAttack_Small"|| other.tag == "PlayerAttack_Big") 
        {
            EnemyCanDamage = true;
        }
        
    }*/

}
