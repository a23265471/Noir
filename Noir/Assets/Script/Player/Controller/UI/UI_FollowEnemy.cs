using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_FollowEnemy : MonoBehaviour {

    
    public float HP_Max;    
    public float HP; 
    public GameObject HP_Light;
    public GameObject Enemy;
    private Vector3 UI_pos;
    private GameObject maincamera;
    private EnemyController enemyController;

    // Use this for initialization
    void Start ()
    {
       
        maincamera = GameObject.Find("Main Camera");
        enemyController = Enemy.GetComponent<EnemyController>();

    }
	
	// Update is called once per frame
	void Update ()
    {

        /*UI_pos = new Vector3(0, 2.5f, 0) +Enemy.transform.position;
        GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(UI_pos);*/
        HP_Light.transform.localPosition = new Vector3(-2.006f + (HP / HP_Max * 2.006f), 0, 0);

        transform.rotation = Quaternion.LookRotation(transform.position - maincamera.transform.position);

        if (HP <= 0 && enemyController.enemyState != EnemyController.EnemyState.Dead) 
        {
            enemyController.Dead();
        }
	}

    
}
