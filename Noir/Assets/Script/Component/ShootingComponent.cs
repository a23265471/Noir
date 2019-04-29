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
        originPos = transform.position;

        // MoveToTarget(20);
    }

    private void Update()
    {

    }

    public void MoveToTarget(float maxDistance,float speed)
    {
        rigidbody = GetComponent<Rigidbody>();

        transform.LookAt(targetPos);
        rigidbody.velocity = transform.rotation * new Vector3(0, 0, speed);

        StartCoroutine("CloseObject");
     
    }

    IEnumerator CloseObject()
    {


        yield return new WaitUntil(() => Vector3.Distance(targetPos, transform.position) < 0.5f);

        gameObject.SetActive(false);
    }

}
