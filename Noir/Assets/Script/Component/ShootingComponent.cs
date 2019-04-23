using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingComponent : MonoBehaviour
{

    public Rigidbody rigidbody;
    public Vector3 targetPos;

    IEnumerator moveCoroutine;

    private void Start()
    {
       
        moveCoroutine = null;
    }

    private void OnEnable()
    {

        MoveToTarget(20);
    }

    public void MoveToTarget(float maxDistance)
    {
        rigidbody = GetComponent<Rigidbody>();

        if (Vector3.Distance(targetPos, transform.position)< maxDistance)
        {
            /*moveCoroutine = Move(targetPos, maxDistance);

            StartCoroutine(moveCoroutine);*/
            transform.LookAt(targetPos);
            rigidbody.velocity = transform.rotation * new Vector3(0, 0, 10);
        }


    }


   /* IEnumerator Move(Vector3 targetPos, float maxDistance)
    {


        while(Vector3.Distance(targetPos,transform.position) != 0)
        {
            transform.LookAt(targetPos);
            rigidbody.velocity = new Vector3(0, 0, 10);
            

            yield return null;
        }
        

    }*/

}
