﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_New : MonoBehaviour {

   // private GameStageController gameStageController;
    private PlayerBehaviour playerBehaviour;
    private GameStageData gameStageData;
    private InputSetting inputSetting;


    public int moveDirection_Vertical;
    public int moveDirection_Horizontal;
    
    string keepKeyCode;

    IEnumerator cleanKeepKeyCode;

    private void Awake()
    {
        gameStageData = GameFacade.GetInstance().gameStageData;
        inputSetting=GameFacade.GetInstance().inputSetting;

        playerBehaviour = GetComponent<PlayerBehaviour>();
    }

    void Start ()
    {
        keepKeyCode = "";
        cleanKeepKeyCode = null;
        
    }
	
    private void Update()
    {
        PlayerDirectionControl();       
        GroundedMove();
        Falling();
        Jump();
        Attack();
        Dash();
        Avoid();
    }

    public void GroundedMove()
    { 
        playerBehaviour.GroundedMove(moveDirection_Vertical, moveDirection_Horizontal);
                
    }

    
    public void Falling()
    {
        playerBehaviour.Falling(moveDirection_Vertical, moveDirection_Horizontal);
    }

    public void Avoid()
    {
        if (Input.GetKeyDown(inputSetting.inputKey.Avoid) && (moveDirection_Vertical != 0 || moveDirection_Horizontal != 0) ) 
        {
            playerBehaviour.Avoid(moveDirection_Vertical, moveDirection_Horizontal);
           // Debug.Log(moveDirection_Vertical);
           
        } 
    }

    public void Jump()
    {
        if (Input.GetKeyDown(inputSetting.inputKey.Jump))
        {          
            playerBehaviour.Jump(moveDirection_Vertical, moveDirection_Horizontal);
        }

    }

    public void Attack()
    {
        if (Input.GetKeyDown(inputSetting.inputKey.BigSkill))
        {
            playerBehaviour.Skill();

        }else if (Input.GetKeyDown(inputSetting.inputKey.Shoot))
        {
            playerBehaviour.Shooting();
        }
        else if (Input.GetKeyDown(inputSetting.inputKey.NormalAttack))
        {
            playerBehaviour.NormalAttack();

        }
    }

    

    public void Dash()
    {
        if (Input.GetKeyDown(inputSetting.inputKey.Dash))
        {
            playerBehaviour.Dash(moveDirection_Vertical, moveDirection_Horizontal);
        }


    }


    private void PlayerDirectionControl()
    {     
        if (Input.GetAxis("Vertical") > 0 && (Input.GetKey(inputSetting.inputKey.Forward) || Input.GetKey(KeyCode.UpArrow)))
        {
            moveDirection_Vertical = 1;
        }
        else if (Input.GetAxis("Vertical") < 0 && (Input.GetKey(inputSetting.inputKey.Back) || Input.GetKey(KeyCode.DownArrow)))
        {
            moveDirection_Vertical = -1;
        }
        else
        {
            moveDirection_Vertical = 0;
        }

        if (Input.GetAxis("Horizontal") > 0 && (Input.GetKey(inputSetting.inputKey.Right) || Input.GetKey(KeyCode.RightArrow)))
        {
            moveDirection_Horizontal = 1;
        }
        else if (Input.GetAxis("Horizontal") < 0 && (Input.GetKey(inputSetting.inputKey.Left) || Input.GetKey(KeyCode.LeftArrow)))
        {
            moveDirection_Horizontal = -1;
        }
        else
        {
            moveDirection_Horizontal = 0;
        }       
    }
    
    private void DoKeepCode(ref string keepString,string KeyCode)
    {
        if (keepString == "")
        {
            keepString = KeyCode;
        }
        
    }

    IEnumerator CleanKeepKeyCode()
    {
        yield return new WaitForSeconds(0.2f);
        keepKeyCode = "";
    }

}
