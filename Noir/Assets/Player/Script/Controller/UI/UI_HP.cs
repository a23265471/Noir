using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_HP : MonoBehaviour
{
    public static UI_HP Ui_HP;

    public GameObject HP_Light;
    public GameObject MP_Light;
    //public GameObject SP_Light;

    public float HP_Max;
    public float MP_Max;
    public float SP_Max;

    public float HP;
    public float MP;
    private float SP;

    // Use this for initialization
    void Start ()
    {
        Ui_HP = this;
        HP = HP_Max;
        MP = MP_Max;
        SP = SP_Max;


	}
	
	// Update is called once per frame
	void Update ()
    {
        HP_Light.transform.localPosition = new Vector3(-387 + (HP / HP_Max * 387), 0, 0);
        MP_Light.transform.localPosition = new Vector3(-387 + (MP / MP_Max * 387), 0, 0);

        if (HP <= 0)
        {            
            HP = 0;
            PlayerController.playerController.Dead();
        }
       
	}

}
