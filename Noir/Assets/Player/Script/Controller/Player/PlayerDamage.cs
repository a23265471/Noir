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
            PlayerController.playerController.playerAnimatorState = PlayerController.PlayerAnimatorState.GetDown;
            gameObject.SetActive(false);
            PlayerController.playerController.animator.SetTrigger("Damage_GetDown");
        }
        else if (other.tag == "EnemyAttack_Big")
        {
            PlayerController.playerController.playerAnimatorState = PlayerController.PlayerAnimatorState.Damage;
            PlayerController.playerController.animator.SetTrigger("Damage_Big");
            EnemyController.enemyController.PlayerisDamage = true;
            UI_HP.Ui_HP.HP -= 40;
        }
        else if (other.tag == "EnemyAttack_Small")
        {
            PlayerController.playerController.playerAnimatorState = PlayerController.PlayerAnimatorState.Damage;
            PlayerController.playerController.animator.SetTrigger("Damage_Small");
            EnemyController.enemyController.PlayerisDamage = true;
            UI_HP.Ui_HP.HP -= 20;
        }
    }

    
}
