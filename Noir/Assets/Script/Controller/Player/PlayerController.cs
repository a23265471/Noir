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
        Avoid

    }

    private PlayerState playerState;
    private PlayerBlendTreeState playerBlendTreeState;

    public static PlayerController playerController;  
    public float MoveSpeed;
    public float RotationSpeed;
    private float RotationX;
    private Quaternion rotationEuler;
    public Transform Player_pre_pos;
    public Transform PlayerHead;

    private CapsuleCollider PlayerCollider;
    private float Motion_parameter_x;
    private float Motion_parameter_y;
    private float PlayerAnimation_parameter;
    private bool CanRun;
    private int FloorMask;
    public float grounded_dis;

    private bool IsAttacking;

    private Animator animator;
    AnimatorClipInfo[] AnimatorClipInfo;

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
    }

    private void Update()
    {
        
    }

    private void FixedUpdate ()
    {

        if (playerBlendTreeState == PlayerBlendTreeState.Movement)
        {
            Rotaion();
        }
        
        

        if (Physics.Raycast(transform.position, -Vector3.up, PlayerCollider.bounds.extents.y - grounded_dis, FloorMask))
        {
            Attack();
            if (!IsAttacking)
            {
                Movement();
            }
           
        }

       // Debug.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - PlayerCollider.bounds.extents.y + grounded_dis, transform.position.z), Color.red);

        AnimatorClipInfo = animator.GetCurrentAnimatorClipInfo(0);
        // Debug.Log(AnimatorClipInfo[0].clip.length);

        BlendTreeState();
        
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
        switch (playerBlendTreeState)
        {
            case PlayerBlendTreeState.Movement:
                PlayerAnimation_parameter = Mathf.Lerp(PlayerAnimation_parameter, 0, 0.1f);

                break;
            case PlayerBlendTreeState.Attack:
                PlayerAnimation_parameter = Mathf.Lerp(PlayerAnimation_parameter, 1, 0.1f);
                break;
        }

        if (PlayerAnimation_parameter <= ((int)playerBlendTreeState + 0.06f) && PlayerAnimation_parameter >= -((int)playerBlendTreeState + 0.06f))
        {
            PlayerAnimation_parameter = (int)playerBlendTreeState;
            Debug.Log(PlayerAnimation_parameter);
        }
        animator.SetFloat("Action_Contrll", PlayerAnimation_parameter);
    }

    private void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("a");
            playerBlendTreeState = PlayerBlendTreeState.Attack;
            IsAttacking = true;     
            
            if (playerState != PlayerState.Attack_1 && playerState != PlayerState.Attack_2 && playerState != PlayerState.Attack_3)
            {
                playerState = PlayerState.Attack_1;
            }
            else if (playerState == PlayerState.Attack_1)
            {
                playerState = PlayerState.Attack_2;
            }
            else if (playerState == PlayerState.Attack_2)
            {
                playerState = PlayerState.Attack_3;
            }            
            //animator.SetFloat("AttackControl",)

        }
    }
    
    private void AttackAnimation()
    {
        switch (playerState)
        {
            case PlayerState.Attack_2:

                break;

        }

       
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
                Motion_parameter_x = Mathf.Lerp(Motion_parameter_x, 0, 0.1f);
                Motion_parameter_y = Mathf.Lerp(Motion_parameter_y, 0, 0.1f);
                break;

            case PlayerState.GoRight:
                Motion_parameter_x = Mathf.Lerp(Motion_parameter_x, 1, 0.1f);
                Motion_parameter_y = Mathf.Lerp(Motion_parameter_y, 0, 0.1f);
                if (Motion_parameter_x >= 1)
                {
                    Motion_parameter_x = 1;
                }
                break;

            case PlayerState.GoLeft:
                Motion_parameter_x = Mathf.Lerp(Motion_parameter_x, -1, 0.1f);
                Motion_parameter_y = Mathf.Lerp(Motion_parameter_y, 0, 0.1f);
                if (Motion_parameter_x <= -1)
                {
                    Motion_parameter_x = -1;
                }
                break;

            case PlayerState.GoForward:
                Motion_parameter_x = Mathf.Lerp(Motion_parameter_x, 0, 0.1f);
                Motion_parameter_y = Mathf.Lerp(Motion_parameter_y, 1, 0.1f);
                if (Motion_parameter_y >= 1)
                {
                    Motion_parameter_y = 1;
                }
                break;
            case PlayerState.GoBack:
                Motion_parameter_x = Mathf.Lerp(Motion_parameter_x, 0, 0.1f);
                Motion_parameter_y = Mathf.Lerp(Motion_parameter_y, -1, 0.08f);
                if (Motion_parameter_y <= -1)
                {
                    Motion_parameter_y = -1;
                }
                break;
            case PlayerState.GoForwardRight:
                Motion_parameter_x = Mathf.Lerp(Motion_parameter_x, 1, 0.1f);
                Motion_parameter_y = Mathf.Lerp(Motion_parameter_y, 1, 0.1f);
                if (Motion_parameter_x >= 1)
                {
                    Motion_parameter_x = 1;
                }
                if (Motion_parameter_y >= 1)
                {
                    Motion_parameter_y = 1;
                }
                break;
            case PlayerState.GoForwardLeft:
                Motion_parameter_x = Mathf.Lerp(Motion_parameter_x, -1, 0.1f);
                Motion_parameter_y = Mathf.Lerp(Motion_parameter_y, 1, 0.1f);
                if (Motion_parameter_x <= -1)
                {
                    Motion_parameter_x = -1;
                }
                if (Motion_parameter_y >= 1)
                {
                    Motion_parameter_y = 1;
                }
                break;
            case PlayerState.GoBackLeft:
                Motion_parameter_x = Mathf.Lerp(Motion_parameter_x, -1, 0.1f);
                Motion_parameter_y = Mathf.Lerp(Motion_parameter_y, -1, 0.1f);
                if (Motion_parameter_x <= -1)
                {
                    Motion_parameter_x = -1;
                }
                if (Motion_parameter_y <= -1)
                {
                    Motion_parameter_y = -1;
                }
                break;
            case PlayerState.GoBackRight:
                Motion_parameter_x = Mathf.Lerp(Motion_parameter_x, 1, 0.1f);
                Motion_parameter_y = Mathf.Lerp(Motion_parameter_y, -1, 0.1f);
                if (Motion_parameter_x >= 1)
                {
                    Motion_parameter_x = 1;
                }
                if (Motion_parameter_y <= -1)
                {
                    Motion_parameter_y = -1;
                }
                break;
        }

        if (Motion_parameter_x <= 0.06f && Motion_parameter_x >= -0.06f)
        {
            Motion_parameter_x = 0;
        }
        if (Motion_parameter_y <= 0.06f && Motion_parameter_y >= -0.06f)
        {
            Motion_parameter_y = 0;
        }
        animator.SetFloat("RunSpeed_Horizontal", Motion_parameter_x);
        animator.SetFloat("RunSpeed_Vertical", Motion_parameter_y);
    }

}
