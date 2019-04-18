﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Gravity))]
[RequireComponent(typeof(AnimationHash))]
[RequireComponent(typeof(Animator))]
public class AttackSystem : MonoBehaviour
{
    public SkillList skillList;
    private Animator animator;
    private AnimationHash animationHash;
    private Gravity gravity;
    public Dictionary<int, SkillList.AttackParameter> AttackCollection;

    //站存
    public GameObject AttackCollider_Small;
    public GameObject AttackCollider_Big;
    public GameObject AttackCollider_Skill;
    public AudioSource audioSource;
    public AudioClip audioClip;
    //站存

    public bool CanTriggerNextAttack;
    public bool isTriggerAttack;
    private int nextAttackID;

    private SkillList.AttackParameter currentAttackInfo;

    public bool IsAttack;

    IEnumerator detectAttackStateForceExit;

    private void Awake()
    {
        CreateAttackCollection();
        animator = GetComponent<Animator>();
        animationHash = GetComponent<AnimationHash>();
        gravity = GetComponent<Gravity>();
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        detectAttackStateForceExit = null;
        CanTriggerNextAttack = true;
        isTriggerAttack = false;
        IsAttack = false;

        AttackCollider_Small.SetActive(false);
        AttackCollider_Big.SetActive(false);
        AttackCollider_Skill.SetActive(false);
    }


    private void Update()
    {
        
    }
   

    #region 建立攻擊列表
    public void CreateAttackCollection()
    {
        SkillList.AttackParameter AttackParameter;

        AttackCollection = new Dictionary<int, SkillList.AttackParameter>();

        for (int i=0;i< skillList.normalAttack.Length; i++)
        {
            if (AttackCollection.TryGetValue(skillList.normalAttack[i].Id, out AttackParameter))
            {
                throw new System.Exception("Normal Attack Id已重複");

            }
            else
            {
                AttackCollection[skillList.normalAttack[i].Id] = skillList.normalAttack[i];
            }
         //   Debug.Log(skillList.normalAttack[i].Id);

        }

        for (int i = 0; i < skillList.specialAttack.Length; i++)
        {
            if (AttackCollection.TryGetValue(skillList.specialAttack[i].Id, out AttackParameter) )
            {
                throw new System.Exception("Special Attack Id已重複 specialAttack" +i+"Id = " + skillList.specialAttack[i].Id);

            }
            else
            {
                AttackCollection[skillList.specialAttack[i].Id] = skillList.specialAttack[i];

            }
        }

        for (int j = 0; j < skillList.deputyAttackCollections.Length; j++) 
        {
            for (int i = 0; i < skillList.deputyAttackCollections[j].DeputyAttack.Length; i++)
            {
                if (AttackCollection.TryGetValue(skillList.deputyAttackCollections[j].DeputyAttack[i].Id, out AttackParameter))
                {
                    throw new System.Exception("deputy Attack Collections "+ j + " DeputyAttack "+ i +"Id已重複 Id="+ skillList.deputyAttackCollections[j].DeputyAttack[i].Id);

                }
                else
                {
                    AttackCollection[skillList.deputyAttackCollections[j].DeputyAttack[i].Id] = skillList.deputyAttackCollections[j].DeputyAttack[i];

                }
            }
        }

       
    }
    #endregion

    public void JudgeInputKey(KeyCode input,string animatorTrigger)
    {
        



    }

    public void Attack(string animatorTrigger)
    {
        if (CanTriggerNextAttack)
        {
          //  Debug.Log("fff");
            if (detectAttackStateForceExit != null)
            {
                StopCoroutine(detectAttackStateForceExit);
            }

            StopCoroutine("resetTriggerAttack");
            StopCoroutine("DetectInput");
            animator.SetTrigger(animatorTrigger);

            CanTriggerNextAttack = false;
            isTriggerAttack = true;
            IsAttack = true;
        }

    }

   /* public void Attack()
    {
        if (currentAttackInfo.NextAttack.Length != 0)
        {
            for (int i = 0; i < currentAttackInfo.NextAttack.Length; i++)
            {
                if (Input.GetKeyDown(currentAttackInfo.NextAttack[i].keyCode))
                {
                    animator.SetTrigger(currentAttackInfo.NextAttack[i].AnimatorTriggerName);
                }

            }
        }       
        
    }*/

    #region 動畫事件
    public void GetAttackInfo(int Id)
    {
        currentAttackInfo = AttackCollection[Id];

        if (!currentAttackInfo.moveInfo.UseGravity)
        {
           // Debug.Log("Stop Use Gravity");
            gravity.StopUseGravity();
        }
    }

    public void TriggerNextAttack()
    {
        //   Debug.Log("jjjj");
        CanTriggerNextAttack = true;
        isTriggerAttack = false;

        StartCoroutine("DetectInput");
    }

    public void StopTriggerNextAttack()
    {
        Debug.Log(isTriggerAttack);
        if (!isTriggerAttack)
        {
            CanTriggerNextAttack = false;

        }
    }

    public void StartTriggerNextAnimation()
    {
        if (isTriggerAttack)
        {
            StopCoroutine("triggerNextAnimation");

            StartCoroutine("triggerNextAnimation");
        }

    }

    IEnumerator triggerNextAnimation()
    {
        yield return new WaitUntil(() => isTriggerAttack);

        Debug.Log("TriggerNextAttack");
        Attack(currentAttackInfo.NextAttack[nextAttackID].AnimatorTriggerName);

    }

    IEnumerator DetectInput()
    {        
        if (currentAttackInfo.NextAttack.Length != 0)
        {
            yield return new WaitUntil(() => DetectTriggerNextAttack());
        //    Debug.Log("TriggerNext");
        }
        else
        {
            yield return null;
        //    Debug.Log("dddd");
        }


    }

    private bool DetectTriggerNextAttack()
    {       
        if (currentAttackInfo.NextAttack != null)
        {
            for (int i = 0; i < currentAttackInfo.NextAttack.Length; i++)
            {

                if (Input.GetKeyDown(currentAttackInfo.NextAttack[i].keyCode))
                {
                    nextAttackID = i;
                    isTriggerAttack = true;
                    IsAttack = true;
                    return true;
                }              
            }            
        }
        return false;         
    }

    public void ResetTriggerAttack()
    {
       // Debug.Log(isTriggerAttack);
        if (!isTriggerAttack)
        {
            if (detectAttackStateForceExit != null)
            {
                StopCoroutine(detectAttackStateForceExit);
                Debug.Log("3. Reset Detect Attack State Force Exit");

            }
        }
        gravity.StartUseGravity();
        StopCoroutine("DetectInput");

        StopCoroutine("resetTriggerAttack");
        StartCoroutine("resetTriggerAttack");
    }

    IEnumerator resetTriggerAttack()
    {
        yield return new WaitForSeconds(0.2f);
      //  Debug.Log(IsAttack);
        StopCoroutine("triggerNextAnimation");

        //   IsAttack = false;
        //站存
        AttackCollider_Small.SetActive(false);
        AttackCollider_Big.SetActive(false);
        AttackCollider_Skill.SetActive(false);
        //站存
        
        // Debug.Log("stopDetectInput");

    }

    public void DetectForceExitAttack(string animationTag)
    {
        if (detectAttackStateForceExit != null)
        {
            StopCoroutine(detectAttackStateForceExit);
        }


        detectAttackStateForceExit = DetectAttackStateForceExit(animationTag);

        StartCoroutine(detectAttackStateForceExit);
    }

    IEnumerator DetectAttackStateForceExit(string animationTag)
    {
        //Debug.Log("1.Detect Attack State Force Exit");

       // yield return new WaitUntil(() => animationHash.GetCurrentAnimationTag(animationTag));
       // Debug.Log(animationHash.GetCurrentAnimationTag(animationTag));

        yield return new WaitUntil(() => !animationHash.GetCurrentAnimationTag(animationTag));
        Debug.Log("2.  Attack is State Force Exit");

        //站存
        AttackCollider_Small.SetActive(false);
        AttackCollider_Big.SetActive(false);
        AttackCollider_Skill.SetActive(false);
        //站存

        
        //Debug.Log("Reset TriggerAttack");

        IsAttack = false;
        StopCoroutine("DetectInput");
        CanTriggerNextAttack = true;
        isTriggerAttack = false;
    }

    public void OpenAttackCollider(int ColliderSize)
    {
       
        switch (ColliderSize)
        {
            case 0:
                AttackCollider_Small.SetActive(true);
                break;
            case 1:
                AttackCollider_Big.SetActive(true);
                break;

            case 3:
                AttackCollider_Skill.SetActive(true);


                break;

        }
    }

    public void CloseAttaCollider(int ColliderSize)
    {
        switch (ColliderSize)
        {
            case 0:
                AttackCollider_Small.SetActive(false);
                break;
            case 1:
                AttackCollider_Big.SetActive(false);
                break;
            case 3:
                AttackCollider_Skill.SetActive(false);


                break;

        }



    }

    public void AudioPlay()
    {
        audioSource.clip = audioClip;
        audioSource.Play();

    }


    #endregion


}
