using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_FollowEnemy : MonoBehaviour {

    
    public float HP_Max;    
    public float HP; 
    public GameObject HP_Light;
    private Vector3 UI_pos;
    public EnemyController enemyController;
    public EnemyBehaviour enemyBehaviour;
    public bool UIOpened;

    private Image hpImage;

    private void Awake()
    {
        hpImage = GetComponent<Image>();
    }


    void Update ()
    {

        HP_Light.transform.localPosition = new Vector3(-298f + (HP / HP_Max * 298f), 0, 0);

        if (HP <= 0 && enemyController.enemyState != EnemyController.EnemyState.Dead) 
        {
            enemyController.Dead();
        }
	}

    public void OpenUI()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        UIOpened = true;
    }

    public void CloseUI()
    {
        // hpImage.enabled = false;
        transform.rotation = Quaternion.Euler(0, 90, 0);
        UIOpened = false;
    }

    public void DestroyUI()
    {
        Destroy(gameObject);
    }

}
