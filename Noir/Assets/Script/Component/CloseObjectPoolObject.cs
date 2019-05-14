using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseObjectPoolObject : MonoBehaviour
{

    public void CloseObject()
    {
        StartCoroutine("CloseObjectPoolParticle");

    }

    IEnumerator CloseObjectPoolParticle()
    {

        yield return new WaitUntil(() => GetComponent<ParticleSystem>().isStopped);
        gameObject.SetActive(false);
    }

}
