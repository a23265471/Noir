using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teach : MonoBehaviour {


    Animator animator;
    bool open;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        open = true;
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (open)
            {
                open = false;
                animator.SetTrigger("close");
            }
            else
            {
                open = true ;
                animator.SetTrigger("open");

            }

        }
		
	}
}
