using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BoneEnemy : MonoBehaviour {

    private NavMeshAgent navMeshAgent;
    public EnemyData enemyData;
    private EnemyData.EnemyInfo enemyInfo;
    private ParticleManager particleManager;
    private Animator animator;

    public GameObject meshRenderer;
    private SphereCollider physicsCollider;


    public GameObject BoneCollider;

    private bool CanAttack;
    private bool isMove;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        physicsCollider = GetComponent<SphereCollider>();
        particleManager = GetComponent<ParticleManager>();
        animator = GetComponent<Animator>();
        //meshRenderer = GetComponent<SkinnedMeshRenderer>();
        enemyInfo = enemyData.enemyInfo;
    }

    void Start ()
    {
        CanAttack = true;
        BoneCollider.SetActive(false);
        isMove = false;
    }
	
	void Update ()
    {
        Move();

    }

    private void Move()
    {
        float PlayerDis;
        PlayerDis = Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, transform.position);
     //   Debug.Log(enemyInfo.moveInfo.ChaseDis);

        if (CanAttack)
        {
            if(PlayerDis <= enemyInfo.moveInfo.ChaseDis)
            {
                if (!isMove)
                {
                    isMove = true;
                    animator.SetTrigger("Run");
                }

                if (PlayerDis <= navMeshAgent.stoppingDistance + 2 && PlayerDis > navMeshAgent.stoppingDistance) 
                {
                    navMeshAgent.acceleration = 0.2f;


                }
                else if (PlayerDis <= navMeshAgent.stoppingDistance)
                {
                    navMeshAgent.acceleration = 0;
                    navMeshAgent.velocity = new Vector3(0, 0, 0);

                    if (CanAttack)
                    {
                        CanAttack = false;
                        Attack();
                    }

                }
                else
                {
                    navMeshAgent.acceleration = 6;

                }

                navMeshAgent.SetDestination(GameObject.FindGameObjectWithTag("Player").transform.position);

            }
            else
            {
                if (isMove)
                {
                    isMove = false;
                    animator.SetTrigger("Idle");
                }

            }

        }
        

    }


    private void Attack()
    {
        BoneCollider.SetActive(true);
        particleManager.GetParticle("Bone").Play();
        physicsCollider.enabled = false;
        meshRenderer.SetActive(false);
        StartCoroutine("CloseBoneCollider");

    }

    IEnumerator CloseBoneCollider()
    {

        yield return new WaitForSeconds(0.3f);
        BoneCollider.SetActive(false);
        StartCoroutine("CloseGameObject");

    }

    IEnumerator CloseGameObject()
    {

        yield return new WaitWhile(() => particleManager.GetParticle("Bone").isPlaying);
        gameObject.SetActive(false);

    }

}
