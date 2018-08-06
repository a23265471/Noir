using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {

    public GameObject target;
    public float MoveSpeed_Y;
    public float MoveSpeed_X;
    private Vector3 OffsetPos;
    private Quaternion rotationEuler;
    public Transform PlayerBack;

    private Transform initialTransform;
    
    public float RotateSpeed_X;
    public float RotateSpeed_Y;
    private float CameraLookAt_X;
    private float CameraLookAt_Y;
    private float direction_Y;
    private float direction_X;
    public float distence;
    public float Max_distence;
    public float Min_distence;
    public float CameraHigh;
    public float distenceSpeed;

    // Use this for initialization
    void Start () {
        OffsetPos = transform.position - target.transform.position;
        initialTransform = transform;
    }
	
	// Update is called once per frame
	void LateUpdate () {
        
        Rotaion();
        distenceControl();
    }

    private void Rotaion()
    {
        float rotate_y = Input.GetAxis("Mouse Y");
       
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
        distence -= Input.GetAxis("Mouse ScrollWheel") * distenceSpeed * Time.deltaTime;
        distence = Mathf.Clamp(distence, Min_distence, Max_distence);
    }

}
