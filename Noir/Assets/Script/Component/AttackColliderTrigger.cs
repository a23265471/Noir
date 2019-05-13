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
    //  private int currentbehaviour;


    private void Start()
    {
        GetBehaviour();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<GetHitComponent>() != null)
        {

           // other.transform.GetComponent<GetHitComponent>().ResetGetHitCombo(currentCombo);
            if (other.transform.GetComponent<GetHitComponent>().CurrentGetHitCombo < currentCombo)
            {
                other.transform.GetComponent<GetHitComponent>().CurrentGetHitCombo += 1;
                other.transform.GetComponent<GetHitComponent>().TriggerDamage(attackSystem.currentAttackInfo.AttackPower, attackSystem.currentAttackInfo.DamageAnimator);
                //AttackTimeScaleEffect();
               // StartCoroutine("AttackEffect");
                // Debug.Log(other.transform.GetComponent<GetHitComponent>().GetHitCombo);
            }
        }
    }

   /* private void OnTriggerExit(Collider other)
    {
        other.transform.GetComponent<GetHitComponent>().CurrentGetHitCombo = 0;

    }*/
    public void ResetHitCombo(int currentAttackCombo)
    {
        currentCombo = currentAttackCombo;
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Enemy").Length; i++)
        {
            GameObject.FindGameObjectsWithTag("Enemy")[i].GetComponent<GetHitComponent>().CurrentGetHitCombo = 0;
        }
        
    }

    /*IEnumerator AttackEffect()
    {
        switch (currentbehaviour)
        {
            case 0:
                playerBehaviour.
                break;

        }

        yield return new WaitForSeconds(0.001f);
        Time.timeScale = 1f;

    }*/

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
