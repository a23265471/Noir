using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_FollowEnemy : MonoBehaviour {

    public static UI_FollowEnemy ui_FollowEnemy;
    public float HP_Max;    
    public float HP;

    public GameObject HP_Light;
    private GameObject Enemy;
    private Vector3 UI_pos;
    private GameObject maincamera;
	// Use this for initialization
	void Start ()
    {
        ui_FollowEnemy = this;
        Enemy = GameObject.Find("Enemy_ShortAttack");
        maincamera = GameObject.Find("Main Camera");
	}
	
	// Update is called once per frame
	void Update ()
    {

        /*UI_pos = new Vector3(0, 2.5f, 0) +Enemy.transform.position;
        GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(UI_pos);*/
        HP_Light.transform.localPosition = new Vector3(-2.006f + (HP / HP_Max * 2.006f), 0, 0);

        transform.rotation = Quaternion.LookRotation(transform.position - maincamera.transform.position);

        if (HP <= 0 && EnemyController.enemyController.enemyState != EnemyController.EnemyState.Dead) 
        {
            EnemyController.enemyController.Dead();
        }
	}

    
}
