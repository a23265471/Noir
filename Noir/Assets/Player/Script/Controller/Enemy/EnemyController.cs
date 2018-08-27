using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Animator EnemyAnimator;
    

    // Use this for initialization
    private void Awake()
    {
        EnemyAnimator = GetComponent<Animator>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)//判斷是否被攻擊
    {
        if (other.tag == "PlayerAttack_Small")
        {
            EnemyAnimator.SetTrigger("Damage_Small");
        }
        else if(other.tag == "PlayerAttack_Big")
        {
            EnemyAnimator.SetTrigger("Damage_Big");
        }

    }

}
