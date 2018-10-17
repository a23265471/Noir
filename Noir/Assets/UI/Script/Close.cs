using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Close : MonoBehaviour {
   

	// Use this for initialization
	void Update () {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            gameObject.SetActive(false);
            
        }
    }
	
}
