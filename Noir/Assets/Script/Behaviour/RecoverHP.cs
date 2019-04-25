using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoverHP : MonoBehaviour
{

    public int RecoverHPValue;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UI_HP.Ui_HP.RecoverHP(RecoverHPValue);
            PlayerBehaviour.playerBehaviour.EffectPlay("RecoverFx");
            gameObject.SetActive(false);
        }


    }




}
