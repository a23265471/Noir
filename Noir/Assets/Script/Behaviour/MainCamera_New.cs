﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCamera_New : MonoBehaviour
{

    // public GameObject target;
    //  public PlayerController playerController;
    public static MainCamera_New mainCamera;
    private PlayerBehaviour playerBehavior;
    private GameStageController gameStageController;
    private GameStageData gameStageData;


    private Quaternion rotationEuler;
    private float cameraPreRotation_Y;
    public float RotateSpeed_Y;
    public float CameraLookAt_X;
    private float CameraLookAt_Y;
 
    public float distence;
    public float Max_distence;
    public float Min_distence;
    private float changeCameraHigh;
    public float distenceSpeed;
    public float preDistence;

    private int WallMask;
    private int FloorMask;   
    private int EnemyLayerMask;

    public Ray aimPoint;
    private RaycastHit playerHit;
    private RaycastHit floorHit;
    public Camera camera;

    public bool longAttackRaycastHitSomeThing;
    private bool cameraIsCollision;
    bool cameraCanChangeMovement = false;
    private float moveRotation_Y = 0;

    IEnumerator cameraShakeCoroutine;
    Vector3 cameraShakePos;

    public GameObject ConcentricPrefab;
    private Image concentric;

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
            return Physics.Linecast(playerBehavior.GroudedCollider.bounds.center, transform.position, out playerHit, WallMask);
        }
    }


    private void Awake()
    {
        gameStageController = GameFacade.GetInstance().gameStageController;
        gameStageData = GameFacade.GetInstance().gameStageData;

        concentric = Instantiate(ConcentricPrefab, GameObject.FindGameObjectWithTag("UI").transform).GetComponent<Image>();

        mainCamera = this;

    }

    void Start()
    {
        WallMask = LayerMask.GetMask("Wall");
        EnemyLayerMask = LayerMask.GetMask("Enemy");
        FloorMask = LayerMask.GetMask("Floor");

        playerBehavior = gameStageController.playerBehaviour;
        cameraShakeCoroutine = null;

        camera = GetComponent<Camera>();
    }
    private void Update()
    {
        CameraCollision();
        CollisionFloor();

    }

    void LateUpdate()
    {
        Rotaion();
        Vector3 nowpos;
        distenceControl();
        nowpos = rotationEuler * new Vector3(0, 0, -distence) + playerBehavior.cameraLookAt.position;

        if (nowpos.y <= 0.5f)
        {
            nowpos = new Vector3(nowpos.x, 0.5f, nowpos.z);
        }

        transform.position = nowpos + cameraShakePos;

        concentric.transform.position = camera.WorldToScreenPoint(transform.position+transform.rotation * new Vector3(0, 0,30));

    }
    private void Rotaion()
    {
        Quaternion nowRotation;
        

        CameraLookAt_X += Input.GetAxis("Mouse X") * gameStageData.CurPlayerStageData.playerData.playerParameter.moveParameter.RotateSpeed * Time.deltaTime;
       
        if (cameraCanChangeMovement)
        {
            if (Input.GetAxis("Mouse Y") >= 0)
            {              
               
                distence -= Input.GetAxis("Mouse Y") * Time.deltaTime * 4;
                cameraIsCollision = true;
                CameraLookAt_Y -= Input.GetAxis("Mouse Y") * Time.deltaTime * 10;
               
            }
        }
        else
        {
            moveRotation_Y -= Input.GetAxis("Mouse Y") * RotateSpeed_Y * Time.deltaTime;

            if (CameraLookAt_Y != moveRotation_Y)
            {
                CameraLookAt_Y = Mathf.Lerp(CameraLookAt_Y, moveRotation_Y, 0.5f);
                CameraLookAt_Y = Mathf.Clamp(CameraLookAt_Y, moveRotation_Y, CameraLookAt_Y);
            }
            else
            {
                CameraLookAt_Y = moveRotation_Y;
            }
            
               
            
            cameraPreRotation_Y = CameraLookAt_Y;
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
        moveRotation_Y = Mathf.Clamp(moveRotation_Y, -30, 35);

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
            if (Input.GetAxis("Mouse Y") < -0.2f) 
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
                

              /*  if (CameraLookAt_Y != moveRotation_Y)
                {
                    CameraLookAt_Y = Mathf.Lerp(CameraLookAt_Y, moveRotation_Y, 0.1f);
                    Debug.Log("ss");
                }*/
                
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
        if (!cameraCanChangeMovement && !cameraIsCollision)
        {
            preDistence = distence;
        }
    }

    public Vector3 GetAimTarget()
    {
        RaycastHit RayHitPoint;
        aimPoint = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        longAttackRaycastHitSomeThing = Physics.Raycast(aimPoint.origin, transform.forward, out RayHitPoint);

       /* if (longAttackRaycastHitSomeThing)
        {
          //  Physics.Raycast(transform.position, transform.forward, out RayHitPoint);
           // Debug.Log(RayHitPoint.point + RayHitPoint.transform.name);
            Debug.DrawLine(aimPoint.origin, RayHitPoint.point, Color.red);

            return RayHitPoint.point;

        }
        else
        {*/

            return transform.position;

      //  }
    


    }

    public void CameraShake(float shakeRang, float shakeTime)
    {
        if (cameraShakeCoroutine != null)
        {
            StopCoroutine(cameraShakeCoroutine);

        }

        cameraShakeCoroutine = cameraShake(shakeRang, shakeTime);
        StartCoroutine(cameraShakeCoroutine);


    }

    IEnumerator cameraShake(float shakeRang,float shakeTime)
    {

        float pastTime = 0.0f;
        
        while(pastTime < shakeTime)
        {
            Vector3 currentPos = transform.position;

            float x = Random.Range(-1f, 1f) * shakeRang;
            float y = Random.Range(-1f, 1f) * shakeRang;

            cameraShakePos = new Vector3(x, y, 0);
            pastTime += Time.deltaTime;
           // Debug.Log(cameraShakePos);

            yield return null;

        }
        //Vector3 originalPos = transform.position;

        cameraShakePos = new Vector3(0,0,0);

    }



}