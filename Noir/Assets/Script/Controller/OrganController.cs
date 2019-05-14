using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganController : MonoBehaviour
{
    public GameObject[] Organ;


    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < Organ.Length; i++) 
        {
            if (other.CompareTag(Organ[i].GetComponent<OrganComponent>().TriggerTag) /*&& other.CompareTag("Player")*/)
            {
                Organ[i].GetComponent<OrganComponent>().TriggerOrgan();
                Debug.Log("h");
            }
            
        }

    }


}
