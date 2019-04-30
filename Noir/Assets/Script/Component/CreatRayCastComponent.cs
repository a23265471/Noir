using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatRayCastComponent : MonoBehaviour
{

    [System.Serializable]
    public struct AttackRange
    {
        public GroupOfRayCast[] groupOfRayCast;

    }

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

    public AttackRange attackRange;


    private void Update()
    {
        CreatRaycast();
    }


    private void CreatRaycast()
    {
        Ray ray = new Ray(attackRange.groupOfRayCast[0].OneGroupOfRayCast[0].position, attackRange.groupOfRayCast[0].OneGroupOfRayCast[1].position);

        if (Physics.Raycast(ray, attackRange.groupOfRayCast[0].Long))
        {
            Debug.Log("hhh");
        }
       
        Debug.DrawLine(attackRange.groupOfRayCast[0].OneGroupOfRayCast[0].position, attackRange.groupOfRayCast[0].OneGroupOfRayCast[1].position,Color.red, attackRange.groupOfRayCast[0].Long);
    }

}
