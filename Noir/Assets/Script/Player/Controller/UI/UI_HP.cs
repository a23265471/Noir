using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_HP : MonoBehaviour
{
    public static UI_HP Ui_HP;

    private IEnumerator DarkBarControlCoroutine;

    public GameObject HP_Light;
    public GameObject HP_Dark;
    public GameObject[] SP_Light;
    public float DarkBarSpeed;

    public float HP_Max;
    public float SP_Max;
    
    public float HP;
    private float DarkHP;
    public float SP;
    private float[] SP_small;

    private bool darkBarCanMove;


    private void Awake()
    {
        DarkBarControlCoroutine = null;
    }

    void Start ()
    {
        Ui_HP = this;
        HP = HP_Max;
        SP = SP_Max;
        SP_small = new float[SP_Light.Length];
        ConsumeSP();
        darkBarCanMove = false;
        DarkHP = HP;
    }
	
	void Update ()
    {
        EnergyBarControl(HP_Light, HP, HP_Max, 0);      

        if (HP <= 0)
        {
            PlayerBehaviour.playerBehaviour.Dead();

        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            HP -= 10;
            DarkBarStartControl();
        }
        DarkBarMove();
       

    }

    public void EnergyBarControl(GameObject EnergrBar,float CurEnergy,float MaxEnergy,float MinEnergy)
    {
        EnergrBar.transform.localPosition = new Vector3(-1029 + (CurEnergy / MaxEnergy * 1029), 0, 0);
        CurEnergy = Mathf.Clamp(CurEnergy, MinEnergy , MaxEnergy);
       
    }

    public void DarkBarStartControl()
    {
        DarkBarControlCoroutine = DarkBarStartControl(1.2f);
        darkBarCanMove = false;
        StopCoroutine(DarkBarControlCoroutine);
        StartCoroutine(DarkBarControlCoroutine);
        
    }
    private void DarkBarMove()
    {
        if (darkBarCanMove)
        {
            DarkHP = Mathf.Lerp(DarkHP, HP, 0.2f);
            EnergyBarControl(HP_Dark, DarkHP, HP_Max, HP);
            if (DarkHP >= HP && DarkHP < (HP+0.1f)) 
            {
                darkBarCanMove = false;
                DarkHP = HP;
            }
        }
        
    }

    IEnumerator DarkBarStartControl(float waitSec)
    {
        yield return new WaitForSeconds(waitSec);
        darkBarCanMove = true;
    }

    public void ConsumeHp(float consumeHP)
    {
        HP -= consumeHP;
        DarkBarStartControl();

    }


    public void ConsumeSP()
    {

        if (SP <= 0)
        {
            SP = 0;
        }
        for(int i = 0; i < (int)(SP / (SP_Max / SP_Light.Length)); i++)
        {
            SP_small[i] = SP_Max / SP_Light.Length;

        }

        if (SP % (SP_Max / SP_Light.Length) != 0)
        {
            float SP_transformPosition;
            int notFullSp;

            notFullSp = (int)(SP / (SP_Max / SP_Light.Length));
            SP_small[notFullSp] = SP % (SP_Max / SP_Light.Length);

            SP_transformPosition = -68.9f + ((SP_small[notFullSp] / (SP_Max / SP_Light.Length)) * 68.9f);
            SP_Light[notFullSp].transform.localPosition = new Vector3(SP_transformPosition, 0, 0);

            for(int i = notFullSp + 1; i < SP_Light.Length; i++)
            {
                SP_Light[i].transform.localPosition = new Vector3(-68.9f, 0, 0);
            }

        }
        else if(SP % (SP_Max / SP_Light.Length) == 0 && SP != SP_Max) 
        {
            SP_Light[(int)(SP / (SP_Max / SP_Light.Length))].transform.localPosition = new Vector3(-68.9f, 0, 0);
        }

    }

    public void Consumesp(int consumSp)
    {
        SP -= consumSp;

        ConsumeSP();
        StartRecoverySp();
    }

    public bool RecoverySP(float recoverySP)
    {
        SP += recoverySP;
        ConsumeSP();

        if (SP >= SP_Max)
        {
            SP = SP_Max;
            return true;
        }
        else
        {
            return false;
        }

    }

    public void StartRecoverySp()
    {
        StopCoroutine("recoverySP");
        StartCoroutine("recoverySP");
    }

    IEnumerator recoverySP()
    {
        yield return new WaitUntil(() => RecoverySP(0.1f));

        Debug.Log("SP is Fill");
    }

}

    

