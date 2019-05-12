using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GetHitComponent : MonoBehaviour
{
    public Action<float, string> DamageFunction;
    public int GetHitCombo;
    public int CurrentGetHitCombo;

   // public int currentCombo;

    private void Start()
    {
        GetHitCombo = 0;
    }

    public void TriggerDamage(float DamageInfo, string animatorTrigger)
    {
        DamageFunction(DamageInfo, animatorTrigger);


    }

    public void ResetGetHitCombo(int currentAttackCombo)
    {
        GetHitCombo = currentAttackCombo;
    }

    /* private void OnTriggerEnter(Collider other)//判斷是否被攻擊
     {
         if (other.CompareTag("Attack"))
         {
             TriggerDamage(AttackSystem.attackSystem.currentAttackInfo.AttackPower, AttackSystem.attackSystem.currentAttackInfo.DamageAnimator);


         }

     }*/
}
