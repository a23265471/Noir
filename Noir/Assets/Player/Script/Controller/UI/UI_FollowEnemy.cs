using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_FollowEnemy : MonoBehaviour {

    private GameObject Enemy;
    private Vector3 UI_pos;
    private GameObject maincamera;
	// Use this for initialization
	void Start ()
    {
        Enemy = GameObject.Find("Enemy_ShortAttack");
        maincamera = GameObject.Find("Main Camera");
	}
	
	// Update is called once per frame
	void Update ()
    {

        /*UI_pos = new Vector3(0, 2.5f, 0) +Enemy.transform.position;
        GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(UI_pos);*/

        transform.rotation = Quaternion.LookRotation(transform.position - maincamera.transform.position);
	}

    
}
