using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AttackColliderTrigger : MonoBehaviour
{
    private int currentCombo;
    public AttackSystem attackSystem;
    private EnemyBehaviour enemyBehaviour;
    private PlayerBehaviour playerBehaviour;
    private Action AttackTimeScaleEffect;

    private bool isGetHit;
    //  private int currentbehaviour;

    private void Start()
    {
        GetBehaviour();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<GetHitComponent>() != null)
        {

            if (other.transform.GetComponent<GetHitComponent>().CurrentGetHitCombo < currentCombo)
            {
                other.transform.GetComponent<GetHitComponent>().CurrentGetHitCombo += 1;
                other.transform.GetComponent<GetHitComponent>().TriggerDamage(attackSystem.currentAttackInfo.AttackPower, attackSystem.currentAttackInfo.DamageAnimator);
                
            }
        }
    }

    public void ResetHitCombo(int currentAttackCombo)
    {
        currentCombo = currentAttackCombo;
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Enemy").Length; i++)
        {
            GameObject.FindGameObjectsWithTag("Enemy")[i].GetComponent<GetHitComponent>().CurrentGetHitCombo = 0;
            GameObject.FindGameObjectWithTag("Player").GetComponent<GetHitComponent>().CurrentGetHitCombo = 0;

        }

    }

    public void GetBehaviour()
    {
        if (attackSystem.GetComponent<PlayerBehaviour>() != null)
        {
            playerBehaviour = attackSystem.GetComponent<PlayerBehaviour>();

            AttackTimeScaleEffect = playerBehaviour.AttackTimeScaleEffect;
        }
        else if (attackSystem.GetComponent<EnemyBehaviour>() != null)
        {
            enemyBehaviour = attackSystem.GetComponent<EnemyBehaviour>();
            
        }

    }

}
