using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganComponent : MonoBehaviour
{

    private Animator animator;
    public string AnimatorTriggerString;
    public string TriggerTag;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void TriggerOrgan()
    {
        animator.SetTrigger(AnimatorTriggerString);
    }

}
