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

    private AudioSource audioSource;
    

    private bool CanAttack;
    private bool isMove;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        physicsCollider = GetComponent<SphereCollider>();
        particleManager = GetComponent<ParticleManager>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
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
                        Attack();
                        Debug.Log("jjj");
                    }

                }
                else
                {
                    navMeshAgent.acceleration = 6;

                }

                transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform.position);
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
        CanAttack = false;
        BoneCollider.SetActive(true);
        PlayAudio();
        particleManager.GetParticle("Bone").Play();
        physicsCollider.enabled = false;
        meshRenderer.SetActive(false);
        navMeshAgent.isStopped = true;
        DamageFX(1);
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerAttack_Small")|| other.CompareTag("PlayerLongAttack") || other.CompareTag("PlayerAttack_Big") || other.CompareTag("PlayerAttack_BigSkill"))
        {
            Attack();
        }


    }
    public void PlayAudio()
    {
        audioSource.Play();

    }

    public void DamageFX(int damageState)
    {
        animator.speed = 0.1f;

        StopCoroutine("DamageStopEffect");

        switch (damageState)
        {
            case 0:
                MainCamera_New.mainCamera.CameraShake(0.05f, 0.05f);
                break;
            case 1:
                MainCamera_New.mainCamera.CameraShake(0.1f, 0.09f);


                break;
        }

        //   damageStopEffect = DamageStopEffect(stopTime);
        StartCoroutine("DamageStopEffect");

    }

    IEnumerator DamageStopEffect()
    {
        yield return new WaitForSeconds(0.004f);//0.006
        animator.speed = 1f;
    }

}
