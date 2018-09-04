using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    enum PlayerAnimatorState
    {
        Movement,
        Attack,
        Avoid
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
    }

    private AttackState attackState;
    private MoveState moveState;
    private PlayerAnimatorState playerAnimatorState;
    public static PlayerController playerController;
    public GameObject AttackCollider_Small;
    public GameObject AttackCollider_Big;
    #region Move
    private float PlayerAnimation_parameter;
    private float MoveSpeed;//Player Data
    public float MaxMoveSpeed;//Player Data
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
    #endregion
    private float AvoidSpeed;//Player Data
    public float AvoidMaxSpeed;

   

    private CapsuleCollider PlayerCollider;   
    
    private int FloorMask;
    public float grounded_dis;

    private int AttackTrigger;     
    private bool CanAttack;
    private IEnumerator ResetStateCoroutine;
    private IEnumerator CancelAttackCoroutine;
    private IEnumerator DoubleClickCoroutine;

    private Animator animator;
    AnimatorClipInfo[] AnimatorClipInfo;
    AnimatorStateInfo AnimatorstateInfo;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    // Use this for initialization
    void Start()
    {
        Move_parameter_x = 0;
        Move_parameter_x = 0;
        playerController = this;
        AttackTrigger = 0;
        Player_pre_pos = this.gameObject.transform.GetChild(0);
        PlayerAnimation_parameter = 0;
        PlayerCollider = GetComponent<CapsuleCollider>();
        FloorMask = LayerMask.GetMask("Floor");
        AttackCollider_Small.SetActive(false);
        AttackCollider_Big.SetActive(false);      
        CanAttack = true;
        CancelAttackCoroutine = null;
        ResetStateCoroutine = null;
        DoubleClickCoroutine = null;

        attackState = AttackState.Default;       
        moveState = MoveState.Idle;
        playerAnimatorState = PlayerAnimatorState.Movement;
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
        /* if (AnimatorstateInfo.IsName("Avoid_Left") || AnimatorstateInfo.IsName("Avoid_Right"))
         {
             playerAnimatorState = PlayerAnimatorState.Avoid;            
         }*/
        if (AnimatorstateInfo.IsName("ShortAttack_1") || AnimatorstateInfo.IsName("ShortAttack_2") || AnimatorstateInfo.IsName("ShortAttack_3") || AnimatorstateInfo.IsName("LongAttack"))
        {
            Move_parameter_x = Mathf.Lerp(Move_parameter_x, 0, 0.15f);
            Move_parameter_y = Mathf.Lerp(Move_parameter_y, 0, 0.15f);
            animator.SetFloat("RunSpeed_Horizontal", Move_parameter_x);
            animator.SetFloat("RunSpeed_Vertical", Move_parameter_y);
            playerAnimatorState = PlayerAnimatorState.Attack;
        }

        if (playerAnimatorState != PlayerAnimatorState.Movement)
        {
            Move_parameter_x = Mathf.Lerp(Move_parameter_x, 0, 0.15f);
            Move_parameter_y = Mathf.Lerp(Move_parameter_y, 0, 0.15f);
            animator.SetFloat("RunSpeed_Horizontal", Move_parameter_x);
            animator.SetFloat("RunSpeed_Vertical", Move_parameter_y);
        }
      //  Debug.Log(playerAnimatorState);
      //  Debug.Log(attackState);

    }

   
    private void Attack()
    {
       
        if (Input.GetMouseButtonDown(0) && (playerAnimatorState==PlayerAnimatorState.Movement||playerAnimatorState==PlayerAnimatorState.Attack))
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
                    if(CanAttack)
                    {  
                        attackState = AttackState.Attack_2;
                        AttackTrigger += 1;
                        CanAttack = false;                      
                    }                        
                    break;
                    case AttackState.Attack_2:
                    if(CanAttack)
                    {                     
                        attackState = AttackState.Attack_3;
                        AttackTrigger += 1;
                        CanAttack = false;                    
                    }
                      /*  Debug.Log(AttackTrigger);*/
                        
                    break;
                }                        
        }
        AttackAnimation();
        if (attackState == AttackState.Default)
        {
            CanAttack = true;          
        }
      //  Debug.Log(attackState);
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
                    AttackTrigger = 0;                                       
                    break;
                case AttackState.Attack_2:
                    animator.SetTrigger("Attack2");
                    
                   
                    AttackTrigger = 0;                   
                    break;
                case AttackState.Attack_3:
                    animator.SetTrigger("Attack3");
                    animator.ResetTrigger("Attack1");
                    animator.ResetTrigger("Attack2");
                    AttackTrigger = 0;                    
                    break;
                case AttackState.LongAttack:
                    animator.SetTrigger("LongAttack");
                    AttackTrigger = 0;                  
                    break;
            }
            
        }
        
        if (attackState == AttackState.Attack_3 && AnimatorstateInfo.IsName("PlayerController"))
        {
            attackState = AttackState.Default;            
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
    }
   
    

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
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (CanDoubleClick) 
            {
                StopCoroutine(DoubleClickCoroutine);
                CanDoubleClick = false;
                moveState = MoveState.FastRunForward;
                IsFastRun = true;
            }
            CanDoubleClick = true;
            DoubleClickCoroutine = DoubleClick(DoubleClickTime);
            StartCoroutine(DoubleClickCoroutine);
            Debug.Log(CanDoubleClick);        
        }
        else if (Input.GetKey(KeyCode.W) && Input.GetKeyDown(KeyCode.LeftShift))
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
        Debug.Log(moveState);
       
    }

    IEnumerator DoubleClick(float WaitTime)
    {
        yield return new WaitForSeconds(WaitTime);
        CanDoubleClick = false;

    }

    private void MovementAnimaionControl()
    {
        //  playerAnimatorState = PlayerAnimatorState.Movement;
        if (!IsFastRun)
        {
            if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W))
            {
                moveState = MoveState.GoForwardRight;
            }
            else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W))
            {
                moveState = MoveState.GoForwardLeft;
            }
            else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S))
            {
                moveState = MoveState.GoBackLeft;
            }
            else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S))
            {
                moveState = MoveState.GoBackRight;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                moveState = MoveState.GoRight;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                moveState = MoveState.GoLeft;
            }
            else if (Input.GetKey(KeyCode.W))
            {
                moveState = MoveState.GoForward;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                moveState = MoveState.GoBack;
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
    }
    #endregion

    private void Avoid()
    {
        if (Input.GetKey(KeyCode.A) && Input.GetKeyDown(KeyCode.LeftShift))
        {
            playerAnimatorState = PlayerAnimatorState.Avoid;
            animator.SetTrigger("Avoid_Left");
            
        }
        else if(Input.GetKey(KeyCode.D) && Input.GetKeyDown(KeyCode.LeftShift))
        {
            playerAnimatorState = PlayerAnimatorState.Avoid;
            animator.SetTrigger("Avoid_Right");
            
        }
       
        AvoidMovement();
    
    }
   
    private void AvoidMovement()
    {
        if (AnimatorstateInfo.IsName("Avoid_Left"))
        {
            AvoidSpeed = Mathf.Lerp(AvoidSpeed, AvoidMaxSpeed, 11f);

            float MoveX = -1 * Time.deltaTime * AvoidSpeed;

            transform.Translate(MoveX, 0, 0);
        }
        else if (AnimatorstateInfo.IsName("Avoid_Right"))
        {
            AvoidSpeed = Mathf.Lerp(AvoidSpeed, AvoidMaxSpeed, 1f);
            float MoveX = 1 * Time.deltaTime * AvoidSpeed;

            transform.Translate(MoveX, 0, 0);
        }

    }
//------------------------------------Animatioｎ　Event--------------------
   
   
    public void TriggerCancelAttack(float WaitTime)
    {      
        CancelAttackCoroutine = CancelAttack(WaitTime);
        StartCoroutine(CancelAttackCoroutine);

    }

    IEnumerator CancelAttack(float WaitTime)
    {
        yield return new WaitForSeconds(WaitTime);

        Debug.Log("Default");
        attackState = AttackState.Default;
        
    }

    public void CanTriggerAttack()
    {
        CanAttack = true;
        StopCoroutine(ResetStateCoroutine);
        StopCoroutine(CancelAttackCoroutine);
        Debug.Log("CanAttack");
    }
  
    public void ChangeToIdle(float WaitTime)
    {       
        ResetStateCoroutine = ResetState(WaitTime);
        StartCoroutine(ResetStateCoroutine);
    }

    IEnumerator ResetState(float WaitTime)
    {
        yield return new WaitForSeconds(WaitTime);
        playerAnimatorState = PlayerAnimatorState.Movement;
        //Debug.Log("aa");
    }

    public void CancelAttackNow()
    {
        CanAttack = false;
        attackState = AttackState.Default;
        playerAnimatorState = PlayerAnimatorState.Avoid;
        animator.ResetTrigger("Attack3");
        animator.ResetTrigger("Attack2");
        animator.ResetTrigger("Attack1");
        StopCoroutine("ResetState");

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

    public void AttackColliderClose()
    {
        AttackCollider_Small.SetActive(false);
        AttackCollider_Big.SetActive(false);
    }

   
}
