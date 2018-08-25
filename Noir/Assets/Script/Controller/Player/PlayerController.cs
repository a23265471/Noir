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

    private float PlayerAnimation_parameter;
    public float MoveSpeed;
    private float x_direction;
    private float y_direction;
    private float Move_parameter_x;
    private float Move_parameter_y;
    public float RotationSpeed;
    private float RotationX;
    private Quaternion rotationEuler;
    public Transform Player_pre_pos;
    public Transform PlayerHead;

    private CapsuleCollider PlayerCollider;   
    
    private int FloorMask;
    public float grounded_dis;

    private bool AttackTrigger;
   
    private bool CanClick;

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
        AttackTrigger = true;
        Player_pre_pos = this.gameObject.transform.GetChild(0);
        PlayerAnimation_parameter = 0;
        PlayerCollider = GetComponent<CapsuleCollider>();
        FloorMask = LayerMask.GetMask("Floor");
        CanClick = true;
        attackState = AttackState.Default;
        moveState = MoveState.Idle;
        playerAnimatorState = PlayerAnimatorState.Movement;
    }
    private void Update()
    {
        AnimatorClipInfo = animator.GetCurrentAnimatorClipInfo(0);
        AnimatorstateInfo = animator.GetCurrentAnimatorStateInfo(0);

        Rotaion();

        if (Physics.Raycast(transform.position, -Vector3.up, PlayerCollider.bounds.extents.y - grounded_dis, FloorMask))
        {
            Attack();

            if (playerAnimatorState == PlayerAnimatorState.Movement)
            {
                //Debug.Log("SS");
                Movement();
            }
        }

        ResetBlendTree();
        Debug.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - PlayerCollider.bounds.extents.y + grounded_dis, transform.position.z), Color.red);
    

}

   /* private void FixedUpdate()
    {
        AnimatorClipInfo = animator.GetCurrentAnimatorClipInfo(0);
        AnimatorstateInfo = animator.GetCurrentAnimatorStateInfo(0);

        Rotaion();

        if (Physics.Raycast(transform.position, -Vector3.up, PlayerCollider.bounds.extents.y - grounded_dis, FloorMask))
        {
            Attack();
            
            if (playerAnimatorState == PlayerAnimatorState.Movement)
            {
                //Debug.Log("SS");
                Movement();
            }                      
        }
        
        ResetBlendTree();
        Debug.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - PlayerCollider.bounds.extents.y + grounded_dis, transform.position.z), Color.red);
    }*/

    private void Attack()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            playerAnimatorState = PlayerAnimatorState.Attack;
            StopCoroutine("CancelAttack");
            switch (attackState)
            {
                case AttackState.Default:
                    AttackTrigger = true;
                    attackState = AttackState.Attack_1;                       
                    break;
                case AttackState.Attack_1:                       
                    attackState = AttackState.Attack_2;

                    break;
                case AttackState.Attack_2:
                    
                    attackState = AttackState.Attack_3;
                    break;
            }

               StartCoroutine(ClickIntervel(0.2f));
        }

        
        
        AttackAnimation();
    }
    private void AttackAnimation()
    {
        if (AttackTrigger)
        {
            switch (attackState)
            {
                case AttackState.Attack_1:
                    animator.SetTrigger("Attack1");                    
                    AttackTrigger = false;
                    break;
                case AttackState.Attack_2:
                    animator.SetTrigger("Attack2");                  
                    AttackTrigger = false;
                    break;
                case AttackState.Attack_3:
                    animator.SetTrigger("Attack3");
                    AttackTrigger = false;
                    break;
                case AttackState.LongAttack:
                    animator.SetTrigger("LongAttack");
                    AttackTrigger = false;
                    break;
            }            
        }

        if (attackState == AttackState.Attack_3 && AnimatorstateInfo.IsName("PlayerController"))
        {
            attackState = AttackState.Default;
            playerAnimatorState = PlayerAnimatorState.Movement;
        }
        
        Debug.Log(playerAnimatorState);
            
        // Debug.Log(AnimatorstateInfo.IsName("PlayerController"));
        /*  Debug.Log(attackState);*/
        
    }

    IEnumerator CancelAttack()
    {
        
        yield return new WaitForSeconds(1.5f);
        Debug.Log("a");
        attackState = AttackState.Default;
    }

    IEnumerator ClickIntervel(float IntervelTime)
    {
        CanClick = true;
        yield return new WaitForSeconds(IntervelTime);
        CanClick = false;
    }

    private void ResetBlendTree()
    {
        if (playerAnimatorState != PlayerAnimatorState.Movement)
        {
            Move_parameter_x = Mathf.Lerp(Move_parameter_x, 0, 0.15f);
            Move_parameter_y = Mathf.Lerp(Move_parameter_y, 0, 0.15f);
            animator.SetFloat("RunSpeed_Horizontal", Move_parameter_x);
            animator.SetFloat("RunSpeed_Vertical", Move_parameter_y);
        }
    }
    

    private void Movement()
    {
        float MoveX = Input.GetAxis("Horizontal") * Time.deltaTime * MoveSpeed;
        float MoveZ = Input.GetAxis("Vertical") * Time.deltaTime * MoveSpeed;
        transform.Translate(MoveX, 0, MoveZ);

        MovementAnimaionControl();
    }   
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
  
    
    private void MovementAnimaionControl()
    {
      //  playerAnimatorState = PlayerAnimatorState.Movement;

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
        }

        Move_parameter_x = Mathf.Lerp(Move_parameter_x, x_direction, 0.15f);
        Move_parameter_y = Mathf.Lerp(Move_parameter_y, y_direction, 0.15f);      
        
        if (Move_parameter_x <= 0.06f && Move_parameter_x >= -0.06f)
        {
            Move_parameter_x = 0;
        }
        if (Move_parameter_y <= 0.06f && Move_parameter_y >= -0.06f)
        {
            Move_parameter_y = 0;
        }

        Move_parameter_x = Mathf.Clamp(Move_parameter_x, -1, 1);
        Move_parameter_y = Mathf.Clamp(Move_parameter_y, -1, 1);

        animator.SetFloat("RunSpeed_Horizontal", Move_parameter_x);
        animator.SetFloat("RunSpeed_Vertical", Move_parameter_y);
    }
    
}
