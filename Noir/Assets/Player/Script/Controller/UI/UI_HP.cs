using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_HP : MonoBehaviour
{
    public static UI_HP Ui_HP;

    public GameObject HP_Light;
    public GameObject MP_Light;
    public GameObject[] SP_Light;
    //public GameObject SP_Light;

    public float HP_Max;
    public float MP_Max;
    public float SP_Max;
    
    public float HP;
    public float MP;
    public float SP;
    private float[] SP_small;

    // Use this for initialization
    void Start ()
    {
        Ui_HP = this;
        HP = HP_Max;
        MP = MP_Max;
        SP = SP_Max;
        SP_small = new float[SP_Light.Length];
        ConsumeSP();

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
        if (Input.GetKeyDown(KeyCode.R))
        {
            MP = MP_Max;
        }
       
      /*  if (Input.GetMouseButton(0))
        {
            SP -= 0.5f;
            ConsumeSP();
        }*/

    }

    public void ConsumeSP()
    {

        if (SP <= 0)
        {
            SP = 0;
        }
        {for(int i = 0; i < (int)(SP / (SP_Max / SP_Light.Length)); i++)
        {
            SP_small[i] = SP_Max / SP_Light.Length;

        }

        if (SP % (SP_Max / SP_Light.Length) != 0)
        {
            float SP_transformPosition;
            int notFullSp;

            notFullSp = (int)(SP / (SP_Max / SP_Light.Length));
            SP_small[notFullSp] = SP % (SP_Max / SP_Light.Length);

            SP_transformPosition = -72 + ((SP_small[notFullSp] / (SP_Max / SP_Light.Length)) * 72);
            SP_Light[notFullSp].transform.localPosition = new Vector3(SP_transformPosition, 0, 0);

        }
        else if(SP % (SP_Max / SP_Light.Length) == 0 && SP != SP_Max) 
        {
            SP_Light[(int)(SP / (SP_Max / SP_Light.Length))].transform.localPosition = new Vector3(-72, 0, 0);
        }

        }
        
    }

    
}
