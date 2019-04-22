using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController: MonoBehaviour {

    public enum PlayerAnimatorState
    {
        Movement,
        Attack,
        Avoid,
        Damage,
        GetDown,
        GetUp,
        Dead,
        Jump,

    }
    public enum JumpState
    {
        Jump,
        DoubleJump,
        Falling,
    }
    public enum MoveState
    {
        Idle,
        GoForward,
        GoBack,
        GoRight,
        GoLeft,
        GoForwardRight,
        GoForwardLeft,
        GoBackRight,
        GoBackLeft,        
        FastRunForward,
        FastRunRight,
        FastRunLeft,
        
    }
    public enum AttackState
    {
        Default,
        Attack_1,
        Attack_2,
        Attack_3,
        LongAttack,
        BigSkill,
        DashAttack,
    }
    enum AvoidState
    {
        Default,
        Forward,
        Back,
        Right,
        Left,
        ForwardRight,
        ForwardLeft,
        BackRight,
        BackLeft,
    }
    public AttackState attackState;
    public MoveState moveState;
    public JumpState jumpState;
    private AvoidState avoidState;
    public PlayerAnimatorState playerAnimatorState;
    public static PlayerController playerController;

    public GameObject LightAccessoriesUp;
    public GameObject LightAccessoriesDown;
    public Transform LightAccessoriesUpBone;
    public Transform LightAccessoriesDownBone;
    //-----------------------------Attack--------------------
    public GameObject AttackCollider_Small;
    public GameObject AttackCollider_Big;
    private GameObject AttackCollider_BigSkill;
    private GameObject Bullet;
    private Vector3 FixBulletPos;
    private Vector3 BulletStartPos;
    public float LongAttackMaxDis;
    public float LongAttackDis;
   // public float LongAttackFracDistance;
    public float LongAttackSpeed;  //PlayerData 
    public Vector3 LongAttackEndPos;
    public float DashAttack_MaxDis;//PlayerData
    private float DashAttack_NowDis;
    private float DashAttack_FracDis;
    private Quaternion DashAttack_Euler;
    private float DashAttack_StartTime;
    public float DashAttack_Speed;
    private Vector3 DashAttack_Pos;

    public GameObject Weapons;//--
   
    public Transform WeaponsBone;//--
    private Transform Bullet_pos;
    //-----------------------------Attack--------------------
    //-------------------------------- Move------------------
    private float PlayerAnimation_parameter;
    private float MoveSpeed;//Player Data
    private float MaxMoveSpeed;//Player Data
    public float RunSpeed;//Player Data
    private float RunNowSpeed;
    public float FastRunSpeed;//Player Data
    public float ConsumeSp;//Player Data
    public float RecoverSp;
    private float x_direction;
    private float y_direction;
    private float Move_parameter_x;
    private float Move_parameter_y;
    public float RotationSpeed;//Player Data
    public float RotationX;
    private Quaternion rotationEuler;
    public Transform Player_pre_pos;
   // public Transform PlayerHead;
    private bool CanDoubleClick;
    public float DoubleClickTime;//PlayerData
    private bool IsFastRun;
    private bool CanFastRun;
    private float preClickTime;
    private float nextClickTime;
    private Rigidbody rigi;

    public float JumpHeight;
    private int JumpCount;
    public float DoubleJumpHigh;
    private bool debounce;

    //----------------------------------Move-----------------
    private bool Shift_LongPress;
    private bool Shift_Click;
    private float ShiftPressTime;
    private bool CanMove;
    //---------------------------------------Avoid-------------
    public float AvoidSpeed;//Player Data
    public float AvoidDistance;
    public float AvoidMaxDistance;
    private float AvoidRotate;  
    public bool AvoidCanMove;
    private float avoidStartTime;
    private string InputKey_pre;
    private string InputKey_next;
    private float IsAvoidDistance;
    private float FracDistance;
    private Quaternion AvoidEuler;   
    private Vector3 AvoidPosition;
    private Vector3 AvoidColliderPos;
    //private GameObject PlayerCollider;
    //-----------------------------------------Avoid-------------

    //--------------------------------Damage----------------------------------
    public float GetupTime;//PlayerData
    private GameObject DamageObject;
    private CapsuleCollider DamageCollider;
    //--------------------------------Damage----------------------------------

    //-----------------------------Particle------------
    public GameObject ShortAttack1_Object;
    public GameObject ShortAttack2_Object;
    public GameObject ShortAttack3_Object;
    private GameObject LongAttack_Object;
    //private GameObject LongAttackBullet_Object;
    private GameObject BigSkill_Object;
    private GameObject DashAttack_Object;
    public GameObject DoubleJump_Object;

    private ParticleSystem ShortAttack1_Particle;
    private ParticleSystem ShortAttack2_Particle;
    private ParticleSystem ShortAttack3_Particle;
    private ParticleSystem LongAttack_Particle;
    //private ParticleSystem LongAttackBullet_Particle;
    private ParticleSystem BigSkill_Particle;
    private ParticleSystem DashAttack_Particle;
    private ParticleSystem DoubleJump_Particle;
    //-----------------------------Particle------------
    //---------------Audio----------
    private AudioSource audioSource;
    public AudioClip AudioClip_ShortAttack;
    public AudioClip AudioClip_LongAttack;
    public AudioClip AudioClip_Damage;
    //---------------Audio----------

    private bool IsGround;
    //private CapsuleCollider PlayerCollider;
    public CapsuleCollider[] PlayerCollider;

    private int FloorMask;
    public float grounded_dis;

    private int AttackTrigger;     
    private bool CanAttack;
    private IEnumerator ResetStateCoroutine;
    private IEnumerator CancelAttackCoroutine;
    private IEnumerator DoubleClickCoroutine;
    private IEnumerator FastRunCoroutine;
    private IEnumerator GetUpCoroutine;

    public Animator animator;
    AnimatorClipInfo[] AnimatorClipInfo;
    public AnimatorStateInfo AnimatorstateInfo;


   
    private void Awake()
    {
        
        animator = GetComponent<Animator>();
        Player_pre_pos = gameObject.transform.Find("Camare_LookAt");
        ShortAttack1_Object = gameObject.transform.Find("fx_ShortAttack_1").gameObject;
        ShortAttack2_Object = gameObject.transform.Find("fx_ShortAttack_2").gameObject;
        ShortAttack3_Object = gameObject.transform.Find("fx_ShortAttack_3").gameObject;
        LongAttack_Object = gameObject.transform.Find("LongAttack_MagicCircle").gameObject;
        DamageObject = gameObject.transform.Find("DamageCollider").gameObject;
        AttackCollider_BigSkill = gameObject.transform.Find("BigSkillCollider").gameObject;
        //LongAttackBullet_Object = gameObject.transform.Find("fx_LongAttackBullet").gameObject;
        BigSkill_Object = gameObject.transform.Find("fx_AuraMagicCircle").gameObject;
        DashAttack_Object = gameObject.transform.Find("fx_Slash_White").gameObject;
        Bullet_pos=gameObject.transform.Find("Bullet_pos");

        PlayerCollider = GetComponents<CapsuleCollider>();
        DamageCollider = DamageObject.GetComponent<CapsuleCollider>();
        FloorMask = LayerMask.GetMask("Floor");
        AvoidColliderPos = GetComponent<CapsuleCollider>().center;
        //----particle--
        ShortAttack1_Particle = ShortAttack1_Object.GetComponent<ParticleSystem>();
        ShortAttack2_Particle = ShortAttack2_Object.GetComponent<ParticleSystem>();
        ShortAttack3_Particle = ShortAttack3_Object.GetComponent<ParticleSystem>();
        LongAttack_Particle = LongAttack_Object.GetComponent<ParticleSystem>();
       // LongAttackBullet_Particle = LongAttackBullet_Object.GetComponent<ParticleSystem>();
        BigSkill_Particle = BigSkill_Object.GetComponent<ParticleSystem>();
        DashAttack_Particle = DashAttack_Object.GetComponent<ParticleSystem>();
        DoubleJump_Particle = DoubleJump_Object.GetComponent<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
        rigi = GetComponent<Rigidbody>();

        //-----particle---
    }
    // Use this for initialization

    void Start()
    {
       

        Move_parameter_x = 0;
        Move_parameter_x = 0;
        playerController = this;
        AttackTrigger = 0;
        PlayerAnimation_parameter = 0;
        GhostShadow.ghostShadow.gameObject.GetComponent<GhostShadow>().enabled = false;

        debounce = true;
        CanMove = true;
        AttackCollider_Small.SetActive(false);
        AttackCollider_Big.SetActive(false);
        AttackCollider_BigSkill.SetActive(false);
        CanAttack = true;
        CancelAttackCoroutine = null;
        ResetStateCoroutine = null;
        DoubleClickCoroutine = null;
        FastRunCoroutine = null;
        GetUpCoroutine = null;
        AvoidDistance = AvoidMaxDistance;
        FixBulletPos = Bullet_pos.transform.position - transform.position;

        Weapons.transform.parent = WeaponsBone;//--
        LightAccessoriesUp.transform.parent = LightAccessoriesUpBone;
        LightAccessoriesDown.transform.parent = LightAccessoriesDownBone;

        attackState = AttackState.Default;       
        moveState = MoveState.Idle;
        playerAnimatorState = PlayerAnimatorState.Movement;
        avoidState = AvoidState.Default;
        jumpState = JumpState.Jump;
    }
    private void Update()
    {
        for(int i = 0; i < PlayerCollider.Length; i++)
        {
            GetComponents<CapsuleCollider>()[i].height = animator.GetFloat("PlayerColliderHeight");
            AvoidColliderPos.y = animator.GetFloat("PlayerCollider_Pos_Y");
            GetComponents<CapsuleCollider>()[i].center = AvoidColliderPos;
        }
        

    }
     private void FixedUpdate()
     {
        AnimatorClipInfo = animator.GetCurrentAnimatorClipInfo(0);
        AnimatorstateInfo = animator.GetCurrentAnimatorStateInfo(0);      
        IsGround = Physics.Raycast(PlayerCollider[1].bounds.center, -Vector3.up, PlayerCollider[1].bounds.extents.y + grounded_dis, FloorMask);
        Rotaion();
        Movement();
        ShiftIntervel();
        Avoid();
        Attack();
        FastRun();
        Jump(); 
        //Debug.Log(IsGround);
        /* if (Physics.Raycast(PlayerCollider[1].bounds.center, -Vector3.up, PlayerCollider[1].bounds.extents.y + grounded_dis, FloorMask))
        {
            ShiftIntervel();//
            Avoid();//
            Attack();//
          //  rigi.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
            Debug.Log("Ground");

            if (playerAnimatorState == PlayerAnimatorState.Movement)
            {
               
                
                FastRun();//
                Jump();
                
            }
        }*/


        if (!IsFastRun && playerAnimatorState != PlayerAnimatorState.Avoid && UI_HP.Ui_HP.SP < UI_HP.Ui_HP.SP_Max) 
        {
            UI_HP.Ui_HP.SP += RecoverSp;
            UI_HP.Ui_HP.ConsumeSP();
        }

        AnimatorStateControll();


        Debug.DrawLine(PlayerCollider[1].bounds.center, new Vector3(PlayerCollider[1].bounds.center.x, PlayerCollider[1].bounds.center.y -(PlayerCollider[1].bounds.extents.y + grounded_dis), PlayerCollider[1].bounds.center.z), Color.red);
       
    }
    private void AnimatorStateControll()
    {       
        if (playerAnimatorState != PlayerAnimatorState.Movement)
        {
            Move_parameter_x = Mathf.Lerp(Move_parameter_x, 0, 0.15f);
            Move_parameter_y = Mathf.Lerp(Move_parameter_y, 0, 0.15f);
            animator.SetFloat("RunSpeed_Horizontal", Move_parameter_x);
            animator.SetFloat("RunSpeed_Vertical", Move_parameter_y);
        }
      /*  if (playerAnimatorState == PlayerAnimatorState.Attack) 
        {
            if (attackState == AttackState.DashAttack)
            {
                animator.applyRootMotion = false;
            }
            else
            {
                animator.applyRootMotion = true;
            }
        }
        else
        {
            animator.applyRootMotion = false;
        }*/
       /* Debug.Log(playerAnimatorState);*/
       
      //  Debug.Log(attackState);

    }

    //--------------------------Attack---------------------------------   
    #region Attack
    private void Attack()
    {
       if(playerAnimatorState == PlayerAnimatorState.Movement || playerAnimatorState == PlayerAnimatorState.Attack || playerAnimatorState == PlayerAnimatorState.Avoid && IsGround) 
       {

            if (Input.GetKeyDown(KeyCode.Q) && attackState == AttackState.Default/* && UI_HP.Ui_HP.MP >= 30*/)  
            {
                if (CanAttack)
                {
                   // UI_HP.Ui_HP.MP -= 30;
                    attackState = AttackState.BigSkill;
                    AttackTrigger += 1;
                    CanAttack = false;
                }
            }
            else if (Input.GetMouseButtonDown(0))//按下滑鼠左鍵
            {
               
                switch (attackState)//用現現階段的狀態來判斷下一擊要觸發哪一個攻擊
                {
                    case AttackState.Default://假如現階段為Default

                        if (CanAttack)//判斷可以觸發下一階段的時機
                        {
                            if (IsFastRun)
                            {
                                switch (moveState)
                                {
                                    case MoveState.FastRunForward:
                                        DashAttack_Euler = Quaternion.Euler(0, RotationX, 0);
                                        DashAttack_Pos = DashAttack_Euler * new Vector3(0, 0, DashAttack_MaxDis) + transform.position;
                                        break;
                                    case MoveState.FastRunLeft:
                                        DashAttack_Euler = Quaternion.Euler(0, RotationX - 45, 0);
                                        DashAttack_Pos = DashAttack_Euler * new Vector3(0, 0, DashAttack_MaxDis) + transform.position;
                                        break;
                                    case MoveState.FastRunRight:
                                        DashAttack_Euler = Quaternion.Euler(0, RotationX + 45, 0);
                                        DashAttack_Pos = DashAttack_Euler * new Vector3(0, 0, DashAttack_MaxDis) + transform.position;
                                        break;

                                }

                                DashAttack_StartTime = Time.time;
                                attackState = AttackState.DashAttack;
                                AttackTrigger += 1;
                                CanAttack = false;
                               
                            }
                            else
                            {
                                attackState = AttackState.Attack_1;//則觸發後的下一階段為Attack_1,
                                AttackTrigger += 1;//用來限定Aniamtor.trigger只能觸發一次
                                CanAttack = false;//用來限定當攻擊的動畫還沒播完則不能觸發下一階段攻擊,需搭配Animation Event
                                
                            }
                            
                        }
                        break;
                    case AttackState.Attack_1:
                        if (CanAttack)
                        {
                            
                            attackState = AttackState.Attack_2;
                            AttackTrigger += 1;
                            CanAttack = false;
                        }
                        break;
                    case AttackState.Attack_2:
                        if (CanAttack)
                        {
                            attackState = AttackState.Attack_3;
                            AttackTrigger += 1;
                            CanAttack = false;
                        }
                        /*  Debug.Log(AttackTrigger);*/
                        break;
                }
            }
            else if (Input.GetMouseButtonDown(1))
            {
                if (CanAttack && attackState != AttackState.BigSkill /*&& UI_HP.Ui_HP.MP >= 5*/ && attackState != AttackState.LongAttack)  //--
                {                                    
                    attackState = AttackState.LongAttack;
                    AttackTrigger += 1;
                    CanAttack = false;
                                      
                }

            }


       }        
        AttackAnimation();       
    }

    private void AttackAnimation()//觸發攻擊動畫
    {
        if (AttackTrigger == 1) //動畫只需觸發一次,因為我是把這個放在Update裡面,會每一偵觸發
        {
            switch (attackState)
            {
                case AttackState.Attack_1:  //當狀態為Attack_1                 
                    animator.SetTrigger("Attack1");  //觸發動畫                  
                    animator.ResetTrigger("Attack3");
                    //為了防上一個狀態的Animator.Trigger多觸發一次的bug所以在執行下一個動作時重置上一動作的Animator.Trigger
                           
                    AttackTrigger = 0; //在觸發動畫過後將 AttackTrigger重置,即可依下一狀態判斷來觸發相對應的動畫                                     
                    break;
                case AttackState.Attack_2:                   
                    animator.SetTrigger("Attack2");
                    // ShortAttack2_Particle.Play();
                    
                    AttackTrigger = 0;                   
                    break;
                case AttackState.Attack_3:                   
                    animator.SetTrigger("Attack3");
                    animator.ResetTrigger("Attack1");
                    animator.ResetTrigger("Attack2");
                   
                    //ShortAttack3_Particle.Play();
                    AttackTrigger = 0;                    
                    break;
                case AttackState.LongAttack:                   
                    animator.SetTrigger("LongAttack");
                    //  LongAttack_Particle.Play();
                   
                    AttackTrigger = 0;                  
                    break;
                case AttackState.BigSkill:                   
                    animator.SetTrigger("BigSkill");
                    AttackTrigger = 0;
                    break;
                case AttackState.DashAttack:
                    CanMove = true;
                    animator.SetTrigger("DashAttack");
                    AttackTrigger = 0;
                    
                    break;
            }
            
        }      
        if (attackState == AttackState.Default)
        {
            CanAttack = true;
            AttackTrigger = 0;
        }

       
        DashAttackMove();
       
    }
   
  /*  private void LongAttackGetObj()
    {           
        Bullet = ObjectPool.objectPool.LongAttackObj();

        if (Bullet == null) return;

        
        BulletStartPos = transform.position + rotationEuler * FixBulletPos;
        
        Bullet.transform.position = BulletStartPos;
        Bullet.transform.rotation = Quaternion.LookRotation(MainCamera.mainCamera.GetAimTarget());
                      
        if (MainCamera.mainCamera.longAttackRaycastHitSomeThing && MainCamera.mainCamera.RayHitPoint.transform.CompareTag("Wall") && MainCamera.mainCamera.RayHitPoint.transform.CompareTag("Floor") && MainCamera.mainCamera.RayHitPoint.transform.CompareTag("Enemy"))
        {
            LongAttackEndPos = MainCamera.mainCamera.GetAimTarget();
           // Debug.Log(MainCamera.mainCamera.RayHitPoint.transform.name);
                       
        }
        else
        {
            LongAttackEndPos = Quaternion.LookRotation(MainCamera.mainCamera.GetAimTarget()) * new Vector3(1, 0, LongAttackMaxDis) + Bullet.transform.position;
            
        }
        LongAttackDis = Vector3.Distance(BulletStartPos, LongAttackEndPos) * 3;
        LongAttackDis = Mathf.Clamp(LongAttackDis,0, LongAttackMaxDis);
        Bullet.SetActive(true);
    }
    */
    


    private void DashAttackMove()
    {
        if(attackState == AttackState.DashAttack && CanMove)
        {
            DashAttack_NowDis = (Time.time - DashAttack_StartTime) * DashAttack_Speed;
            DashAttack_FracDis = DashAttack_NowDis / DashAttack_MaxDis;
            DashAttack_FracDis = Mathf.Clamp(DashAttack_FracDis, 0, 1);
            transform.position = Vector3.Lerp(transform.position, DashAttack_Pos, DashAttack_FracDis);
            //Debug.Log(CanMove);
        }
        else
        {
            DashAttack_NowDis = 0;
            DashAttack_FracDis = 0;
        }
       
    }
    #endregion
    //--------------------------Attack---------------------------------      

    //--------------------------Move---------------------------------   
    #region 移動 
    private void Rotaion()
    { 
        RotationX += Input.GetAxis("Mouse X") * Time.deltaTime * RotationSpeed;

        if (RotationX > 360)
        {
            RotationX -= 360;
        }
        else if (RotationX < 0)
        {
            RotationX += 360;
        }
        rotationEuler = Quaternion.Euler(0, RotationX, 0);
        transform.rotation = rotationEuler;
        
    }
    private void Movement()
    {
        if (playerAnimatorState == PlayerAnimatorState.Movement || playerAnimatorState == PlayerAnimatorState.Jump)
        {
            if (IsFastRun)
            {
                MaxMoveSpeed = FastRunSpeed;
                UI_HP.Ui_HP.SP -= ConsumeSp;
                UI_HP.Ui_HP.ConsumeSP();

            }
            else
            {
                MaxMoveSpeed = RunSpeed;
            }
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                MoveSpeed = Mathf.Lerp(MoveSpeed, MaxMoveSpeed, 0.03f);
                MoveSpeed = Mathf.Clamp(MoveSpeed, 0, MaxMoveSpeed);
            }
            else if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
            {
                MoveSpeed = Mathf.Lerp(MoveSpeed, 0, 0.1f);
                if (MoveSpeed <= 0.06f && MoveSpeed >= -0.06f)
                {
                    MoveSpeed = 0;
                }
            }

            if (Input.GetAxis("Horizontal") == 0 || Input.GetAxis("Vertical") == 0)
            {
                RunNowSpeed = Mathf.Sqrt((Mathf.Pow(MoveSpeed, 2) * 2));
            }
            else
            {
                RunNowSpeed = MoveSpeed;
            }
            float MoveX = Input.GetAxis("Horizontal") * Time.deltaTime * RunNowSpeed;
            float MoveZ = Input.GetAxis("Vertical") * Time.deltaTime * RunNowSpeed;

            //Debug.Log(Input.GetAxis("Horizontal"));
            transform.Translate(MoveX, 0, MoveZ);

            MovementAnimaionControl();

        }
        
    }
    
    private void FastRun()
    {        
        if (Shift_LongPress && Input.GetKey(KeyCode.W) && UI_HP.Ui_HP.SP > 0 && IsGround && playerAnimatorState == PlayerAnimatorState.Movement)  
        {
            IsFastRun = true;
            if (Input.GetKey(KeyCode.A) && Input.GetAxis("Horizontal") < 0)
            {
                moveState = MoveState.FastRunLeft;

            }
            else if (Input.GetKey(KeyCode.D) && Input.GetAxis("Horizontal") > 0)
            {
                moveState = MoveState.FastRunRight;
            }           
            else if (Input.GetAxis("Vertical") > 0) 
            {
                moveState = MoveState.FastRunForward;
            }
            // Debug.Log("a");
           
        }

        if ( (Input.GetKeyUp(KeyCode.LeftShift) && !Shift_LongPress) || Input.GetKeyUp(KeyCode.W) || UI_HP.Ui_HP.SP <= 0)  
        {
            moveState = MoveState.Idle;
            IsFastRun = false;
            
        }
        /*Debug.Log(moveState);*/
        
    }
    

    private void MovementAnimaionControl()
    {
        //  playerAnimatorState = PlayerAnimatorState.Movement;
        if (!IsFastRun)
        {         
            if (Input.GetKey(KeyCode.W) && Input.GetAxis("Vertical") > 0)  
            {                
                if (Input.GetKey(KeyCode.A) && Input.GetAxis("Horizontal") < 0)   
                {
                    moveState = MoveState.GoForwardLeft;                                       
                }
                else if (Input.GetKey(KeyCode.D) && Input.GetAxis("Horizontal") > 0)   
                {                                        
                    moveState = MoveState.GoForwardRight;                   
                }
                else
                {
                    moveState = MoveState.GoForward;
                }
            }
            else if (Input.GetKey(KeyCode.S) && Input.GetAxis("Vertical") < 0) 
            {
                if (Input.GetKey(KeyCode.A) && Input.GetAxis("Horizontal") < 0)
                {
                    moveState = MoveState.GoBackLeft;
                }
                else if (Input.GetKey(KeyCode.D) && Input.GetAxis("Horizontal") > 0)
                {
                    moveState = MoveState.GoBackRight;
                }
                else
                {
                    moveState = MoveState.GoBack;
                }
            }
            else if (Input.GetKey(KeyCode.A) && Input.GetAxis("Horizontal") < 0)
            {
                moveState = MoveState.GoLeft;
            }
            else if(Input.GetKey(KeyCode.D) && Input.GetAxis("Horizontal") > 0)
            {
                moveState = MoveState.GoRight;
            }
            else
            {
                moveState = MoveState.Idle;
            }
        }
       
        switch (moveState)
        {          
            case MoveState.Idle:          
                x_direction = 0;
                y_direction = 0;
                break;

            case MoveState.GoRight:            
                x_direction = 1;
                y_direction = 0;
                break;

            case MoveState.GoLeft:            
                x_direction = -1;
                y_direction = 0;
                break;

            case MoveState.GoForward:           
                x_direction = 0;
                y_direction = 1;
                break;
            case MoveState.GoBack:          
                x_direction = 0;
                y_direction = -1;
                break;
            case MoveState.GoForwardRight:         
                x_direction = 1;
                y_direction = 1;
                break;
            case MoveState.GoForwardLeft:            
                x_direction = -1;
                y_direction = 1;
                break;
            case MoveState.GoBackLeft:               
                x_direction = -1;
                y_direction = -1;
                break;
            case MoveState.GoBackRight:            
                x_direction = 1;
                y_direction = -1;
                break;
            case MoveState.FastRunForward:
                x_direction = 0;
                y_direction = 2;
                break;
            case MoveState.FastRunLeft:
                x_direction = -2;
                y_direction = 2;
                break;
            case MoveState.FastRunRight:
                x_direction = 2;
                y_direction = 2;
                break;
        }

        Move_parameter_x = Mathf.Lerp(Move_parameter_x, x_direction, 0.1f);
        Move_parameter_y = Mathf.Lerp(Move_parameter_y, y_direction, 0.1f);      
        
        if (Move_parameter_x <= 0.06f && Move_parameter_x >= -0.06f)
        {
            Move_parameter_x = 0;
        }
        if (Move_parameter_y <= 0.06f && Move_parameter_y >= -0.06f)
        {
            Move_parameter_y = 0;
        }

        Move_parameter_x = Mathf.Clamp(Move_parameter_x, -2, 2);
        Move_parameter_y = Mathf.Clamp(Move_parameter_y, -2, 2);

        animator.SetFloat("RunSpeed_Horizontal", Move_parameter_x);
        animator.SetFloat("RunSpeed_Vertical", Move_parameter_y);

        //Debug.Log(moveState);
    }

    #endregion
    //--------------------------Move---------------------------------   
    //--------------------------Jump---------------------------------  
    private void Jump()
    {
       
        if (IsGround)
        {
            
            if (playerAnimatorState==PlayerAnimatorState.Jump)
            {
                playerAnimatorState = PlayerAnimatorState.Movement;
                jumpState = JumpState.Jump;
                moveState = MoveState.Idle;
                
                animator.SetTrigger("Idle");
             //   Debug.Log(moveState);
            }
            JumpCount = 0;
        }
        

        
        if (Input.GetKeyDown(KeyCode.Space) && (playerAnimatorState == PlayerAnimatorState.Movement || playerAnimatorState == PlayerAnimatorState.Jump)) 
        {
            if (IsGround && debounce)
            {
                rigi.AddForce(0, JumpHeight, 0, ForceMode.Impulse);
                animator.SetTrigger("Jump");
                playerAnimatorState = PlayerAnimatorState.Jump;
                jumpState = JumpState.Jump;
                StartCoroutine("Debounce");
            }
            else if (JumpCount == 0 && !IsGround && debounce)
            {
                JumpCount-=1;
                rigi.mass = 10;
                rigi.velocity = Vector3.zero;
                rigi.AddForce(0, DoubleJumpHigh, 0, ForceMode.Impulse);
                rigi.mass = 800;
                animator.SetTrigger("DoubleJump");
                playerAnimatorState = PlayerAnimatorState.Jump;
                jumpState = JumpState.DoubleJump;
                
            }
            
        }
       
    }

    public void StateChangeToFalling()
    {
        playerAnimatorState = PlayerAnimatorState.Jump;
        jumpState = JumpState.Falling;
        animator.ResetTrigger("Idle");
    }

    IEnumerator Debounce()
    {
        debounce = false;
        yield return new WaitForSeconds(0.1f);
        debounce = true;
    }

   
    //--------------------------Jump---------------------------------

    private void ShiftIntervel()
    {
        if (IsGround)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                ShiftPressTime = Time.time;
            }

            if (Input.GetKeyUp(KeyCode.LeftShift) && Time.time - ShiftPressTime < 0.3f)
            {
                Shift_Click = true;
                Shift_LongPress = false;
                return;
            }
            else if (Input.GetKey(KeyCode.LeftShift) && Time.time - ShiftPressTime > 0.3f)
            {
                Shift_Click = false;
                Shift_LongPress = true;

                return;
            }
            if (!Input.GetKey(KeyCode.LeftShift) && (Shift_Click || Shift_LongPress))
            {
                Shift_Click = false;
                Shift_LongPress = false;
            }


        }



    }
    //--------------------------Avoid---------------------------------   
    #region 迴避
    private void Avoid()
    {
        
        if (playerAnimatorState == PlayerAnimatorState.Movement || playerAnimatorState == PlayerAnimatorState.Attack && attackState != AttackState.BigSkill && UI_HP.Ui_HP.SP >= 20 && IsGround)    
        {
            
            if (Shift_Click)
            {
        //        Debug.Log("hh");

                if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W))
                {
                    AvoidStateSelect(AvoidState.ForwardRight);
                }
                else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W))
                {                    
                    AvoidStateSelect(AvoidState.ForwardLeft);
                }
                else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S))
                {                   
                    AvoidStateSelect(AvoidState.BackLeft);
                }
                else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S))
                {                  
                    AvoidStateSelect(AvoidState.BackRight);
                }               
                else if (Input.GetKey(KeyCode.W))
                {                 
                    AvoidStateSelect(AvoidState.Forward);
                    
                }
                else if (Input.GetKey(KeyCode.S)) 
                {
                    AvoidStateSelect(AvoidState.Back);
                }
                else if (Input.GetKey(KeyCode.A))
                {                 
                    AvoidStateSelect(AvoidState.Left);
                }
                else if (Input.GetKey(KeyCode.D))
                {                 
                    AvoidStateSelect(AvoidState.Right);
                }
               
            }
            if (Input.GetKey(KeyCode.W))
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    Avoid_DoubleClickFuntion(AvoidState.ForwardLeft, Input.inputString);
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    Avoid_DoubleClickFuntion(AvoidState.ForwardRight, Input.inputString);
                }
            }
            else if (Input.GetKey(KeyCode.S))
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    Avoid_DoubleClickFuntion(AvoidState.BackLeft, Input.inputString);
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    Avoid_DoubleClickFuntion(AvoidState.BackRight, Input.inputString);
                }

            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                Avoid_DoubleClickFuntion(AvoidState.Forward, Input.inputString);

                
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                Avoid_DoubleClickFuntion(AvoidState.Back, Input.inputString);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                Avoid_DoubleClickFuntion(AvoidState.Left, Input.inputString);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                Avoid_DoubleClickFuntion(AvoidState.Right, Input.inputString);
            }
            
            
            //Debug.Log(AvoidPosition);
        }

      
        Debug.DrawLine(transform.position, AvoidPosition, Color.red);
       
        AvoidMovement();    
    }
    private void Avoid_DoubleClickFuntion(AvoidState avoidState,string InputKey)
    {
        if (CanDoubleClick)
        {
            InputKey_next = InputKey;
            nextClickTime = Time.time;
            if (nextClickTime - preClickTime > 0.1f && InputKey_next == InputKey_pre) 
            {
                StopCoroutine(DoubleClickCoroutine);
                CanDoubleClick = false;
                /* moveState = MoveState.FastRunForward;
                 IsFastRun = true;*/
                AvoidStateSelect(avoidState);
            }
        }
        else
        {
            InputKey_pre = InputKey;
            preClickTime = Time.time;
            CanDoubleClick = true;
            DoubleClickCoroutine = DoubleClick(DoubleClickTime);
            StartCoroutine(DoubleClickCoroutine);
        }
    }
    IEnumerator DoubleClick(float WaitTime)
    {
        yield return new WaitForSeconds(WaitTime);
        CanDoubleClick = false;
        
    }
    private void AvoidStateSelect(AvoidState InputAvoidState)
    {
        UI_HP.Ui_HP.SP -= UI_HP.Ui_HP.SP_Max/UI_HP.Ui_HP.SP_Light.Length;
        UI_HP.Ui_HP.ConsumeSP();
        CanMove = true;
        playerAnimatorState = PlayerAnimatorState.Avoid;
        DamageObject.SetActive(false);
        avoidStartTime = Time.time;
        switch (InputAvoidState)
        {
            case AvoidState.Forward:
                avoidState = InputAvoidState;
                animator.SetTrigger("Avoid_Forward");
                AvoidRotate = 0;                
                break;
            case AvoidState.Back:              
                avoidState = AvoidState.Back;
                animator.SetTrigger("Avoid_Back");
                AvoidRotate = 180;
                break;
            case AvoidState.Left:             
                avoidState = AvoidState.Left;
                animator.SetTrigger("Avoid_Left");
                AvoidRotate = -90;
                break;
            case AvoidState.Right:            
                avoidState = AvoidState.Right;
                animator.SetTrigger("Avoid_Right");
                AvoidRotate = 90;
                break;
            case AvoidState.ForwardLeft:          
                avoidState = AvoidState.ForwardLeft;
                animator.SetTrigger("Avoid_ForwardLeft");
                AvoidRotate = -45;
                break;
            case AvoidState.ForwardRight:
                avoidState = AvoidState.ForwardRight;
                animator.SetTrigger("Avoid_ForwardRight");
                AvoidRotate = 45;
                break;
            case AvoidState.BackLeft:       
                avoidState = AvoidState.BackLeft;
                animator.SetTrigger("Avoid_BackLeft");
                AvoidRotate = -135;
                break;
            case AvoidState.BackRight:        
                avoidState = AvoidState.BackRight;
                animator.SetTrigger("Avoid_BackRight");
                AvoidRotate = 135;
                break;

        }
        AvoidEuler = Quaternion.Euler(0, RotationX + AvoidRotate, 0);
        AvoidPosition = AvoidEuler * new Vector3(0, 0, AvoidDistance) + transform.position;
    }

    private void AvoidMovement()
    {
       
        if (playerAnimatorState == PlayerAnimatorState.Avoid && CanMove/*AvoidCanMove*/)
        {

            IsAvoidDistance = (Time.time - avoidStartTime) * AvoidSpeed;
            FracDistance = IsAvoidDistance / AvoidDistance;           
            FracDistance = Mathf.Clamp(FracDistance, 0, 1);
            transform.position = Vector3.Lerp(transform.position, AvoidPosition, FracDistance);
           // Debug.Log(FracDistance);
           // Debug.Log(AvoidPosition);
        }
        else
        {
            IsAvoidDistance = 0;
            FracDistance = 0;
            
            
        }
        
        
    }
    #endregion
    //--------------------------Avoid---------------------------------   
    //--------------------------Dead----------------------------------

    public void Dead()
    {
        if (playerAnimatorState != PlayerAnimatorState.Dead)
        {
            animator.SetTrigger("Dead");
            playerAnimatorState = PlayerAnimatorState.Dead;
            
        }
       
    }
    public void GameOver()
    {
        Time.timeScale = 0;
    }

    //--------------------------Dead----------------------------------
    //-------------------------Animatioｎ　Event--------------------******

    //------------Attack,以下為掛在Animation Event上的Function-----------

    public void TriggerCancelAttack(float WaitTime) //當攻擊動畫播完後,則重置攻擊狀態(掛在攻擊動畫的最後)
    {      
        CancelAttackCoroutine = CancelAttack(WaitTime);
        StartCoroutine(CancelAttackCoroutine);//執行 IEnumerator CancelAttack(float WaitTime) 

    }

    IEnumerator CancelAttack(float WaitTime) 
    {
        yield return new WaitForSeconds(WaitTime);//當過了WaitTime秒後則重置現階段狀態
        attackState = AttackState.Default;        
    }

    public void CanTriggerAttack() 
        //用來判斷哪時可以開始觸發下一階段,掛的位置需搭配每一個動畫之間的Exit Time來配置(數動畫的fram會比較精確)
    {        
        CanAttack = true;//可以觸發下一階段
        playerAnimatorState = PlayerAnimatorState.Attack;
        //PlayerAnimatorState用來判斷角色的動作(Movement,Attack,Jump,Avoid...)
        //,再宣告一個attackState來記錄Attack的狀態(Default,Attack1,Attack2...)
        if (ResetStateCoroutine != null)
        {
            StopCoroutine(ResetStateCoroutine);  //停止將狀態轉為Idle的Coroutine  
        }
        if (CancelAttackCoroutine != null)
        {
            StopCoroutine(CancelAttackCoroutine); //停止將攻擊狀態轉為Default的Coroutine  
        }
        StopCoroutine("CancelAttackNow");
       
    }
    public void FirstAttackState()
    {

        playerAnimatorState = PlayerAnimatorState.Attack;
    }

    public void ChangeToIdle(float WaitTime) //在動畫結束之後則將狀態轉為Default及idle//-----Attack,Avoid,Damage-----
    {
        ResetStateCoroutine = ResetState(WaitTime);

        // Debug.Log("attack");
        if (AnimatorstateInfo.IsTag("Avoid") && attackState != AttackState.Default)//當迴避及攻擊的動作結束之後才觸發ResetState
        {

            return;
        }
        else
        {
            StartCoroutine(ResetStateCoroutine);
        }

    }

    IEnumerator ResetState(float WaitTime)
    {
        yield return new WaitForSeconds(WaitTime);
        playerAnimatorState = PlayerAnimatorState.Movement;
        avoidState = AvoidState.Default;
        GhostShadow.ghostShadow.gameObject.GetComponent<GhostShadow>().enabled = false;
        IsFastRun = false;
        DamageObject.SetActive(true);

    }

    public void CancelAttackNow() //-----Avoid-----
    {
        CanAttack = false;
        attackState = AttackState.Default;

        animator.ResetTrigger("Attack3");
        animator.ResetTrigger("Attack2");
        animator.ResetTrigger("Attack1");
        if (ResetStateCoroutine != null)
        {
            StopCoroutine(ResetStateCoroutine);
        }
    }
    //-------------Audio------------
    public void ShortAttackAudioPlay()
    {
        audioSource.clip = AudioClip_ShortAttack;
        audioSource.Play();
    }
    public void LongAttackAudioPlay()
    {
        audioSource.clip = AudioClip_LongAttack;
        audioSource.Play();
    }
    public void DamageAudioPlay()
    {
        audioSource.clip = AudioClip_Damage;
        audioSource.Play();
    }
    //-------------Audio------------
    public void ParticleTrigger()
    {
        if (jumpState == JumpState.DoubleJump)
        {
            DoubleJump_Particle.Stop();
            DoubleJump_Particle.Play();
        }
        switch (attackState)
        {
            case AttackState.Attack_1:
                ShortAttack1_Particle.Stop();
                ShortAttack1_Particle.Play();               
                break;
            case AttackState.Attack_2:
                ShortAttack2_Particle.Stop();
                ShortAttack2_Particle.Play();
                break;
            case AttackState.Attack_3:
                ShortAttack3_Particle.Stop();
                ShortAttack3_Particle.Play();
                break;
            case AttackState.LongAttack:
                
                LongAttack_Particle.Stop();
               // LongAttackBullet_Particle.Stop();
                LongAttack_Particle.Play();
               // LongAttackBullet_Particle.Play();
            //    LongAttackGetObj();
                break;
            case AttackState.BigSkill:
               
                BigSkill_Particle.Stop();
                BigSkill_Particle.Play();
                break;
            case AttackState.DashAttack:
                DashAttack_Particle.Stop();
                DashAttack_Particle.Play();
                break;
            

        }
    }

    

    public void ChangeAvoidState()
    {
        playerAnimatorState = PlayerAnimatorState.Avoid;
        AttackColliderClose();
        ShortAttack1_Particle.Stop();
        ShortAttack2_Particle.Stop();
        ShortAttack3_Particle.Stop();
        LongAttack_Particle.Stop();
        if (avoidState == AvoidState.Back || avoidState == AvoidState.BackLeft || avoidState == AvoidState.BackRight)
        {
            GhostShadow.ghostShadow.gameObject.GetComponent<GhostShadow>().enabled = true;
        }
    }

    public void AttackColliderOpen_Small() 
    {
        AttackCollider_Small.SetActive(true);
      //  EnemyController.enemyController.EnemyCanDamage = true;
    }

    public void AttackColliderOpen_Big()
    {
        AttackCollider_Big.SetActive(true);
     //   EnemyController.enemyController.EnemyCanDamage = true;
    }
    public void AttackColliderOpen_BigSkill()
    {
        AttackCollider_BigSkill.SetActive(true);
       // EnemyController.enemyController.EnemyCanDamage = true;
    }
    public void AttackColliderClose()
    {
        AttackCollider_Small.SetActive(false);
        AttackCollider_Big.SetActive(false);
        AttackCollider_BigSkill.SetActive(false);
    }
    //------------Attack-----------

    //------------Damage-----------
    public void GetUp()
    {
        GetUpCoroutine = TriggerGetUp(GetupTime);
        StartCoroutine(GetUpCoroutine);
    }
    IEnumerator TriggerGetUp(float GetUpWaitTime)
    {
        yield return new WaitForSeconds(GetUpWaitTime);
        animator.SetTrigger("GettingUp");
    }
    //------------Damage-----------
    //------------FastRun-----------
    /*  public void TriggerFastRun(float time)
      {
          if (FastRunCoroutine != null)
          {
              StopCoroutine(FastRunCoroutine);
          }
          CanFastRun = true;

          FastRunCoroutine = FastRunTimeInterval(time);
          StartCoroutine(FastRunCoroutine);

      }
      IEnumerator FastRunTimeInterval(float time)
      {

          yield return new WaitForSeconds(time);
          CanFastRun = false;
      }*/
    //------------FastRun-----------

    //-------------------------Animatioｎ　Event--------------------******

    //----------------------------Trigger---------------------------------

    private void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "Wall" ||　collider.tag=="Enemy")
        {
            /*  AvoidCanMove = false;*/
            CanMove = false;
            
        }
    }

    //----------------------------Trigger---------------------------------
}
