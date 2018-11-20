using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
 //   public Animator animator{ get { return GetAnimator(); } }


    abstract public void Move();
    
    public void ActionControl(Animator animator,ParticleSystem particleSystem,AudioSource audioSource,AudioClip audioClip)
    {

    }

}
