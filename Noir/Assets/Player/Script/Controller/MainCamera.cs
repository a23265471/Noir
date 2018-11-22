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
    public float CameraHitWallDis;
    public float preDistence;
    private int EnemyLayerMask;

    public Ray aimPoint;
    public bool longAttackRaycastHitSomeThing;
    private bool playerRaycastHitSomeThing;
    private bool cameraRaycastHitSomeThing;

    // Use this for initialization
    void Start () {
        mainCamera = this;
        WallMask = LayerMask.GetMask("Wall");
        EnemyLayerMask = LayerMask.GetMask("Enemy");
    }
	
	// Update is called once per frame
	void LateUpdate () {        

        
        Rotaion();
        distenceControl();
        transform.position = rotationEuler * new Vector3(0, 0, -distence) + PlayerController.playerController.Player_pre_pos.position;
        Debug.DrawRay(transform.position, transform.forward , Color.red);
       
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
    private void distenceControl()
    {      
        RaycastHit cameraHit;
        RaycastHit playerHit;

        playerRaycastHitSomeThing = Physics.Raycast(PlayerController.playerController.transform.position,transform.position, distence);
        

        if (playerRaycastHitSomeThing)
        {            
            Physics.Raycast(PlayerController.playerController.transform.position, transform.position, out playerHit);
            distence = Mathf.Lerp(distence, (playerHit.distance - 1f), 0.1f);
            distence = Mathf.Clamp(distence, Min_distence, Max_distence);
        }
        else
        {
            if (distence != preDistence)
            {
                
                if (distence < preDistence)
                {
                    distence += 0.1f;
                    if (distence >= preDistence)
                    {
                        distence = preDistence;
                    }
                }
            }
            else
            {
                distence -= Input.GetAxis("Mouse ScrollWheel") * distenceSpeed * Time.deltaTime;
                distence = Mathf.Clamp(distence, Min_distence, Max_distence);
                preDistence = distence;
            }
             
        }
       /* Debug.DrawLine(playerController.Player_pre_pos.position, -playerController.Player_pre_pos.forward, Color.red);*/
       
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
