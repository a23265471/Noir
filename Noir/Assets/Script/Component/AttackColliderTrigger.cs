using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackColliderTrigger : MonoBehaviour
{
    private int currentCombo;
    public AttackSystem attackSystem;


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<GetHitComponent>() != null)
        {
            if (currentCombo > 0)
            {
                currentCombo -= 1;
                other.transform.GetComponent<GetHitComponent>().TriggerDamage(attackSystem.currentAttackInfo.AttackPower, attackSystem.currentAttackInfo.DamageAnimator);
                Debug.Log("ss");
            }
        }
    }

    public void ResetHitCombo(int currentAttackCombo)
    {
        currentCombo = currentAttackCombo;
    }


}
