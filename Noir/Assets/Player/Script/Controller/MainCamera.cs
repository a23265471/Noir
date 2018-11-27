using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {

    // public GameObject target;
    //  public PlayerController playerController;
    public static MainCamera mainCamera;
    
    private Quaternion rotationEuler;
    public float RotateSpeed_X;
    public float RotateSpeed_Y;
    private float CameraLookAt_X;
    private float CameraLookAt_Y;
    public float distence;
    public float Max_distence;
    public float Min_distence;
    public float CameraHigh;
    public float distenceSpeed;
    public float Camera_Wall_distence;
    private int WallMask;
    private int FloorMask;
    public float CameraHitWallDis;
    public float preDistence;
    private int EnemyLayerMask;

    public Ray aimPoint;
    private RaycastHit playerHit;
    public bool longAttackRaycastHitSomeThing;
    private bool cameraIsCollision;
    bool playerRaycastHitSomeThing
    {
        get
        {
          
            
            return Physics.Linecast(PlayerController.playerController.PlayerCollider[1].bounds.center, transform.position, out playerHit);
        }
    }

    // Use this for initialization
    void Start () {
        mainCamera = this;
        WallMask = LayerMask.GetMask("Wall");
        EnemyLayerMask = LayerMask.GetMask("Enemy");
        FloorMask = LayerMask.GetMask("Floor");
    }
    private void Update()
    {
        CameraCollision();
    }

    // Update is called once per frame
    void LateUpdate () {        

        
        Rotaion();
        distenceControl();
        transform.position = rotationEuler * new Vector3(0, 0, -distence) + PlayerController.playerController.Player_pre_pos.position;
        Debug.DrawLine(PlayerController.playerController.PlayerCollider[1].bounds.center, transform.position, Color.green);
       
    }
    private void Rotaion()
    {
        CameraLookAt_X += Input.GetAxis("Mouse X") * PlayerController.playerController.RotationSpeed * Time.deltaTime;
        CameraLookAt_Y -= Input.GetAxis("Mouse Y") * RotateSpeed_Y * Time.deltaTime;

        if (CameraLookAt_X > 360)
        {
            CameraLookAt_X -= 360;
        }
        else if (CameraLookAt_X < 0)
        {
            CameraLookAt_X += 360;
        }

        if (CameraLookAt_Y > 35)
        {
            CameraLookAt_Y = 35;
        }
        else if (CameraLookAt_Y < -30)
        {
            CameraLookAt_Y = -30;
        }
        rotationEuler = Quaternion.Euler(CameraLookAt_Y, CameraLookAt_X, 0);
        transform.rotation = rotationEuler;
        

    }


    private void CameraCollision()
    {
        //playerRaycastHitSomeThing = Physics.Linecast(PlayerController.playerController.PlayerCollider[1].center, transform.position, out playerHit);

        if (playerRaycastHitSomeThing)
        {
            distence = Mathf.Lerp(distence, playerHit.distance, 0.1f);
            Debug.Log("aa");
            cameraIsCollision = true;
        }
        else
        {
            if (distence != preDistence && cameraIsCollision)
            {
                distence = Mathf.Lerp(distence, preDistence, 0.1f);

                if (preDistence - distence < 0.1f)
                {
                    distence = preDistence;
                    cameraIsCollision = false;
                }
                Debug.Log("dis  " + distence + "pre  " + preDistence);
            }
           
        }

    }

    private void distenceControl()
    {
        
        distence -= Input.GetAxis("Mouse ScrollWheel") * distenceSpeed * Time.deltaTime;
        distence = Mathf.Clamp(distence, Min_distence, Max_distence);
        if (!cameraIsCollision)
        {
            preDistence = distence;
        }
        


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
