using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerNoviceTeach : MonoBehaviour
{
    public NoviceTeachingController.NoviceTeachingState NoviceTeachingState;
    public bool SlowMotion;
    public string[] AnimatorTransition;
    public Vector3[] Enemy; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (NoviceTeachingController.noviceTeachingController.enabled == false)
            {
           
                NoviceTeachingController.noviceTeachingController.OpenNoviceTeaching(NoviceTeachingState, SlowMotion, AnimatorTransition);
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
