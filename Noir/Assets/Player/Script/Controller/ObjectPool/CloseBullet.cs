using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseBullet : MonoBehaviour {

    public float DistroyTime;//PlayerData

    private float LongAttackStartTime;
    private float LongAttackNowDis;
    private float LongAttackFracDistance;
    // Use this for initialization
    private void OnEnable()
    {
        LongAttackStartTime = Time.time;
    }

	// Update is called once per frame
	void Update ()
    {
        LongAttackNowDis = (Time.time - LongAttackStartTime) * PlayerController.playerController.LongAttackSpeed;
        LongAttackFracDistance = LongAttackNowDis / PlayerController.playerController.LongAttackMaxDis;
        LongAttackFracDistance = Mathf.Clamp(LongAttackFracDistance, 0, 1);
        transform.position = Vector3.Lerp(transform.position, PlayerController.playerController.LongAttackEndPos, LongAttackFracDistance);


        if (LongAttackFracDistance >= 0.2f)
        {
            gameObject.SetActive(false);
            
        }
        Debug.Log(LongAttackFracDistance);
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall" || other.tag == "Enemy")
        {
           
            StartCoroutine("LongAttackHitWaitTime");
        }


    }
    IEnumerator LongAttackHitWaitTime()
    {
        yield return new WaitForSeconds(DistroyTime);
        gameObject.SetActive(false);        
    }
}
