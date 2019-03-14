using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackRain : MonoBehaviour {

    private BoxCollider box;

	// Use this for initialization
	void Start () {
        box = GetComponent<BoxCollider>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine("CloseObject");
           

        }




    }

    IEnumerator CloseObject()
    {

        yield return new WaitForSeconds(0.5f);
        box.enabled = false;
        StartCoroutine("OpenObject");
    }

    IEnumerator OpenObject()
    {
        yield return new WaitForSeconds(2);
        box.enabled = true;
    }

}
