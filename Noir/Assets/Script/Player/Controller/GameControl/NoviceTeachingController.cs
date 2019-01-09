using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoviceTeachingController : MonoBehaviour
{
    public static NoviceTeachingController noviceTeachingController;

  /*  public GameObject TriggerNextTeach;*/

    private TriggerNoviceTeach curTriggerNoviceTeach;

  /*  private bool SlowMotion;
    private string[] AnimatorTransitionInfo;
    private Transform[] creatEnemyPoint;*/
    public enum NoviceTeachingState
    {
        Move,
        Dash,
        Jump,
        DoubleJump,
        Avoid,
        ShortAttack,
        LongAttack,
        BigSkill,
        DashAttack,
        
    }

    
    public NoviceTeachingState noviceTeachingState;

    private void Awake()
    {
        noviceTeachingController = this;
        this.enabled = false;
      //  EnemyLayer = LayerMask.GetMask("Enemy");
    }
    private void Start()
    {
        
    }


    private void Update()
    {
        CloseNoviceTeaching(curTriggerNoviceTeach.SlowMotion);

        




        
    }
    public void OpenNoviceTeaching(TriggerNoviceTeach triggerNoviceTeach, NoviceTeachingState curNoviceTeachingState,bool isSlowMotion,string[] animatorTransitionInfo,Transform[] enemyCreatPos,GameObject enemy,GameObject triggerNextTeach)
    {
        this.enabled = true;
       /* noviceTeachingState = curNoviceTeachingState;*/
        
        curTriggerNoviceTeach = triggerNoviceTeach;
        NoviceTeachingImage.noviceTeachingImage.LoadImage((int)curTriggerNoviceTeach.NoviceTeachingState);
        /* SlowMotion = isSlowMotion;
         creatEnemyPoint = enemyCreatPos;
         TriggerNextTeach = triggerNextTeach;
         AnimatorTransitionInfo = animatorTransitionInfo;*/

        if (isSlowMotion)
        {
            Time.timeScale = 0.5f;
           
        }

        if (curTriggerNoviceTeach.CreatEnemyPoint.Length != 0) 
        {
            for(int i=0;i< curTriggerNoviceTeach.CreatEnemyPoint.Length; i++)
            {
                Instantiate(enemy, curTriggerNoviceTeach.CreatEnemyPoint[i].position, curTriggerNoviceTeach.CreatEnemyPoint[i].rotation);
            }
            
        }

    }


    public void CloseNoviceTeaching(bool isSlowMotion)
    {     
        if (MakeSureInput())
        {
            NoviceTeachingImage.noviceTeachingImage.UnLoadImage();
            
            if (isSlowMotion)
            {
                Time.timeScale = 1;
            }

           
        }

        if (curTriggerNoviceTeach.needToKillAllEnemy)
        {
            if(GameObject.FindGameObjectsWithTag("TeachEnemy").Length == 0)
            {
                this.enabled = false;
                if (curTriggerNoviceTeach.TriggerNextTeach != null)
                {
                    curTriggerNoviceTeach.TriggerNextTeach.SetActive(true);
                }

            }
            else
            {
                Debug.Log("NoEnemy");
                return;
            }
        }
        else if (NoviceTeachingImage.noviceTeachingImage.TeachImge.color.a == 0)
        {
            this.enabled = false;
            if (curTriggerNoviceTeach.TriggerNextTeach != null)
            {
                curTriggerNoviceTeach.TriggerNextTeach.SetActive(true);
            }
        }
               
    }

    public void ForceCloseNoviceTeaching(bool isSlowMotion)
    {
        NoviceTeachingImage.noviceTeachingImage.UnLoadImage();
        
        Time.timeScale = 1;
       
        this.enabled = false;
        Debug.Log("ttt");
    }

    private bool MakeSureInput()      
    {       
        if(curTriggerNoviceTeach.NoviceTeachingState == NoviceTeachingState.Move)
        {
            
            if (PlayerController.playerController.moveState != PlayerController.MoveState.Idle)
            {
                
                return true;
            }
        }       
        else if (curTriggerNoviceTeach.AnimatorTransition.Length != 0)
        {
            for(int i=0; i < curTriggerNoviceTeach.AnimatorTransition.Length; i++)
            {
                if (PlayerController.playerController.animator.GetAnimatorTransitionInfo(0).IsName(curTriggerNoviceTeach.AnimatorTransition[i]))
                {
                    return true;

                }
            }
            
        }
        else if (PlayerController.playerController.AnimatorstateInfo.IsTag(curTriggerNoviceTeach.NoviceTeachingState.ToString()))
        {
                return true;
        }
         return false;
        
    }

}
