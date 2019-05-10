using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class EnemyBehaviour : Character
{

    public enum EnemyState
    {
//Idle = 0x01,
        Move = 0x01,
        Attack = 0x02,
        Skill = 0x04,
        Damage = 0x08,
        Dead = 0x10,

        
        CanAttack = Move | Attack,

    }

    public EnemyState enemyState;

    #region Data
    public EnemyData enemyData;
    private EnemyData.EnemyInfo enemyInfo;
    public Dictionary<int, EnemyData.AttackDisInfo> AttackDisInfoCollection;

    #endregion

    #region Component
    private Animator animator;
    public NavMeshAgent navMeshAgent;
    private AttackSystem attackSystem;
    private ObjectPoolManager objectPoolManager;
    private ParticleManager particleManager;
    private GetHitComponent getHitComponent;
    private AudioSource audiosource;
    private Rigidbody rigidbody;
    #endregion

    #region UI
    public UI_FollowEnemy HP;
    public GameObject HP_UI;
    private Image hp_UI;
    public Vector3 Pos_UI;
    #endregion

    public Transform ShootingStartPos;

    private string preParticle;
    private bool isMove;
    private float animationBlendTreeControllValue;
    public float disWithPlayer;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        attackSystem = GetComponent<AttackSystem>();
        objectPoolManager = GetComponent<ObjectPoolManager>();
        getHitComponent = GetComponent<GetHitComponent>();
        audiosource = GetComponent<AudioSource>();
        rigidbody = GetComponent<Rigidbody>();
        enemyInfo = enemyData.enemyInfo;
        CreatAttackDisInfoCollection();

    }

    void Start ()
    {
        enemyState = EnemyState.Move;
        // getHitComponent.DamageFunction=
        hp_UI = Instantiate(HP_UI, GameObject.FindGameObjectWithTag("UI").transform).GetComponent<Image>();
        hp_UI.GetComponent<UI_FollowEnemy>().enemyBehaviour = this;
        HP = hp_UI.GetComponent<UI_FollowEnemy>();
        HP.CloseUI();
        getHitComponent.DamageFunction = Damage;
        animationBlendTreeControllValue = 0;
    }

    void Update ()
    {
        transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
        disWithPlayer = Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);

        
    }

    private void CreatAttackDisInfoCollection()
    {
        AttackDisInfoCollection = new Dictionary<int, EnemyData.AttackDisInfo>();
        for (int i = 0; i < enemyInfo.attackDisInfo.Length; i++)
        {
            AttackDisInfoCollection[enemyInfo.attackDisInfo[i].ID] = enemyInfo.attackDisInfo[i];

        }

    }

    public void AnimationControll(string animationTrigger,float targetValue,float animationacceleration)
    {


        AnimationBlendTreeControll(animator, animationTrigger, targetValue,ref animationBlendTreeControllValue, animationacceleration);
      //  animator.SetFloat(animationTrigger, animationacceleration);
            
    }

    public void Move()
    {
        // navMeshAgent.acceleration = acceleration;    
        navMeshAgent.isStopped = false;

        if (enemyState == EnemyState.Move)
        {
            if (disWithPlayer < enemyInfo.moveInfo.ChaseDis)
            {
                if (disWithPlayer > navMeshAgent.stoppingDistance + enemyInfo.moveInfo.BufferDis)
                {

                    AnimationControll("MoveState", 1, 0.2f);

                    //Move(enemyInfo.moveInfo.Acceleration);
                    ChageSpeed(enemyInfo.moveInfo.Acceleration);
                    navMeshAgent.SetDestination(GameObject.FindGameObjectWithTag("Player").transform.position);
                }
                else if (disWithPlayer <= navMeshAgent.stoppingDistance + enemyInfo.moveInfo.BufferDis && disWithPlayer > navMeshAgent.stoppingDistance)
                {
                    ChageSpeed(enemyInfo.moveInfo.Acceleration);
                    navMeshAgent.SetDestination(GameObject.FindGameObjectWithTag("Player").transform.position);
                    AnimationControll("MoveState", 1, 0.2f);

                }
                else if (disWithPlayer <= navMeshAgent.stoppingDistance)
                {

                    AnimationControll("MoveState", 0, 0.2f);

                    StopMove();
                }

            }
        }
        
    }

    public void ChageSpeed(float acceleration)
    {
        navMeshAgent.acceleration = acceleration;
    }

    public void StopMove()
    {
        navMeshAgent.acceleration = 0;
        navMeshAgent.velocity = new Vector3(0, 0, 0);
        navMeshAgent.isStopped = true;
    }

    public void Attack(string animatorTrigger)
    {

        /*if (Random.Range(0, 1000) < AttackDisInfoCollection[1].AttackProbability)
        {
            attackSystem.Attack(animatorTrigger);
            //attackSystem.GetShtooingTargetPos = ShootingTargetPos;
        }        */
    }

    private Vector3 ShootingTargetPos(int bulletID)
    {
        return objectPoolManager.ObjectPoolItemInfoCollection[bulletID].ObjectPoolItemStartTransform.position + transform.rotation * new Vector3(0, 0, attackSystem.currentAttackInfo.shootingInfo.MaxDistance);
    }

    public void TriggerDamage(float damageValue,string animatortrigger)
    {
        animator.SetTrigger(animatortrigger);
        audiosource.Stop();
      //  audiosource.clip = AudioClip_Damage;
        audiosource.Play();
    }

    public void Dead()
    {
        animator.SetTrigger("Dead");
        SwitchState(16);
        DamageFX(1);
    }

    #region AniamtionEvent

    public void SwitchState(int Enemystate)
    {
        switch (Enemystate)
        {
            case (int)EnemyState.Move:
                enemyState = EnemyState.Move;


                break;

            case (int)EnemyState.Attack:
                enemyState = EnemyState.Attack;

                DetectForceExitAttack();
                break;

            case (int)EnemyState.Damage:
                enemyState = EnemyState.Damage;


                break;

            case (int)EnemyState.Dead:
                enemyState = EnemyState.Dead;


                break;
        }


    }


    #region Attack

    public void ResetToIdleState(int currentState)
    {
        StopCoroutine("detectForceExitAttack");

     /*   if (attackStateResetToIdle != null)
        {
            StopCoroutine(attackStateResetToIdle);

        }
        attackStateResetToIdle = resetToIdleState(currentState);*/
        StartCoroutine("resetToIdleState");



    }

    IEnumerator resetToIdleState()
    {

        yield return new WaitUntil(() => !attackSystem.IsAttack);

        SwitchState(1);



      //  ForceMove = false;

    }


    public void AttackDisplacement(int AttackId)
    {
        //  Debug.Log("Attack hhh");
        if (enemyState == EnemyState.Attack)
        {
            Displacement(rigidbody,
               transform.rotation,
               attackSystem.AttackCollection[AttackId].moveInfo.MoveSpeed,
               attackSystem.AttackCollection[AttackId].moveInfo.MoveDistance,
               attackSystem.AttackCollection[AttackId].moveInfo.MoveDirection_X,
               attackSystem.AttackCollection[AttackId].moveInfo.MoveDirection_Y,
               attackSystem.AttackCollection[AttackId].moveInfo.MoveDirection_Z
               );
        }
    }

    public void DetectForceExitAttack()
    {
        StopCoroutine("detectForceExitAttack");
        StartCoroutine("detectForceExitAttack");

    }

    IEnumerator detectForceExitAttack()
    {

        yield return new WaitUntil(() => (enemyState & EnemyState.Attack) == 0);
       // Debug.Log(playerState);
        attackSystem.ForceExitAttack();

    }


    #endregion



    public void DamageFX(int damageState)
    {
        animator.speed = 0.1f;

        StopCoroutine("DamageStopEffect");

        switch (damageState)
        {
            case 0:
                MainCamera_New.mainCamera.CameraShake(0.08f, 0.05f);
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

    public void PlayParticle(string Id)
    {
        /* particleManager.GetParticle(Id).*/
        if (preParticle != null)
        {
            particleManager.GetParticle(preParticle).Stop();


        }

        preParticle = Id;
        particleManager.GetParticle(Id).Play();
    }

    public void Damage(float damageValue, string animatorTrigger)
    {
        HP.HP -= damageValue;
        animator.SetTrigger(animatorTrigger);


    }

    #endregion
}
