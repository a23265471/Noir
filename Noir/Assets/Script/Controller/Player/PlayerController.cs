using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //public float MoveSpeed;
    public float RotationSpeed;
    private float RotationX;
    private float RotationY;
    public Quaternion rotationEuler;
    private Animator animator;
    private float RunSpeed;
    public Transform camera_pre_pos;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        
        Rotaion();
        Movement();
    }

    private void Movement()
    {
        /* float MoveX = Input.GetAxis("Horizontal") * Time.deltaTime * MoveSpeed;
         float MoveZ = Input.GetAxis("Vertical") * Time.deltaTime * MoveSpeed;
         // transform.Translate(MoveX, 0, MoveZ);*/

        /* if (Input.GetAxis("Horizontal") != 0 && Input.GetAxis("Vertical") == 0)
          {
              animator.SetFloat("Run_Left_Right", Input.GetAxis("Horizontal"));
          }
          else
          {
              animator.SetBool("Run_Left", false);
              animator.SetFloat("RunSpeed", Input.GetAxis("Vertical"));
          }*/
        
        animator.SetFloat("Action_Contrll", 0f);
        animator.SetFloat("RunSpeed_Horizontal", Input.GetAxis("Horizontal"));
        animator.SetFloat("RunSpeed_Vertical", Input.GetAxis("Vertical"));

        if (Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Vertical") < 0)
        {
            transform.rotation = Quaternion.Euler(0, -45, 0);

            Debug.Log("a");
        }
        else
        {
            camera_pre_pos = transform;
        }
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
