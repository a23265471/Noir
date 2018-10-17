using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour {
  public GameObject Tutorial;
	// Use this for initialization
	void Start () {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Tutorial.SetActive(true);
        }
    }
   
  
}
