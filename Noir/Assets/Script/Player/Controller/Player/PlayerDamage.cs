using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour {

	// Use this for initialization
	void Start () {  
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyAttack_GetDown")
        {
            PlayerControllerOld.playerControllerOld.playerAnimatorState = PlayerControllerOld.PlayerAnimatorState.GetDown;
            gameObject.SetActive(false);
            PlayerControllerOld.playerControllerOld.animator.SetTrigger("Damage_GetDown");
            PlayerControllerOld.playerControllerOld.DamageAudioPlay();
            UI_HP.Ui_HP.DarkBarStartControl();

        }
        else if (other.tag == "EnemyAttack_Big")
        {
            PlayerControllerOld.playerControllerOld.playerAnimatorState = PlayerControllerOld.PlayerAnimatorState.Damage;
            PlayerControllerOld.playerControllerOld.animator.SetTrigger("Damage_Big");
            EnemyController.enemyController.PlayerisDamage = true;
            PlayerControllerOld.playerControllerOld.DamageAudioPlay();
            UI_HP.Ui_HP.HP -= 40;
            UI_HP.Ui_HP.DarkBarStartControl();
        }
        else if (other.tag == "EnemyAttack_Small")
        {
            PlayerControllerOld.playerControllerOld.playerAnimatorState = PlayerControllerOld.PlayerAnimatorState.Damage;
            PlayerControllerOld.playerControllerOld.animator.SetTrigger("Damage_Small");
            EnemyController.enemyController.PlayerisDamage = true;
            PlayerControllerOld.playerControllerOld.DamageAudioPlay();
            UI_HP.Ui_HP.HP -= 20;
            UI_HP.Ui_HP.DarkBarStartControl();
        }
    }

    
}
