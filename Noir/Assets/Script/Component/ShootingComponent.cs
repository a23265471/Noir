using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingComponent : MonoBehaviour
{

    public Rigidbody rigidbody;
    public Vector3 targetPos;
    private Vector3 originPos;



    IEnumerator moveCoroutine;

    private void Start()
    {
       
        moveCoroutine = null;
     //   rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {

        MoveToTarget(20);
        originPos = transform.position;
    }

    private void Update()
    {

       /* if(Vector3.Distance(targetPos, transform.position)<= maxDistance)
        {


        }*/
            

    }

    public void MoveToTarget(float maxDistance)
    {
        rigidbody = GetComponent<Rigidbody>();


        Debug.Log(targetPos);
        
        if (Vector3.Distance(targetPos, transform.position) < maxDistance)
        {

                transform.LookAt(targetPos);
                rigidbody.velocity = transform.rotation * new Vector3(0, 0, 10);
        }

        


    }

    /*IEnumerator Move(Vector3 targetPos, float maxDistance)
    {
        while(Vector3.Distance(targetPos,transform.position) != 0)
        {
            transform.LookAt(targetPos);
            rigidbody.velocity = new Vector3(0, 0, 10);
            

            yield return null;
        }
        

    }*/

    IEnumerator CloseObject()
    {

        //while(Vector3.Distance(transform.position, originPos)>=)
        yield return null;

    }

}
