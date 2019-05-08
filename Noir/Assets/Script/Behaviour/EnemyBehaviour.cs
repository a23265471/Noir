using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


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
    private ObjectPoolManager objectPoolManager;
    private ParticleManager particleManager;
    private GetHitComponent getHitComponent;
    private AudioSource audiosource;

    public UI_FollowEnemy HP;
    public GameObject HP_UI;
    private Image hp_UI;
    public Vector3 Pos_UI;

    public Transform ShootingStartPos;

    private string preParticle;
    private bool isMove;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        attackSystem = GetComponent<AttackSystem>();
        objectPoolManager = GetComponent<ObjectPoolManager>();
        getHitComponent = GetComponent<GetHitComponent>();
        audiosource = GetComponent<AudioSource>();
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

    }

    void Update ()
    {
        transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);


        if (LongAttackEnemy.longAttackEnemy.playerDis <= LongAttackEnemy.longAttackEnemy.enemyMoveInfo.ChaseDis && !HP.UIOpened)
        {
            HP.OpenUI();
        }
        else if (LongAttackEnemy.longAttackEnemy.playerDis > LongAttackEnemy.longAttackEnemy.enemyMoveInfo.ChaseDis)
        {
            HP.CloseUI();
        }

        if (HP.UIOpened)
        {
            hp_UI.transform.position = MainCamera_New.mainCamera.camera.WorldToScreenPoint(transform.position + Pos_UI);

        }
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
}
