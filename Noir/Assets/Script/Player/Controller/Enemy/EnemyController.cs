using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class EnemyController : MonoBehaviour
{
    public enum EnemyState
    {
        Movement,
        Attack,
        Damage,
        GetDown,
        GetUp,
        Dead,
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
    public EnemyState enemyState;
    private Movestate moveState;
    private AttackState attackState;

    private Animator EnemyAnimator;
   // AnimatorStateInfo animatorStateInfo;
    public static EnemyController enemyController;
    public bool EnemyCanDamage;
    public UI_FollowEnemy HP;
    private ParticleManager particleManager;

 //   public GameObject Enemy_UI;

    private NavMeshAgent EnemyNav;
    IEnumerator damageStopEffect;


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
    public GameObject Attack_R_Collider;
    public GameObject Attack_L_Collider;
    private bool TriggerNextAttack;
    private float AttackProbability;
    /*  private GameObject AttackDown_R_Collider;
      private GameObject AttackSmall_L_Collider;
      private GameObject AttackBig_L_Collider;
      private GameObject AttackDown_L_Collider;*/
    private CapsuleCollider DamageCollider;
    private string preParticle;
    //----------------------Attack----------------
    //--------------------Coroutine---------------
    private IEnumerator ResetStateCoroutine;


    //--------------------Coroutine---------------
    //----------------Audio----------------
    private AudioSource audiosource;
    public AudioClip AudioClip_Damage;

    //----------------Audio----------------

    public GameObject HP_UI;
    private Image hp_UI;
    public Vector3 Pos_UI;

    private void Awake()
    {
        EnemyAnimator = GetComponent<Animator>();
        enemyController = this;
        EnemyNav = GetComponent<NavMeshAgent>();
        DamageCollider = GetComponent<CapsuleCollider>();
        audiosource = GetComponent<AudioSource>();
        particleManager = GetComponent<ParticleManager>();
    
        Attack_L_Collider.SetActive(false);
        Attack_R_Collider.SetActive(false);
        TriggerNextAttack = false;

        // Player = GameObject.FindGameObjectWithTag("Player").gameObject;
    }

    void Start()
    {
        CanChase = true;
        enemyState = EnemyState.Movement;
        ResetStateCoroutine = null;

        hp_UI = Instantiate(HP_UI, GameObject.FindGameObjectWithTag("UI").transform).GetComponent<Image>();
        hp_UI.GetComponent<UI_FollowEnemy>().enemyController = this;
        HP = hp_UI.GetComponent<UI_FollowEnemy>();
        HP.CloseUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerDis <= PlayerChaseDis && !HP.UIOpened) 
        {
            HP.OpenUI();
        }
        else if(PlayerDis > PlayerChaseDis)
        {
            HP.CloseUI();
        }

        if (HP.UIOpened)
        {
            hp_UI.transform.position = MainCamera_New.mainCamera.camera.WorldToScreenPoint(transform.position + Pos_UI);

        }

    }
    private void FixedUpdate()
    {

        PlayerDis = Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, transform.position);

        if (PlayerBehaviour.playerBehaviour.playerState != PlayerBehaviour.PlayerState.Dead && PlayerDis <= PlayerChaseDis) 
        {
            if (PlayerDis <= EnemyNav.stoppingDistance && enemyState == EnemyState.Movement && attackState == AttackState.Defualt)
            {
                AttackProbability = Random.Range(1, 10);
                if (AttackProbability >= 7)
                {
                    Attack(1);
                }

            }

            EnemyMove();
        }
        //Debug.Log(enemyState);
      /*  if (!EnemyAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack") && (Attack_L_Collider.activeInHierarchy == true || Attack_R_Collider.activeInHierarchy == true)) 
        {
            AttackCollider_Close();

        }
        */

    }

    private void EnemyMove()
    {
        EnemyNav.SetDestination(GameObject.FindGameObjectWithTag("Player").transform.position);
        if (enemyState != EnemyState.Movement)
        {
            EnemyNav.isStopped = true;
        }
        else
        {
            EnemyNav.isStopped = false;
        }

        if (EnemyNav.velocity != new Vector3(0, 0, 0))
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
              //  EnemyNav.acceleration = Mathf.Pow(EnemyNav.speed, 2) / (2 * BufferDis);
            }
            moveState = Movestate.Idle;
        }

       
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


    public void Attack(int AttackStateInt)
    {
        switch (AttackStateInt)
        {
            case (int)AttackState.Attack1:
                transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
                EnemyAnimator.SetTrigger("Attack1");
                attackState = AttackState.Attack1;
                Attack_R_Collider.tag = "EnemyAttack_Small";
                break;
            case (int)AttackState.Attack2:             
                if (PlayerisDamage)
                {
               //     Debug.Log("hh");

                    transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
                    Attack_L_Collider.tag = "EnemyAttack_Small";
                    EnemyAnimator.SetTrigger("Attack2");
                    attackState = AttackState.Attack2;
                    TriggerNextAttack = true;
                    PlayerisDamage = false;
                }
                else
                {
                    TriggerNextAttack = false;
                }              
                break;
            case (int)AttackState.Attack3:
                if (PlayerisDamage)
                {
                    transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
                    Attack_L_Collider.tag = "EnemyAttack_Big";
                    EnemyAnimator.SetTrigger("Attack3");
                    attackState = AttackState.Attack3;
                    PlayerisDamage = false;
                    TriggerNextAttack = true;
                }
                else
                {
                    TriggerNextAttack = false;

                }
                break;

        }


    }

    public void Dead()
    {
        enemyState = EnemyState.Dead;
        EnemyAnimator.SetTrigger("Dead");
        AttackCollider_Close();
        DamageCollider.enabled = false;
        
    }

    //--------------------------Aniamtion Event------------------------------------------
    public void EnemyChangToIdle(float WaitTime)
    {
        ResetStateCoroutine = ResetState(WaitTime);
        //StartCoroutine(ResetStateCoroutine);
        
        if (!TriggerNextAttack)
        {
            //Debug.Log("ChangToIdle");

            StartCoroutine(ResetStateCoroutine);
       }
       TriggerNextAttack = false;

        // Debug.Log("ChangToIdle");
    }

    IEnumerator ResetState(float WaitTime)
    {
        yield return new WaitForSeconds(WaitTime);
        enemyState = EnemyState.Movement;
        moveState = Movestate.Idle;
        attackState = AttackState.Defualt;
        
    }

    public void SetAttackState()
    {
        enemyState = EnemyState.Attack;
        if (ResetStateCoroutine != null)
        {
            StopCoroutine(ResetStateCoroutine);
        }
    }


    public void AttackCollider_Open(int AttackState_Number)
    {
        switch (AttackState_Number)
        {
            case (int)AttackState.Attack1:
                Attack_R_Collider.SetActive(true);
          //      Debug.Log("hh");

                break;
            case (int)AttackState.Attack2:
                Attack_L_Collider.SetActive(true);
                break;
            case (int)AttackState.Attack3:
               // Attack_R_Collider.SetActive(true);
                Attack_L_Collider.SetActive(true);
                break;
        }       
    }
    public void AttackCollider_Close()
    {
        Attack_L_Collider.SetActive(false);
        Attack_R_Collider.SetActive(false);
    }

    public void EnemyDamage()
    {
        enemyState = EnemyState.Damage;
        AttackCollider_Close();
        if (ResetStateCoroutine != null)
        {
            StopCoroutine(ResetStateCoroutine);
        }
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


    public void DistroyEnemy()
    {
        StartCoroutine("distroyEnemy");
    }

    IEnumerator distroyEnemy()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
        HP.DestroyUI();
    }

    //--------------------------Aniamtion Event------------------------------------------
    //---------------------------Collider------------------------------------------------
    private void OnTriggerEnter(Collider other)//判斷是否被攻擊
    {
        switch (other.tag)
        {
            case "PlayerAttack_BigSkill":
                EnemyAnimator.SetTrigger("Damage_Big");
                HP.HP -= 100;
                break;
            case "PlayerAttack_Big":
                EnemyAnimator.SetTrigger("Damage_Big");
                HP.HP -= 30;

                break;
            case "PlayerAttack_Small":
                EnemyAnimator.SetTrigger("Damage_Small");
                HP.HP -= 10;

                break;
            case "PlayerLongAttack":
                EnemyAnimator.SetTrigger("Damage_Small");
                HP.HP -= 30;

                break;
            case "Bone":
                EnemyAnimator.SetTrigger("Damage_Big");
                HP.HP -= 30;

                break;
        }
        transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform.position);
        audiosource.clip = AudioClip_Damage;
        audiosource.Play();

      /*  if (other.tag == "PlayerAttack_BigSkill")
        {
            EnemyAnimator.SetTrigger("Damage_Big");
            transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform.position);
            HP.HP -= 100;
            audiosource.clip = AudioClip_Damage;
            audiosource.Play();
        }
        else if (other.tag == "PlayerAttack_Big")
        {           
            EnemyAnimator.SetTrigger("Damage_Big");
            transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform.position);
            HP.HP -= 30;
            audiosource.clip = AudioClip_Damage;
            audiosource.Play();
  
        }
        else if (other.tag == "PlayerAttack_Small")
        {
            EnemyAnimator.SetTrigger("Damage_Small");
            transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform.position);
            HP.HP -= 10;
            audiosource.clip = AudioClip_Damage;
            audiosource.Play();

        }
        else if(other.tag == "PlayerLongAttack")
        {
            EnemyAnimator.SetTrigger("Damage_Small");
            transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform.position);
            HP.HP -= 30;
            audiosource.clip = AudioClip_Damage;
            audiosource.Play();
        }                   
        else*/
        //EnemyCanDamage = false;
    }

    public void DamageFX(int damageState)
    {
        EnemyAnimator.speed = 0.1f;
       
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
        EnemyAnimator.speed = 1f;
    }


    //--------------------------------


}
