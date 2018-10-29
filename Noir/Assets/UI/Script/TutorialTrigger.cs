using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour {
  public GameObject Tutorial;
  public Animator Tutorial_anim;
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
    public void OnTriggerExit(Collider other)
    {
        Tutorial_anim.SetTrigger("CardFade");
        StartCoroutine(WaitTime());
        
    }
    IEnumerator  WaitTime()
    {
        yield return new WaitForSeconds(1f);
        Tutorial.SetActive(false);
        gameObject.SetActive(false);
    }
}
