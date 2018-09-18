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
    public GameObject AttackCollider_Small;
    public GameObject AttackCollider_Big;
    private GameObject AttackCollider_BigSkill;
    //-------------------------------- Move------------------
    private float PlayerAnimation_parameter;
    private float MoveSpeed;//Player Data
    private float MaxMoveSpeed;//Player Data
    public float RunSpeed;//Player Data
    public float FastRunSpeed;//Player Data
    private float x_direction;
    private float y_direction;
    private float Move_parameter_x;
    private float Move_parameter_y;
    public float RotationSpeed;//Player Data
    private float RotationX;
    private Quaternion rotationEuler;
    public Transform Player_pre_pos;
    public Transform PlayerHead;
    private bool CanDoubleClick;
    public float DoubleClickTime;//PlayerData
    private bool IsFastRun;
    private bool CanFastRun;
    private float preClickTime;
    private float nextClickTime;  
    //----------------------------------Move-----------------
    //---------------------------------------Avoid-------------
    public float AvoidSpeed;//Player Data
    public float AvoidDistance;
    public float AvoidMaxDistance;
    private float AvoidRotate;
   
    private bool AvoidCanMove;
    private string InputKey_pre;
    private string InputKey_next;
    private float IsAvoidDistance;
    private float FracDistance;
    private Quaternion AvoidEuler;   
    private Vector3 AvoidPosition;
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

    private ParticleSystem ShortAttack1_Particle;
    private ParticleSystem ShortAttack2_Particle;
    private ParticleSystem ShortAttack3_Particle;
    private ParticleSystem LongAttack_Particle;
    //-----------------------------Particle------------

    private CapsuleCollider PlayerCollider;   
    
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
        Player_pre_pos = this.gameObject.transform.GetChild(0);
        ShortAttack1_Object = this.gameObject.transform.GetChild(1).gameObject;
        ShortAttack2_Object = this.gameObject.transform.GetChild(2).gameObject;
        ShortAttack3_Object = this.gameObject.transform.GetChild(3).gameObject;
        LongAttack_Object = this.gameObject.transform.GetChild(4).gameObject;
        DamageObject = this.gameObject.transform.GetChild(5).gameObject;
        AttackCollider_BigSkill = this.gameObject.transform.GetChild(6).gameObject;
    }
    // Use this for initialization
    
    void Start()
    {
        Move_parameter_x = 0;
        Move_parameter_x = 0;
        playerController = this;
        AttackTrigger = 0;       
        PlayerAnimation_parameter = 0;
        PlayerCollider = GetComponent<CapsuleCollider>();
        DamageCollider = DamageObject.GetComponent<CapsuleCollider>();
        FloorMask = LayerMask.GetMask("Floor");
        AvoidCanMove = true;
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
        
        //----particle--
        ShortAttack1_Particle = ShortAttack1_Object.GetComponent<ParticleSystem>();
        ShortAttack2_Particle = ShortAttack2_Object.GetComponent<ParticleSystem>();
        ShortAttack3_Particle = ShortAttack3_Object.GetComponent<ParticleSystem>();
        LongAttack_Particle = LongAttack_Object.GetComponent<ParticleSystem>();

        //-----particle---

        attackState = AttackState.Default;       
        moveState = MoveState.Idle;
        playerAnimatorState = PlayerAnimatorState.Movement;
        avoidState = AvoidState.Default;
    }
   
     private void FixedUpdate()
     {
        AnimatorClipInfo = animator.GetCurrentAnimatorClipInfo(0);
        AnimatorstateInfo = animator.GetCurrentAnimatorStateInfo(0);
        Rotaion();
        
        if (Physics.Raycast(transform.position, -Vector3.up, PlayerCollider.bounds.extents.y - grounded_dis, FloorMask))
        {
            Avoid();
            Attack();

            FastRun();

            if (playerAnimatorState == PlayerAnimatorState.Movement)
            {
                
                Movement();
            }
        }

        AnimatorStateControll();


        Debug.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - PlayerCollider.bounds.extents.y + grounded_dis, transform.position.z), Color.red);
       
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
        Debug.Log(playerAnimatorState);
      //  Debug.Log(attackState);

    }

//--------------------------Attack---------------------------------   
    private void Attack()
    {
       if(playerAnimatorState == PlayerAnimatorState.Movement || playerAnimatorState == PlayerAnimatorState.Attack || playerAnimatorState==PlayerAnimatorState.Avoid)
        {

            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (CanAttack)
                {
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
                            attackState = AttackState.Attack_1;
                            AttackTrigger += 1;
                            CanAttack = false;
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
                if (CanAttack && attackState != AttackState.BigSkill) 
                {
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
                    ShortAttack1_Particle.Play();

                    AttackTrigger = 0;                                       
                    break;
                case AttackState.Attack_2:
                    animator.SetTrigger("Attack2");
                    ShortAttack2_Particle.Play();
                    AttackTrigger = 0;                   
                    break;
                case AttackState.Attack_3:
                    animator.SetTrigger("Attack3");
                    animator.ResetTrigger("Attack1");
                    animator.ResetTrigger("Attack2");
                    ShortAttack3_Particle.Play();
                    AttackTrigger = 0;                    
                    break;
                case AttackState.LongAttack:
                    animator.SetTrigger("LongAttack");
                    LongAttack_Particle.Play();

                    AttackTrigger = 0;                  
                    break;
                case AttackState.BigSkill:
                    animator.SetTrigger("BigSkill");
                    AttackTrigger = 0;
                    break;
            }
            
        }      
        if (attackState == AttackState.Default)
        {
            CanAttack = true;
            AttackTrigger = 0;
        }
        //  Debug.Log(AnimatorstateInfo.shortNameHash);
        // Debug.Log(playerAnimatorState);            
        // Debug.Log(AnimatorstateInfo.IsName("PlayerController"));
        //Debug.Log(CanAttack);
        // Debug.Log(AttackTrigger);
       // Debug.Log(attackState);
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
            MoveSpeed = Mathf.Lerp(MoveSpeed, MaxMoveSpeed, 0.04f);
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
                     
        
        float MoveX = Input.GetAxis("Horizontal") * Time.deltaTime * MoveSpeed;
        float MoveZ = Input.GetAxis("Vertical") * Time.deltaTime * MoveSpeed;

       // Debug.Log(MoveSpeed);
        transform.Translate(MoveX, 0, MoveZ);

        MovementAnimaionControl();
    }
    
    private void FastRun()
    {
        /* if (Input.GetKeyDown(KeyCode.W) && !Back) 
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
         }
         else if (Input.GetKey(KeyCode.W) && Input.GetKeyDown(KeyCode.LeftShift))
         {
             moveState = MoveState.FastRunForward;
             IsFastRun = true;
         }*/
        if (CanFastRun && Input.GetKey(KeyCode.W))
        {
            moveState = MoveState.FastRunForward;
            IsFastRun = true;
        }
        if (IsFastRun && Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.A))
            {
                moveState = MoveState.FastRunLeft;

            }
            else if (Input.GetKey(KeyCode.D))
            {
                moveState = MoveState.FastRunRight;
            }           
            else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
            {
                moveState = MoveState.FastRunForward;
            }
            
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            moveState = MoveState.Idle;
            IsFastRun = false;
        }

        Debug.Log(CanFastRun);
    }
    public void TriggerFastRun(float time)
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
    //--------------------------Avoid---------------------------------   
    private void Avoid()
    {
        
        if (playerAnimatorState == PlayerAnimatorState.Movement || playerAnimatorState == PlayerAnimatorState.Attack) 
        {
            
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                AvoidCanMove = true;
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
                else if (Input.GetKey(KeyCode.S))
                {                   
                    AvoidStateSelect(AvoidState.Back);
                }
                else if (Input.GetKey(KeyCode.W))
                {                 
                    AvoidStateSelect(AvoidState.Forward);
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

        
        Debug.DrawLine(transform.position + new Vector3(0, 0.5f, PlayerCollider.radius), AvoidPosition + new Vector3(0, 0.5f, 0), Color.red);
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

    private void AvoidStateSelect(AvoidState avoidState)
    {
        AvoidCanMove = true;
        playerAnimatorState = PlayerAnimatorState.Avoid;
        DamageObject.SetActive(false);
        switch (avoidState)
        {
            case AvoidState.Forward:               
                avoidState = AvoidState.Forward;
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
        
        if (playerAnimatorState == PlayerAnimatorState.Avoid && AvoidCanMove)
        {           
            IsAvoidDistance += Time.deltaTime * AvoidSpeed;
            FracDistance = IsAvoidDistance / AvoidDistance;

            transform.position = Vector3.Lerp(transform.position, AvoidPosition, FracDistance);
            //Debug.Log("FracDistance" + FracDistance);
        }
        else
        {
            IsAvoidDistance = 0;
            FracDistance = 0;
        }
    }
    //--------------------------Avoid---------------------------------   

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
    }

    public void FirstAttackState() 
    {
        playerAnimatorState = PlayerAnimatorState.Attack;
    }

    public void ChangeToIdle(float WaitTime) //-----Attack,Avoid,Damage-----
    {       
        ResetStateCoroutine = ResetState(WaitTime);
      
        StartCoroutine(ResetStateCoroutine);
    }

    IEnumerator ResetState(float WaitTime)
    {
        yield return new WaitForSeconds(WaitTime);
        playerAnimatorState = PlayerAnimatorState.Movement;
        avoidState = AvoidState.Default;
        IsFastRun = false;
        DamageObject.SetActive(true);
    }

    public void CancelAttackNow() //-----Avoid-----
    {
        CanAttack = false;
        attackState = AttackState.Default;
      //  playerAnimatorState = PlayerAnimatorState.Avoid;
        animator.ResetTrigger("Attack3");
        animator.ResetTrigger("Attack2");
        animator.ResetTrigger("Attack1");
        if (ResetStateCoroutine != null)
        {
            StopCoroutine(ResetStateCoroutine);
        }
    }

    public void AttackColliderOpen_Small() 
    {
        AttackCollider_Small.SetActive(true);
        EnemyController.enemyController.EnemyCanDamage = true;
    }

    public void AttackColliderOpen_Big()
    {
        AttackCollider_Big.SetActive(true);
        EnemyController.enemyController.EnemyCanDamage = true;
    }
    public void AttackColliderOpen_BigSkill()
    {
        AttackCollider_BigSkill.SetActive(true);
        EnemyController.enemyController.EnemyCanDamage = true;
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
    

    //-------------------------Animatioｎ　Event--------------------******

    //----------------------------Trigger---------------------------------
    private void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "Wall")
        {
            AvoidCanMove = false;
            Debug.Log("aa");
        }
    }
    
    //----------------------------Trigger---------------------------------
}
