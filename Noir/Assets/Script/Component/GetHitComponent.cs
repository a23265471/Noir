using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GetHitComponent : MonoBehaviour
{
    public Action<float, string> DamageFunction;
    public int GetHitCombo;

    private void Start()
    {
        GetHitCombo = 0;
    }

    public void TriggerDamage(float DamageInfo, string animatorTrigger)
    {
        DamageFunction(DamageInfo, animatorTrigger);


    }

   /* private void OnTriggerEnter(Collider other)//判斷是否被攻擊
    {
        if (other.CompareTag("Attack"))
        {
            TriggerDamage(AttackSystem.attackSystem.currentAttackInfo.AttackPower, AttackSystem.attackSystem.currentAttackInfo.DamageAnimator);


        }

    }*/
}
