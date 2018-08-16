﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

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

    public static PlayerController playerController;  
    public float MoveSpeed;
    public float RotationSpeed;
    private float RotationX;
    private Quaternion rotationEuler;
    public Transform Player_pre_pos;
    public Transform PlayerHead;

    private float Motion_parameter_x;
    private float Motion_parameter_y;
    private float PlayerAnimation_parameter;

    private Animator animator { get; set; }
    public string[] AnimatorStateNames;
    private float AnimationTime;
    private Dictionary<int, string> NameTable { get; set; }

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
        Player_pre_pos = this.gameObject.transform.GetChild(0);
        PlayerAnimation_parameter = 0;

        BuildNameTable();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {        
        Rotaion();
        Movement();
        GetCurrentAnimatorStateName();
    }

    public string GetCurrentAnimatorStateName()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorClipInfo[] stateClipInfo = animator.GetCurrentAnimatorClipInfo(0);
        string stateName;
        if (NameTable.TryGetValue(stateInfo.shortNameHash, out stateName))
        {
            AnimationTime = stateClipInfo[0].clip.length * stateInfo.normalizedTime;
            Debug.Log(stateName);
            return stateName;
        }
        else
        {
            Debug.LogWarning("Unknown animator state name.");
            return string.Empty;
        }
    }

    private void BuildNameTable()
    {
        NameTable = new Dictionary<int, string>();

        foreach (string stateName in AnimatorStateNames)
        {
            NameTable[Animator.StringToHash(stateName)] = stateName;
        }
    }

    private void Movement()
    {
        float MoveX = Input.GetAxis("Horizontal") * Time.deltaTime * MoveSpeed;
        float MoveZ = Input.GetAxis("Vertical") * Time.deltaTime * MoveSpeed;
        transform.Translate(MoveX, 0, MoveZ);

        PlayerAnimation_parameter = Mathf.Lerp(PlayerAnimation_parameter, 0, 0.1f);
        if(PlayerAnimation_parameter <= 0.06f && PlayerAnimation_parameter >= -0.06f)
        {
            PlayerAnimation_parameter = 0;
        }
        animator.SetFloat("Action_Contrll", PlayerAnimation_parameter);

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
    private void Attack()
    {

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

   

}
