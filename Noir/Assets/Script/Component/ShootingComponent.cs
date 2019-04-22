using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingComponent : MonoBehaviour
{

    Rigidbody rigidbody;

    IEnumerator moveCoroutine;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        moveCoroutine = null;
    }


    public void MoveToTarget(Vector3 targetPos,float maxDistance)
    {

        if(Vector3.Distance(targetPos, transform.position)< maxDistance)
        {
            moveCoroutine = Move(targetPos, maxDistance);

            StartCoroutine(moveCoroutine);
        }


    }


    IEnumerator Move(Vector3 targetPos, float maxDistance)
    {


        while(Vector3.Distance(targetPos,transform.position) != 0)
        {
            transform.LookAt(targetPos);
            rigidbody.velocity = new Vector3(0, 0, 10);
            

            yield return null;
        }


    }

}
