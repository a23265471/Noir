using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    enum PlayerBlendTreeState
    {
        Movement,
        Attack,
    }
    enum PlayerState
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
        Attack_1,
        Attack_2,
        Attack_3,
        LongAttack,
        Avoid

    }

    private PlayerState playerState;
    private PlayerBlendTreeState playerBlendTreeState;

    public static PlayerController playerController;  
    public float MoveSpeed;
    private float x_direction;
    private float y_direction;
    public float RotationSpeed;
    private float RotationX;
    private Quaternion rotationEuler;
    public Transform Player_pre_pos;
    public Transform PlayerHead;

    private CapsuleCollider PlayerCollider;
    private float Motion_parameter_x;
    private float Motion_parameter_y;
    private float PlayerAnimation_parameter;
    private int FloorMask;
    public float grounded_dis;

    private bool IsAttacking;
    private float Attack_parameter;
    private float Attack_Short_parameter;
    private bool CanAttack;

    private Animator animator;
    AnimatorClipInfo[] AnimatorClipInfo;
    AnimatorStateInfo AnimatorstateInfo;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    // Use this for initialization
    void Start ()
    {
        Motion_parameter_x = 0;
        Motion_parameter_x = 0;
        playerController = this;
        IsAttacking = false;
        Player_pre_pos = this.gameObject.transform.GetChild(0);
        PlayerAnimation_parameter = 0;
        PlayerCollider = GetComponent<CapsuleCollider>();
        FloorMask = LayerMask.GetMask("Floor");
        CanAttack = true;
    }

    private void Update()
    {
        
    }

    private void FixedUpdate ()
    {
        AnimatorClipInfo = animator.GetCurrentAnimatorClipInfo(0);
        AnimatorstateInfo = animator.GetCurrentAnimatorStateInfo(0);
        BlendTreeState();
        Rotaion();

        if (Physics.Raycast(transform.position, -Vector3.up, PlayerCollider.bounds.extents.y - grounded_dis, FloorMask))
        {
            Attack();
            if (playerBlendTreeState == PlayerBlendTreeState.Movement) 
            {
                Movement();
            }
           
        }

        // Debug.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - PlayerCollider.bounds.extents.y + grounded_dis, transform.position.z), Color.red);

       
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

    private void BlendTreeState()
    {
        
        if (playerBlendTreeState != PlayerBlendTreeState.Movement) 
        {
            Motion_parameter_x = Mathf.Lerp(Motion_parameter_x, 0, 0.1f);
            Motion_parameter_y = Mathf.Lerp(Motion_parameter_y, 0, 0.1f);
            animator.SetFloat("RunSpeed_Horizontal", Motion_parameter_x);
            animator.SetFloat("RunSpeed_Vertical", Motion_parameter_y);
        }


        if (playerState == PlayerState.Attack_3 && AnimatorstateInfo.IsName("PlayerController") && playerState == PlayerState.LongAttack) 
        {
            playerBlendTreeState = PlayerBlendTreeState.Movement;
            playerState = PlayerState.Idle;
            
        }
        else if (playerState != PlayerState.Idle && AnimatorstateInfo.IsName("PlayerController"))
        {         
            StartCoroutine("CancelAttack");
        }

        Debug.Log(IsAttacking);
        //Debug.Log(playerState);
        //Debug.Log(AnimatorstateInfo.IsName("PlayerController"));

    }

    private void Attack()
    {

        if (CanAttack)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StopCoroutine("CancelAttack");
                playerBlendTreeState = PlayerBlendTreeState.Attack;

                if (playerState != PlayerState.Attack_1 && playerState != PlayerState.Attack_2 && playerState != PlayerState.Attack_3)
                {
                    playerState = PlayerState.Attack_1;
                    animator.SetTrigger("Attack1");
                }
                else if (playerState == PlayerState.Attack_1)
                {
                    playerState = PlayerState.Attack_2;
                    animator.SetTrigger("Attack2");
                }
                else if (playerState == PlayerState.Attack_2)
                {
                    playerState = PlayerState.Attack_3;
                    animator.SetTrigger("Attack3");
                }

                CanAttack = false;
                StartCoroutine("Canattack");
            }
            else if (Input.GetMouseButtonDown(1))
            {
                StopCoroutine("CancelAttack");
                playerBlendTreeState = PlayerBlendTreeState.Attack;

                playerState = PlayerState.LongAttack;
                animator.SetTrigger("LongAttack");
                CanAttack = false;
                StartCoroutine("Canattack");

            }
             
        }

        

    }

    IEnumerator Canattack()
    {
        yield return new WaitForSeconds(0.3f);
        CanAttack = true;

    }

    IEnumerator CancelAttack()
    {      
        yield return new WaitForSeconds(1f);
        playerBlendTreeState = PlayerBlendTreeState.Movement;
        // animator.SetTrigger("Idle");
        playerState = PlayerState.Idle;

        

    }

    private void MovementAnimaionControl()
    {
        
        playerBlendTreeState = PlayerBlendTreeState.Movement;

        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W))
        {
            playerState = PlayerState.GoForwardRight;
        }
        else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W))
        {
            playerState = PlayerState.GoForwardLeft;
        }
        else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S))
        {
            playerState = PlayerState.GoBackLeft;
        }
        else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S))
        {
            playerState = PlayerState.GoBackRight;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            playerState = PlayerState.GoRight;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            playerState = PlayerState.GoLeft;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            playerState = PlayerState.GoForward;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            playerState = PlayerState.GoBack;
        }
        else
        {
            playerState = PlayerState.Idle;
        }

        switch (playerState)
        {          
            case PlayerState.Idle:          
                x_direction = 0;
                y_direction = 0;
                break;

            case PlayerState.GoRight:            
                x_direction = 1;
                y_direction = 0;
                break;

            case PlayerState.GoLeft:            
                x_direction = -1;
                y_direction = 0;
                break;

            case PlayerState.GoForward:           
                x_direction = 0;
                y_direction = 1;
                break;
            case PlayerState.GoBack:          
                x_direction = 0;
                y_direction = -1;
                break;
            case PlayerState.GoForwardRight:         
                x_direction = 1;
                y_direction = 1;
                break;
            case PlayerState.GoForwardLeft:            
                x_direction = -1;
                y_direction = 1;
                break;
            case PlayerState.GoBackLeft:               
                x_direction = -1;
                y_direction = -1;
                break;
            case PlayerState.GoBackRight:            
                x_direction = 1;
                y_direction = -1;
                break;
        }

        Motion_parameter_x = Mathf.Lerp(Motion_parameter_x, x_direction, 0.1f);
        Motion_parameter_y = Mathf.Lerp(Motion_parameter_y, y_direction, 0.1f);      
        
        if (Motion_parameter_x <= 0.06f && Motion_parameter_x >= -0.06f)
        {
            Motion_parameter_x = 0;
        }
        if (Motion_parameter_y <= 0.06f && Motion_parameter_y >= -0.06f)
        {
            Motion_parameter_y = 0;
        }

        Motion_parameter_x = Mathf.Clamp(Motion_parameter_x, -1, 1);
        Motion_parameter_y = Mathf.Clamp(Motion_parameter_y, -1, 1);

        animator.SetFloat("RunSpeed_Horizontal", Motion_parameter_x);
        animator.SetFloat("RunSpeed_Vertical", Motion_parameter_y);
    }
   


}
