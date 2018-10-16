using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour {
    public GameObject Tutorial;
	// Use this for initialization
	void Start () {
        Tutorial.SetActive(false);
	}
    private void OnTriggerOpen()
    {
        Tutorial.SetActive(true);
        StartCoroutine(OnTriggerClose());
        Tutorial.SetActive(false);

    }
    IEnumerator OnTriggerClose()
    {
        yield return new WaitForSeconds(5f);
    }
}
