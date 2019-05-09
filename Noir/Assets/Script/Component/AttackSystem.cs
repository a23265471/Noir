using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Gravity))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(ObjectPoolManager))]

public class AttackSystem : MonoBehaviour
{
    public static AttackSystem attackSystem;
    public SkillList skillList;
    private Animator animator;
    private Gravity gravity;
    public Dictionary<int, SkillList.AttackParameter> AttackCollection;
    public Dictionary<int, GameObject> AttackRangeObjectCollection;
    public ObjectPoolManager objectPoolManager;

    //站存
 /* public GameObject AttackCollider_Small;
    public GameObject AttackCollider_Big;
    public GameObject AttackCollider_Skill;*/
    public AudioSource audioSource;
 //   public AudioClip audioClip;
    //站存

    [System.Serializable]
    public struct AttackRang
    {
        public string Name;
        public int ID;
        public Transform Parent;
        public GameObject AttackRangTransform;
       // public GameObject AttackCollider_Ray;
    } 
    
    public Vector3 longAttackAimTartgetPos;
    public Func<int,Vector3> GetShtooingTargetPos;

    public bool CanTriggerNextAttack;
    public bool isTriggerAttack;
    public bool IsAttack;

    private int nextAttackID;

    public SkillList.AttackParameter currentAttackInfo;
    public AttackRang[] attackRange;

  //  IEnumerator detectAttackStateForceExit;

    private void Awake()
    {
        CreateAttackCollection();
        animator = GetComponent<Animator>();
      //  animationHash = GetComponent<AnimationHash>();
        gravity = GetComponent<Gravity>();
        audioSource = GetComponent<AudioSource>();
        objectPoolManager = GetComponent<ObjectPoolManager>();

    }

    void Start()
    {
       // detectAttackStateForceExit = null;
        CanTriggerNextAttack = true;
        isTriggerAttack = false;
        IsAttack = false;

        /*   AttackCollider_Small.SetActive(false);
           AttackCollider_Big.SetActive(false);*/
        // AttackCollider_Skill.SetActive(false);
    }


    private void Update()
    {
      //  Debug.DrawLine(transform.position, MainCamera_New.mainCamera.transform.forward * AttackCollection[104].shootingInfo.MaxDistance, Color.red);

    }
   

    #region 建立攻擊列表
    public void CreateAttackCollection()
    {
        SkillList.AttackParameter AttackParameter;

        AttackCollection = new Dictionary<int, SkillList.AttackParameter>();
        AttackRangeObjectCollection = new Dictionary<int, GameObject>();


        for (int i=0;i< skillList.normalAttack.Length; i++)
        {
            if (AttackCollection.TryGetValue(skillList.normalAttack[i].Id, out AttackParameter))
            {
                throw new System.Exception("Normal Attack Id已重複");

            }
            else
            {
                AttackCollection[skillList.normalAttack[i].Id] = skillList.normalAttack[i];
                if (skillList.normalAttack[i].AttackRang != null)
                {
                    CreateAttackRange(skillList.normalAttack[i].Id);

                }
            }

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
                if (skillList.specialAttack[i].AttackRang != null)
                {
                    CreateAttackRange(skillList.specialAttack[i].Id);

                }
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
                    if (skillList.deputyAttackCollections[j].DeputyAttack[i].AttackRang != null)
                    {
                        CreateAttackRange(skillList.deputyAttackCollections[j].DeputyAttack[i].Id);

                    }

                }
            }
        }

       

    }

    public void CreateAttackRange(int currentSkill_ID)
    {

       /* if (skillList.normalAttack[currentSkill].AttackRang != null)
        {*/
            for(int i=0;i< attackRange.Length; i++)
            {
                if (currentSkill_ID == attackRange[i].ID) 
                {
                    GameObject createattackRange = Instantiate(AttackCollection[currentSkill_ID].AttackRang, attackRange[i].Parent);
                    createattackRange.transform.position = attackRange[i].AttackRangTransform.transform.position;
                    createattackRange.transform.rotation = attackRange[i].AttackRangTransform.transform.rotation;
                    //attackRange[i].AttackCollider_Ray = createattackRange;
                    if (createattackRange.GetComponent<CreatRayCastComponent>() != null)
                    {
                        createattackRange.GetComponent<CreatRayCastComponent>().attackSystem = this;
                    }

                    if (createattackRange.GetComponent<AttackColliderTrigger>() != null)
                    {
                        createattackRange.GetComponent<AttackColliderTrigger>().attackSystem = this;

                    }

                    AttackRangeObjectCollection[attackRange[i].ID] = createattackRange;

                    AttackRangeObjectCollection[attackRange[i].ID].SetActive(false);

                    Destroy(attackRange[i].AttackRangTransform);
                }

            }


       // }

    }


    
    #endregion

    public void Attack(string animatorTrigger)
    {
        if (CanTriggerNextAttack)
        {
        /*    if (detectAttackStateForceExit != null)
            {
                StopCoroutine(detectAttackStateForceExit);
            }*/

            StopCoroutine("resetTriggerAttack");
            StopCoroutine("DetectInput");
            animator.SetTrigger(animatorTrigger);

            CanTriggerNextAttack = false;
            isTriggerAttack = true;
            IsAttack = true;
        }

    }

    public void Shooting(int Bullet_ID)
    {
        GameObject bullet;
        bullet = objectPoolManager.GetObjectPool(Bullet_ID);
        longAttackAimTartgetPos = GetShtooingTargetPos(Bullet_ID);


      //  longAttackAimTartgetPos += MainCamera_New.mainCamera.transform.rotation * new Vector3(0, 0, currentAttackInfo.shootingInfo.MaxDistance);
        bullet.GetComponent<ShootingComponent>().targetPos = longAttackAimTartgetPos;
     
        bullet.SetActive(true);
        bullet.GetComponent<ShootingComponent>().MoveToTarget(currentAttackInfo.shootingInfo.MaxDistance, currentAttackInfo.shootingInfo.ShootingSpeed);

    }


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
                    return true;
                }
            }
        }
        return false;
    }

    public void StopTriggerNextAttack()//Dash
    {
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

     //   Debug.Log("TriggerNextAttack");
        Attack(currentAttackInfo.NextAttack[nextAttackID].AnimatorTriggerName);

    }

   
    public void EndOfAttack()
    {
      
     /*   if (detectAttackStateForceExit != null)
        {
                StopCoroutine(detectAttackStateForceExit);

        }
        */
        Debug.Log("3. Reset Detect Attack State Force Exit");

        StopDetectInput();
        ResetTriggerAttack();
        IsAttack = false;

    }

    public void StopDetectInput()
    {
        StopCoroutine("DetectInput");
        StopCoroutine("triggerNextAnimation");
    
       
    }

    public void ResetTriggerAttack()
    {
      /*  if (!isTriggerAttack)
        {
            /*if (detectAttackStateForceExit != null)
            {
                StopCoroutine(detectAttackStateForceExit);
            //    Debug.Log("3. Reset Detect Attack State Force Exit");

            }*/
        //}
        gravity.StartUseGravity();
        CanTriggerNextAttack = true;
        isTriggerAttack = false;
    }





    public void ForceExitAttack()
    {
        Debug.Log("2.  Attack is State Force Exit");

       
        //      IsAttack = false;
        StopCoroutine("DetectInput");
        CanTriggerNextAttack = true;
        isTriggerAttack = false;

        AttackRangeObjectCollection[currentAttackInfo.Id].SetActive(false);

    }

    public void OpenAttackCollider(int ID)
    {
        
        AttackRangeObjectCollection[ID].SetActive(true);

        if (AttackRangeObjectCollection[ID].GetComponent<CreatRayCastComponent>() != null) 
        {

            AttackRangeObjectCollection[ID].GetComponent<CreatRayCastComponent>().ResetHitCombo(currentAttackInfo.Combo);

        }

        if (AttackRangeObjectCollection[ID].GetComponent<AttackColliderTrigger>() != null)
        {
            AttackRangeObjectCollection[ID].GetComponent<AttackColliderTrigger>().ResetHitCombo(currentAttackInfo.Combo);

        }

    }

    public void CloseAttaCollider(int ID)
    {
        
            AttackRangeObjectCollection[ID].SetActive(false);

      
          


    }

    public void AudioPlay()
    {
        audioSource.clip = currentAttackInfo.AudioClip_Attack;
        audioSource.Play();

    }


    #endregion


}
