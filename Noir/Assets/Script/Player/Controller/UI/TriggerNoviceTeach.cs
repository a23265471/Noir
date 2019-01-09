using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerNoviceTeach : MonoBehaviour
{
    public NoviceTeachingController.NoviceTeachingState NoviceTeachingState;
    public bool SlowMotion;
    public string[] AnimatorTransition;
    public GameObject TriggerNextTeach;
    public bool needToKillAllEnemy;
    public Transform[] CreatEnemyPoint;
    public GameObject Enemy;


    private void OnTriggerEnter(Collider other)
    {
        TriggerNoviceTeach triggerNoviceTeach = GetComponent<TriggerNoviceTeach>();
        if (other.gameObject.CompareTag("Player"))
        {
            if (NoviceTeachingController.noviceTeachingController.enabled == false)
            {           
                NoviceTeachingController.noviceTeachingController.OpenNoviceTeaching(triggerNoviceTeach,NoviceTeachingState, SlowMotion, AnimatorTransition, CreatEnemyPoint,Enemy, TriggerNextTeach);
                gameObject.SetActive(false);

                        
            }
            else
            {
                NoviceTeachingController.noviceTeachingController.ForceCloseNoviceTeaching(SlowMotion);

                Debug.Log("WWW");
                gameObject.SetActive(false);
            }
            
        }
        
    }


}
