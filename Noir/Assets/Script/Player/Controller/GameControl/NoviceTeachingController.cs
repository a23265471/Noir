using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoviceTeachingController : MonoBehaviour
{
    public static NoviceTeachingController noviceTeachingController;

    public GameObject LongAttackTeachTrigger;
    public GameObject BigSkillTeachTrigger;
    private bool SlowMotion;
    private string[] AnimatorTransitionInfo;
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
      //  EnemyLayer = LayerMask.GetMask("Enemy");
    }

    private void Update()
    {
        CloseNoviceTeaching(SlowMotion);






        Debug.Log("hhh");
    }
    public void OpenNoviceTeaching(NoviceTeachingState curNoviceTeachingState,bool isSlowMotion,string[] animatorTransitionInfo)
    {
        this.enabled = true;
        noviceTeachingState = curNoviceTeachingState;
        NoviceTeachingImage.noviceTeachingImage.LoadImage((int)noviceTeachingState);
        SlowMotion = isSlowMotion;
        
        AnimatorTransitionInfo = animatorTransitionInfo;
        
        
        if (isSlowMotion)
        {
            Time.timeScale = 0.5f;
            Debug.Log("ttt");
        }


    }

    public void CloseNoviceTeaching(bool isSlowMotion)
    {     
        if (MakeSureInput(noviceTeachingState))
        {
            NoviceTeachingImage.noviceTeachingImage.UnLoadImage();
            if (isSlowMotion)
            {
                Time.timeScale = 1;
            }
            this.enabled = false;
        }
        
    }

    public void ForceCloseNoviceTeaching(bool isSlowMotion)
    {
        NoviceTeachingImage.noviceTeachingImage.UnLoadImage();
        
        Time.timeScale = 1;
       
        this.enabled = false;
        Debug.Log("ttt");
    }

    public void ShortAttackTeach()
    {
        /*if(Physics.Raycast(PlayerController.playerController.transform.position, PlayerController.playerController.transform.forward, 5, EnemyLayer))
        {
            Debug.Log("yyy");
        }*/
       
    }
    private bool MakeSureInput(NoviceTeachingState curNoviceTeachingState/*,string AnimatorTransitionInfo*/)      
    {
        /* if (SlowMotion)
         {
             if(PlayerController.playerController.animator.GetAnimatorTransitionInfo(0).IsName("PlayerController -> Jump"))
             {
                 return true;
             }

         }
         else

         {*/
        if(curNoviceTeachingState== NoviceTeachingState.Move)
        {
            if (PlayerController.playerController.moveState != PlayerController.MoveState.Idle)
            {
                return true;
            }
        }       
        else if (AnimatorTransitionInfo.LongLength != 0)
        {
            for(int i=0; i < AnimatorTransitionInfo.LongLength; i++)
            {
                if (PlayerController.playerController.animator.GetAnimatorTransitionInfo(0).IsName(AnimatorTransitionInfo[i]))
                {
                    return true;
                }
            }
            
        }
        else if (PlayerController.playerController.AnimatorstateInfo.IsTag(curNoviceTeachingState.ToString()))
        {
                return true;
        }
       // }
        

        /*switch ((int)noviceTeachingState)
        {
            case (int)NoviceTeachingState.Move:
                if (PlayerController.playerController.moveState!=PlayerController.MoveState.Idle)
                {
                    return true;
                }
                break;
            case (int)NoviceTeachingState.Jump:
                if (PlayerController.playerController.animator.GetAnimatorTransitionInfo(0).IsName("PlayerController -> Jump"))
                {
                   
                    return true;
                }
                break;
            case (int)NoviceTeachingState.DoubleJump:
                if (PlayerController.playerController.AnimatorstateInfo.IsName("DoubleJump"))
                {
                    return true;
                }
                break;
            case (int)NoviceTeachingState.Avoid:
                if (PlayerController.playerController.AnimatorstateInfo.IsTag("Avoid"))
                {
                    return true;
                }
                break;
            case (int)NoviceTeachingState.ShortAttack:
                if (PlayerController.playerController.AnimatorstateInfo.IsTag("ShortAttack"))
                {
                    return true;
                }
                break;
            case (int)NoviceTeachingState.LongAttack:
                if (PlayerController.playerController.AnimatorstateInfo.IsName("LongAttack"))
                {
                    return true;
                }
                break;
            case (int)NoviceTeachingState.DashAttack:
                if (PlayerController.playerController.AnimatorstateInfo.IsName("DashAttack"))
                {
                    return true;
                }
                break;
            case (int)NoviceTeachingState.BigSkill:
                if (PlayerController.playerController.AnimatorstateInfo.IsName("BigSkill"))
                {
                    return true;
                }
                break;
        }*/

        return false;
        

    }

}
