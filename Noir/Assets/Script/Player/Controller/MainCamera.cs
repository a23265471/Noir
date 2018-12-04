﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{

    // public GameObject target;
    //  public PlayerController playerController;
    public static MainCamera mainCamera;

    private Quaternion rotationEuler;
    private Quaternion moveRotationEuler;
    public float RotateSpeed_X;
    public float RotateSpeed_Y;
    private float CameraLookAt_X;
    private float CameraLookAt_Y;
    public float distence;
    public float Max_distence;
    public float Min_distence;
    private float changeCameraHigh;
    public float distenceSpeed;
    private int WallMask;
    private int FloorMask;
    public float preDistence;
    private int EnemyLayerMask;

    public Ray aimPoint;
    private RaycastHit playerHit;
    private RaycastHit floorHit;
    public bool longAttackRaycastHitSomeThing;
    private bool cameraIsCollision;
    bool cameraCanChangeMovement = false;

    private float moveRotation_Y = 0;
    bool cameraRaycastHitFloor
    {
        get
        {
            return Physics.Linecast(transform.position, transform.position - new Vector3(0, 1, 0), out floorHit, FloorMask);
        }

    }
    bool playerRaycastHitSomeThing
    {
        get
        {
            return Physics.Linecast(PlayerController.playerController.PlayerCollider[1].bounds.center, transform.position, out playerHit, WallMask);
        }
    }

    // Use this for initialization
    void Start()
    {
        mainCamera = this;
        WallMask = LayerMask.GetMask("Wall");
        EnemyLayerMask = LayerMask.GetMask("Enemy");
        FloorMask = LayerMask.GetMask("Floor");

    }
    private void Update()
    {

      //  Debug.Log(cameraCanChangeMovement);
        CameraCollision();
        CollisionFloor();
        Rotaion();
    }

    // Update is called once per frame
    void LateUpdate()
    {

        Vector3 nowpos;
        distenceControl();
        nowpos = rotationEuler * new Vector3(0, 0, -distence) + PlayerController.playerController.Player_pre_pos.position;

        if (nowpos.y <= 0.5f)
        {
            nowpos = new Vector3(nowpos.x, 0.5f, nowpos.z);
        }

        transform.position = nowpos;
        
        // transform.position = Vector3.Lerp(transform.position, rotationEuler * new Vector3(0, 0, -distence) + PlayerController.playerController.Player_pre_pos.position, 1f);

       

        Debug.DrawLine(PlayerController.playerController.PlayerCollider[1].bounds.center, transform.position, Color.green);
        Debug.DrawLine(transform.position, transform.position - new Vector3(0, 1, 0), Color.red);
    }
    private void Rotaion()
    {
        Quaternion nowRotation;       
        CameraLookAt_X += Input.GetAxis("Mouse X") * PlayerController.playerController.RotationSpeed * Time.deltaTime;
       
        if (cameraCanChangeMovement)
        {
            if (Input.GetAxis("Mouse Y") >= 0)
            {              
               
                distence -= Input.GetAxis("Mouse Y") * Time.deltaTime * 4;
                //  cameraIsCollision = true;
                cameraIsCollision = true;

                CameraLookAt_Y -= Input.GetAxis("Mouse Y") * Time.deltaTime * 10;

            }
        }
        else
        {
            moveRotation_Y -= Input.GetAxis("Mouse Y") * RotateSpeed_Y * Time.deltaTime;
            CameraLookAt_Y = moveRotation_Y;
        }

        if (CameraLookAt_X > 360)
        {
            CameraLookAt_X -= 360;
        }
        else if (CameraLookAt_X < 0)
        {
            CameraLookAt_X += 360;
        }

        CameraLookAt_Y = Mathf.Clamp(CameraLookAt_Y, -30, 35);
        moveRotation_Y = Mathf.Clamp(CameraLookAt_Y, -30, 35);


        /* if(cameraRaycastHitFloor && floorHit.distance <= 0.5f && Input.GetAxis("Mouse Y") > 0)
         {
             rotationEuler = Quaternion.Euler(5, CameraLookAt_X, 0);
         }
         else
         {

         }*/
        rotationEuler = Quaternion.Euler(moveRotation_Y, CameraLookAt_X, 0);
        nowRotation = Quaternion.Euler(CameraLookAt_Y, CameraLookAt_X, 0);
        transform.rotation = nowRotation;        
    }

    private void CollisionFloor()
    {
        if (cameraRaycastHitFloor && floorHit.distance <= 0.5f && Input.GetAxis("Mouse Y") > 0 && !cameraCanChangeMovement)
        {
            cameraCanChangeMovement = true;
           // Debug.Log("dd");
           
        }
        else
        {
            if (Input.GetAxis("Mouse Y") < 0) 
            {
                cameraCanChangeMovement = false;
            }           
        }

    }

    private void CameraCollision()
    {
        //playerRaycastHitSomeThing = Physics.Linecast(PlayerController.playerController.PlayerCollider[1].center, transform.position, out playerHit);

        if (playerRaycastHitSomeThing) 
        {

            if(distence != playerHit.distance && !cameraCanChangeMovement)
            {
                distence = Mathf.Lerp(distence, playerHit.distance, 0.1f);
                //distence = Mathf.Clamp(distence, 1, playerHit.distance);                
            }
           
           
           

            cameraIsCollision = true;
            
        }
        else
        {
            if ((distence != preDistence && cameraIsCollision && !cameraCanChangeMovement) || (!cameraCanChangeMovement && cameraIsCollision)) 
            {
                Debug.Log("aa");

             /*   if (CameraLookAt_Y != moveRotation_Y)
                {
                    CameraLookAt_Y = Mathf.Lerp(distence, CameraLookAt_Y, moveRotation_Y);
                }
                */
                distence = Mathf.Lerp(distence, preDistence, 0.1f);

                if (preDistence - distence < 0.1f && CameraLookAt_Y - moveRotation_Y < 0.1f) 
                {
                    distence = preDistence;
                    cameraIsCollision = false;
                }

            }

        }

    }

    private void distenceControl()
    {

        distence -= Input.GetAxis("Mouse ScrollWheel") * distenceSpeed * Time.deltaTime;

        distence = Mathf.Clamp(distence, 1, Max_distence);

    }

    public Vector3 GetAimTarget()
    {
        RaycastHit RayHitPoint;
        aimPoint = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        longAttackRaycastHitSomeThing = Physics.Raycast(transform.position, transform.forward);

        if (longAttackRaycastHitSomeThing)
        {
            Physics.Raycast(transform.position, transform.forward, out RayHitPoint);
            Debug.Log(RayHitPoint.point + RayHitPoint.transform.name);

            return RayHitPoint.point;

        }
        else
        {
            return transform.forward;

        }



    }




}