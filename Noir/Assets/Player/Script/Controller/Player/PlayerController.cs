using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public enum PlayerAnimatorState
    {
        Movement,
        Attack,
        Avoid,
        Damage,
        GetDown,
        GetUp,
        Dead,
    }
    enum MoveState
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
    enum AttackState
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
    private AttackState attackState;
    private MoveState moveState;
    private AvoidState avoidState;
    public PlayerAnimatorState playerAnimatorState;
    public static PlayerController playerController;
    
    //-----------------------------Attack--------------------
    public GameObject AttackCollider_Small;
    public GameObject AttackCollider_Big;
    private GameObject AttackCollider_BigSkill;
    private GameObject Bullet;
    private Vector3 FixBulletPos;
    private Vector3 BulletStartPos;
    public float LongAttackMaxDis;
   // private float LongAttackNowDis;
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
    private GameObject LongAttackBullet_Object;
    private GameObject BigSkill_Object;
    private GameObject DashAttack_Object;

    private ParticleSystem ShortAttack1_Particle;
    private ParticleSystem ShortAttack2_Particle;
    private ParticleSystem ShortAttack3_Particle;
    private ParticleSystem LongAttack_Particle;
    private ParticleSystem LongAttackBullet_Particle;
    private ParticleSystem BigSkill_Particle;
    private ParticleSystem DashAttack_Particle;
    //-----------------------------Particle------------

    //private CapsuleCollider PlayerCollider;
    private CapsuleCollider[] PlayerCollider;

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
    AnimatorStateInfo AnimatorstateInfo;

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
        LongAttackBullet_Object = gameObject.transform.Find("fx_LongAttackBullet").gameObject;
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
        LongAttackBullet_Particle = LongAttackBullet_Object.GetComponent<ParticleSystem>();
        BigSkill_Particle = BigSkill_Object.GetComponent<ParticleSystem>();
        DashAttack_Particle = DashAttack_Object.GetComponent<ParticleSystem>();
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
        FixBulletPos = ObjectPool.objectPool.LongAttack.transform.position - transform.position;

        Weapons.transform.parent = WeaponsBone;//--


        attackState = AttackState.Default;       
        moveState = MoveState.Idle;
        playerAnimatorState = PlayerAnimatorState.Movement;
        avoidState = AvoidState.Default;
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
        Rotaion();
        
        if (Physics.Raycast(transform.position+new Vector3(0,0.01f,0), -Vector3.up, PlayerCollider[0].bounds.extents.y - (PlayerCollider[0].bounds.extents.y - grounded_dis), FloorMask))
        {
            ShiftIntervel();
            Avoid();
            Attack();
            rigi.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
            //Debug.Log(avoidState);

            if (playerAnimatorState == PlayerAnimatorState.Movement)
            {
               
                Movement();
                FastRun();
            }
        }

        AnimatorStateControll();


        Debug.DrawLine(transform.position + new Vector3(0, 0.01f, 0), new Vector3(transform.position.x, transform.position.y  - (PlayerCollider[0].bounds.extents.y - (PlayerCollider[0].bounds.extents.y - grounded_dis)), transform.position.z), Color.red);
       
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
        if (playerAnimatorState == PlayerAnimatorState.Attack) 
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
        }
       /* Debug.Log(playerAnimatorState);*/
       
      //  Debug.Log(attackState);

    }

//--------------------------Attack---------------------------------   
    private void Attack()
    {
       if(playerAnimatorState == PlayerAnimatorState.Movement || playerAnimatorState == PlayerAnimatorState.Attack || playerAnimatorState==PlayerAnimatorState.Avoid)
        {

            if (Input.GetKeyDown(KeyCode.Q) && attackState == AttackState.Default && UI_HP.Ui_HP.MP >= 30)  
            {
                if (CanAttack)
                {
                    UI_HP.Ui_HP.MP -= 30;
                    attackState = AttackState.BigSkill;
                    AttackTrigger += 1;
                    CanAttack = false;
                }
            }
            else if (Input.GetMouseButtonDown(0))
            {
               
                switch (attackState)
                {
                    case AttackState.Default:
                        if (CanAttack)
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
                                attackState = AttackState.Attack_1;
                                AttackTrigger += 1;
                                CanAttack = false;
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
                //playerAnimatorState = PlayerAnimatorState.Attack;               
                if (CanAttack && attackState != AttackState.BigSkill && UI_HP.Ui_HP.MP >= 5)  //--
                {
                    UI_HP.Ui_HP.MP -= 10;
                    attackState = AttackState.LongAttack;
                    AttackTrigger += 1;
                    CanAttack = false;
                }

            }


        }        
        AttackAnimation();       
    }

    private void AttackAnimation()
    {
        if (AttackTrigger == 1) 
        {
            switch (attackState)
            {
                case AttackState.Attack_1:                   
                    animator.SetTrigger("Attack1");                    
                    animator.ResetTrigger("Attack3");
                   // ShortAttack1_Particle.Play();

                    AttackTrigger = 0;                                       
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
   
    private void LongAttackGetObj()
    {           
        Bullet = ObjectPool.objectPool.LongAttackObj();

        if (Bullet == null) return;

        /*BulletStartPos = transform.position + FixBulletPos;*/
        BulletStartPos = transform.position + rotationEuler * FixBulletPos;
       // Debug.Log(FixBulletPos);
        Bullet.transform.position = BulletStartPos;
        Bullet.transform.rotation = transform.rotation;
        Bullet.SetActive(true);
        
        
        LongAttackEndPos = rotationEuler * new Vector3(0, 0, LongAttackMaxDis) + Bullet.transform.position;
        //Debug.Log(Bullet.transform.position);
    }

    private void DashAttackMove()
    {
        if(attackState == AttackState.DashAttack && CanMove)
        {
            DashAttack_NowDis = (Time.time - DashAttack_StartTime) * DashAttack_Speed;
            DashAttack_FracDis = DashAttack_NowDis / DashAttack_MaxDis;
            DashAttack_FracDis = Mathf.Clamp(DashAttack_FracDis, 0, 1);
            transform.position = Vector3.Lerp(transform.position, DashAttack_Pos, DashAttack_FracDis);
            Debug.Log(CanMove);
        }
        else
        {
            DashAttack_NowDis = 0;
            DashAttack_FracDis = 0;
        }
       
    }

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
        if (IsFastRun)
        {
            MaxMoveSpeed = FastRunSpeed;
        }
        else
        {
            MaxMoveSpeed = RunSpeed;
        }
        if (Input.GetAxis("Horizontal") != 0|| Input.GetAxis("Vertical") != 0) 
        {
            MoveSpeed = Mathf.Lerp(MoveSpeed, MaxMoveSpeed, 0.03f);
            MoveSpeed = Mathf.Clamp(MoveSpeed, 0, MaxMoveSpeed);
        }
        else if(Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            MoveSpeed = Mathf.Lerp(MoveSpeed, 0, 0.1f);
            if (MoveSpeed <= 0.06f && MoveSpeed >= -0.06f)
            {
                MoveSpeed = 0;
            }
        }
                             
        if(Input.GetAxis("Horizontal")==0|| Input.GetAxis("Vertical") == 0)
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
    
    private void FastRun()
    {
        /* if (Input.GetKeyDown(KeyCode.W)) 
         {
             if (CanDoubleClick) 
             {
                 nextClickTime = Time.time;
                 if (nextClickTime - preClickTime > 0.1f)
                 {
                     StopCoroutine(DoubleClickCoroutine);
                     CanDoubleClick = false;
                     moveState = MoveState.FastRunForward;
                     IsFastRun = true;
                 }

             }
             preClickTime = Time.time;
             CanDoubleClick = true;
             DoubleClickCoroutine = DoubleClick(DoubleClickTime);
             StartCoroutine(DoubleClickCoroutine);
             //Debug.Log(CanDoubleClick);        
         }*/


        /* if (CanFastRun && Input.GetKey(KeyCode.W))
         {
             moveState = MoveState.FastRunForward;
             IsFastRun = true;
         }*/
        
        if (Shift_LongPress && Input.GetKey(KeyCode.W))
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

        if ( (Input.GetKeyUp(KeyCode.LeftShift) && !Shift_LongPress) || Input.GetKeyUp(KeyCode.W)) 
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
    private void ShiftIntervel()
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
        else if(Input.GetKey(KeyCode.LeftShift) && Time.time - ShiftPressTime > 0.3f)
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
    //--------------------------Avoid---------------------------------   
    private void Avoid()
    {
        
        if (playerAnimatorState == PlayerAnimatorState.Movement || playerAnimatorState == PlayerAnimatorState.Attack && attackState != AttackState.BigSkill)  
        {
            
            if (Shift_Click)
            {
                
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
            
            AvoidEuler = Quaternion.Euler(0, RotationX + AvoidRotate, 0);            
            AvoidPosition = AvoidEuler * new Vector3(0, 0, AvoidDistance) + transform.position;
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
        CanMove = true;
        playerAnimatorState = PlayerAnimatorState.Avoid;
        DamageObject.SetActive(false);
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
        
    }

    private void AvoidMovement()
    {
       
        if (playerAnimatorState == PlayerAnimatorState.Avoid && CanMove/*AvoidCanMove*/)
        {           
            IsAvoidDistance += Time.deltaTime * AvoidSpeed;
            FracDistance = IsAvoidDistance / AvoidDistance;
            FracDistance = Mathf.Clamp(FracDistance, 0, 1);
            transform.position = Vector3.Lerp(transform.position, AvoidPosition, FracDistance);
                     
        }
        else
        {
            IsAvoidDistance = 0;
            FracDistance = 0;
            
           
        }
        
        
    }
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

    //------------Attack-----------
    public void TriggerCancelAttack(float WaitTime) 
    {      
        CancelAttackCoroutine = CancelAttack(WaitTime);
        StartCoroutine(CancelAttackCoroutine);

    }

    IEnumerator CancelAttack(float WaitTime) 
    {
        yield return new WaitForSeconds(WaitTime);
        attackState = AttackState.Default;        
    }

    public void CanTriggerAttack() 
    {        
        CanAttack = true;
        playerAnimatorState = PlayerAnimatorState.Attack;
        if (ResetStateCoroutine != null)
        {
            StopCoroutine(ResetStateCoroutine);       
        }
        if (CancelAttackCoroutine != null)
        {
            StopCoroutine(CancelAttackCoroutine);
        }
        StopCoroutine("CancelAttackNow");
       
    }
    public void ParticleTrigger()
    {
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
                LongAttackBullet_Particle.Stop();
                LongAttack_Particle.Play();
                LongAttackBullet_Particle.Play();
                LongAttackGetObj();
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

    public void FirstAttackState() 
    {
        
        playerAnimatorState = PlayerAnimatorState.Attack;
    }

    public void ChangeToIdle(float WaitTime) //-----Attack,Avoid,Damage-----
    {       
        ResetStateCoroutine = ResetState(WaitTime);

        Debug.Log("attack");
        if (AnimatorstateInfo.IsTag("Avoid") && attackState != AttackState.Default)
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
           // Debug.Log("aa");
        }
    }

    //----------------------------Trigger---------------------------------
}
