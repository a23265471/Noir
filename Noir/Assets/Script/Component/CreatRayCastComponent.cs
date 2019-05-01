using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatRayCastComponent : MonoBehaviour
{

   /* [System.Serializable]
    public struct AttackRange
    {
        public GroupOfRayCast[] groupOfRayCast;

    }*/

    [System.Serializable]
    public struct GroupOfRayCast
    {
        public Transform[] OneGroupOfRayCast;
        public float Long {

            get
            {
                return Vector3.Distance(OneGroupOfRayCast[0].position, OneGroupOfRayCast[1].position);

            }
           
        }
    }

    public GroupOfRayCast[] groupOfRayCast;

    public int CurrentAttackCombo;
    public float DamageValue;
    private int HitCombo;
    


    private void Update()
    {
        CreatRaycast();
    }


    private void CreatRaycast()
    {
        Ray ray;
        RaycastHit rayHit;

        for (int i=0;i< groupOfRayCast.Length; i++)
        {
            groupOfRayCast[i].OneGroupOfRayCast[0].LookAt(groupOfRayCast[i].OneGroupOfRayCast[1].position);
            ray = new Ray(groupOfRayCast[i].OneGroupOfRayCast[0].position, groupOfRayCast[i].OneGroupOfRayCast[0].forward);
            Debug.DrawLine(groupOfRayCast[i].OneGroupOfRayCast[0].position, groupOfRayCast[i].OneGroupOfRayCast[1].position, Color.red);
            if (Physics.Raycast(ray, out rayHit, groupOfRayCast[0].Long))
            {

                if (rayHit.transform.GetComponent<GetHitComponent>() != null)
                {
                    Debug.Log("hhh");

                   
                    rayHit.transform.GetComponent<GetHitComponent>().TriggerDamage(10);
                       
                    break;
                }
              //  
            }

        }

        
       
    }

}
