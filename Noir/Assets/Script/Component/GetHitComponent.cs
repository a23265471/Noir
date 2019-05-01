using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GetHitComponent : MonoBehaviour
{
    public Action<int> DamageFunction;
    public int GetHitCombo;

    private void Start()
    {
        GetHitCombo = 0;
    }

    public void TriggerDamage(int DamageInfo)
    {
        DamageFunction(DamageInfo);

        Debug.Log("jjj");

    }


	
}
