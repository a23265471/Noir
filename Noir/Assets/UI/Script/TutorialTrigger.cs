using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour {
    public GameObject Tutorial;
	// Use this for initialization
	void Start () {
        Tutorial.SetActive(false);
    }
    public void OnTriggerEnter(Collider du)
    {
        if(du.gameObject.name=="Tutorial_Collider")
        Tutorial.SetActive(true);  
    }
    public void Update()
    {
        if (Input.GetMouseButtonDown(0)|| Input.GetMouseButtonDown(1))
        {
            Tutorial.SetActive(false);
            gameObject.SetActive(false);
        }
    }

}
