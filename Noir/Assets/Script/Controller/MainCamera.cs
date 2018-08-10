using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {

    public GameObject target;      
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

    // Use this for initialization
    void Start () {

        WallMask = LayerMask.GetMask("Wall");
    }
	
	// Update is called once per frame
	void LateUpdate () {        
        Rotaion();
        distenceControl();
    }

    private void Rotaion()
    {
        CameraLookAt_X += Input.GetAxis("Mouse X") * RotateSpeed_X * Time.deltaTime;
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
        transform.position = rotationEuler * new Vector3(0, CameraHigh , -distence) + target.transform.position;

    }
    private void distenceControl()
    {      
        RaycastHit Hit;
        if (Physics.Raycast(target.transform.position, -target.transform.forward, preDistence, WallMask))
        {
            Physics.Raycast(target.transform.position, -target.transform.forward, out Hit);
            // Debug.Log(Hit.distance);
            distence = Mathf.Lerp(distence, Hit.distance, 0.1f);
           // distence = Hit.distance;
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
       // Debug.DrawLine(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z), new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z) - (target.transform.forward * distence), Color.red);

       

    }

    
}
