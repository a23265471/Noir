using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class PlayerBehaviour : Character
{
    [Flags]
    public enum PlayerState
    {
        Move = 0x01,//0001 
        Jump = 0x02,//0010
        Falling = 0x04,//0100
        DoubleJump = 0x08,//1000
        Dash = 0x10,//1 0000
        Attack = 0x20,//10 0000
     //   DashAttack = 0x30,//11 0000
        Skill = 0x40,//0100 0000
        Avoid = 0x80,//100 0000
//        SkyAttack = 0xf0,//
        Damage = 0x01 + 0xff,//1 0000 0000
        GetDown = 0xff,
        Dead = 0x01+0xff+0xff+0x01,//10 0000 0000 

        CanFalling = Move | Attack | Dash/* | SkyAttack*/ ,
        FallingMove = Falling | Jump | DoubleJump /*| SkyAttack*/,
        CanDoubleJump = Jump | Falling | Dash,
        CanDash = Move | Attack | Jump | DoubleJump /*| SkyAttack */| Falling | Avoid,
 //       CanDashAttack = CanDash,
        CanSkyAttack = Jump | Falling | DoubleJump,
        CanAvoid = Move | Attack ,
        CanAttack =  Move | Avoid,
        CanSkill = Attack | Move,
        DoNotGroudedMove = Attack | Skill | Damage | Avoid | Dead,
        CanDamage = 0xff,
        CaGetDown = 0xff,
        CanDead = 0xff,

        AttackBehaviour=Attack | Skill,
        LandingChecking = Jump | Falling | DoubleJump /*| SkyAttack*/,

    }

    // public PlayerState DoubleJump;
    public PlayerState playerState;
    public static PlayerBehaviour playerBehaviour;

    private GameStageData gameStageData;
    private PlayerController_New playerController;
    private Animator playerAnimator;
   
    private AudioSource playerAudioSource;
    private PlayerData.PlayerParameter playerParameter;
    private PlayerData playerData;
    private AnimationHash animationHash;
    private ParticleManager particleManager;
    private AttackSystem attackSystem;
    
    public GhostShadow PlayerShader;

   // public GameObject GroundCheckObject;
    public Gravity gravity;


   

    IEnumerator attackStateResetToIdle;

    #region 圖層
    int floorMask;
    #endregion

    #region 子物件
    public Transform cameraLookAt;
    public Transform GetWeaponHand;
    public Transform WeaponPos;
    #endregion

    #region 移動參數
    public float rotation_Horizontal;
    private float curMoveSpeed;
    private float moveSpeed;

    private float moveAnimation_Vertical;
    private float moveAnimation_Horizontal;
    private float FallindAniamtion_Horizontal;
    public float MoveAnimationSmoothSpeed;
    public float JumpMoveAnimationSmoothSpeed;
    private int jumpCount;
    private int Direction_X;
    private int Direction_Z;
    private float avoidSpeed;
    private bool canfall;
    private bool canTriggerFallingCoroutine;

    private bool canFallingMove_x;
    private bool canFallingMove_z;

    //private ParticleSystem jumpParticleSytem;
    // public Transform jumpParticleTransform;
    #endregion

    #region 物理碰撞   
    private Rigidbody playerRigidbody;
    public CapsuleCollider GroudedCollider;
    public GameObject IdlePhysicsCollider;
    public GameObject SmallPhysicsCollider;
    public GameObject MediumPhysicsCollider;
    public float groundedDis;
    private bool ForceMove;
    #endregion

    #region 受傷
    private CapsuleCollider damageCollider;
    private string preFx;
    IEnumerator damageStopEffect;
    private bool inBlackRain;

    #endregion

    private void Awake()
    {
        playerBehaviour = this;
        playerAnimator = GetComponent<Animator>();
        playerAudioSource = GetComponent<AudioSource>();
        GroudedCollider = GetComponent<CapsuleCollider>();
        animationHash = GetComponent<AnimationHash>();
        playerRigidbody = GetComponent<Rigidbody>();
        particleManager = GetComponent<ParticleManager>();
        attackSystem = GetComponent<AttackSystem>();
        gravity = GetComponent<Gravity>();
        damageCollider = GetComponent<CapsuleCollider>();
        PlayerShader.enabled = false;

        gameStageData = GameFacade.GetInstance().gameStageData;
        playerController = GameFacade.GetInstance().playerController;
        playerParameter = gameStageData.CurPlayerStageData.playerData.playerParameter;
        playerData = gameStageData.CurPlayerStageData.playerData;

        floorMask = LayerMask.GetMask("Floor");

        playerState = PlayerState.Move;
        ForceMove = false;
        canfall = true;
        canTriggerFallingCoroutine = true;
        CreateWeapon();
    }

    void Start()
    {
        moveAnimation_Vertical = 0;
        moveAnimation_Horizontal = 0;
        cameraLookAt= gameObject.transform.Find("CameraLookAt");
        damageStopEffect = null;
        attackStateResetToIdle = null;
        inBlackRain = false;

        canFallingMove_x = true;
        canFallingMove_z = true;
    }

    void Update()
    {
        Rotaion();
    }

    private void FixedUpdate()
    {
        
    }

    private void AnimationRotation(int moveDirection_Vertical, int moveDirection_Horizontal)
    {
        if (moveDirection_Vertical == 1)
        {
            playerAnimator.SetBool("Forward", true);
            playerAnimator.SetBool("Back", false);

        }
        else if (moveDirection_Vertical == -1)
        {
            playerAnimator.SetBool("Forward", false);
            playerAnimator.SetBool("Back", true);
        }
        else
        {
            playerAnimator.SetBool("Forward", false);
            playerAnimator.SetBool("Back", false);
        }

        if (moveDirection_Horizontal == 1)
        {
            playerAnimator.SetBool("Left", false);
            playerAnimator.SetBool("Right", true);
        }
        else if (moveDirection_Horizontal == -1)
        {
            playerAnimator.SetBool("Right", false);
            playerAnimator.SetBool("Left", true);
        }
        else
        {
            playerAnimator.SetBool("Right", false);
            playerAnimator.SetBool("Left", false);
        }

    }

    #region 初始化
    private void CreateWeapon()
    {
        GameObject weapon = Instantiate(playerData.Weapon, WeaponPos.position, WeaponPos.rotation, GetWeaponHand);

    }

   
    #endregion

    #region 移動
    private void Rotaion()
    {       
        rotation_Horizontal += Input.GetAxis("Mouse X") * playerParameter.moveParameter.RotateSpeed * Time.deltaTime;
              
        if (rotation_Horizontal > 360)
        {
            rotation_Horizontal -= 360;
        }
        else if (rotation_Horizontal < 0)
        {
            rotation_Horizontal += 360;
        }

        
        transform.rotation = Quaternion.Euler(0, rotation_Horizontal, 0);
        
    }

    public void GroundedMove(int moveDirection_Vertical, int moveDirection_Horizontal)
    {
        float MoveX;
        float MoveZ;
        if (gravity.groundCheck.IsGround)
        {      
            AnimationBlendTreeControll(playerAnimator, "Vertical", moveDirection_Vertical, ref moveAnimation_Vertical, MoveAnimationSmoothSpeed);
            AnimationBlendTreeControll(playerAnimator, "Horizontal", moveDirection_Horizontal, ref moveAnimation_Horizontal, MoveAnimationSmoothSpeed);
            moveSpeed = playerParameter.moveParameter.RunSpeed;

            if (ForceMove || (playerState & PlayerState.DoNotGroudedMove) == 0) 
            {
               
                curMoveSpeed = FixSpeed(moveDirection_Vertical, moveDirection_Horizontal, playerParameter.moveParameter.RunSpeed);

                MoveX = moveAnimation_Horizontal * curMoveSpeed;
                MoveZ = moveAnimation_Vertical * curMoveSpeed;
                playerRigidbody.velocity = transform.rotation * new Vector3(MoveX, playerRigidbody.velocity.y, MoveZ);

             
            }            
        }
    }

    public void Falling(int moveDirection_Vertical, int moveDirection_Horizontal)
    {      
        if (!gravity.groundCheck.IsGround)
        {
            /*  if ((playerState & PlayerState.Move) != 0)
              {
                  playerAnimator.SetTrigger("Falling");
                 // playerState = PlayerState.Falling;
              }*/
            if((playerState & PlayerState.Move) != 0)
            {
                if (canTriggerFallingCoroutine)
                {
                    StopCoroutine("StartFalling");
                    StartCoroutine("StartFalling");
                }
            }
            FallingAniamtion(moveDirection_Vertical, moveDirection_Horizontal);
        }

    }

    IEnumerator StartFalling()
    {
        canTriggerFallingCoroutine = false;
        yield return new WaitForSeconds(0.1f);       
        if (!gravity.groundCheck.IsGround)
        {
            playerAnimator.SetTrigger("Falling");         
            // playerState = PlayerState.Falling;
           // Debug.Log("Start Falling");
        }
        
        canTriggerFallingCoroutine = true;

    }

    public void FallingAniamtion(int moveDirection_Vertical, int moveDirection_Horizontal)
    {
        float direction_X = moveDirection_Horizontal;
        float direction_Y = moveDirection_Vertical;
        direction_Y = Mathf.Clamp(direction_Y, 0, 1);
        if ((playerState & PlayerState.FallingMove) != 0)
        {
            if (moveDirection_Vertical == 1)
            {
                switch (moveDirection_Horizontal)
                {
                    case -1:
                        direction_X = -0.5f;
                        break;
                    case 1:
                        direction_X = 0.5f;
                        break;
                }

            }
            AnimationBlendTreeControll(playerAnimator, "Vertical", moveDirection_Vertical, ref moveAnimation_Vertical, MoveAnimationSmoothSpeed);
            AnimationBlendTreeControll(playerAnimator, "Horizontal", moveDirection_Horizontal, ref moveAnimation_Horizontal, MoveAnimationSmoothSpeed);
            AnimationBlendTreeControll(playerAnimator, "Jump_Rotation", direction_X, ref FallindAniamtion_Horizontal, JumpMoveAnimationSmoothSpeed);

            float fallingMoveX = moveDirection_Horizontal * playerParameter.jumpParameter.JumpMoveSpeed;
            float fallingMoveZ = direction_Y * playerParameter.jumpParameter.JumpMoveSpeed;

          /*  if (!canFallingMove_x)
            {
                fallingMoveX = 0;

            }
            if (!canFallingMove_z)
            {
                fallingMoveZ = 0;
            }*/

            playerRigidbody.velocity = transform.rotation * new Vector3(fallingMoveX, playerRigidbody.velocity.y, fallingMoveZ);

        }


    }

    public void Jump(int moveDirection_Vertical, int moveDirection_Horizontal)
    {
        AnimationRotation(moveDirection_Vertical, moveDirection_Horizontal);
        if (((playerBehaviour.playerState & PlayerState.Move) != 0))
        {
            // Debug.Log("Jump");           
            playerAnimator.SetTrigger("Jump");

        }
        else if (((playerBehaviour.playerState & PlayerState.CanDoubleJump) != 0) && !gravity.groundCheck.IsGround && jumpCount < playerParameter.jumpParameter.SkyJumpCount) 
        {
            //Debug.Log("Double");
            playerAnimator.SetTrigger("DoubleJump");
            gravity.StopGroundCheck();

        }

    }

    private float FixSpeed(int moveDirection_Vertical, int moveDirection_Horizontal, float value)
    {

        if (moveDirection_Vertical == 0 || moveDirection_Horizontal == 0)
        {
            return Mathf.Sqrt((Mathf.Pow(value, 2) * 2));
        }
        else
        {
            return value;
        }
 
    }
   
    #endregion

    #region 迴避
    public void Avoid(int moveDirection_Vertical,int moveDirection_Horizontal)
    {
       
        if (((playerBehaviour.playerState & PlayerState.CanAvoid) != 0) && (UI_HP.Ui_HP.SP>=20))
        {
            Direction_X = moveDirection_Horizontal;
            Direction_Z = moveDirection_Vertical;

            AnimationRotation(moveDirection_Vertical, moveDirection_Horizontal);

            
            avoidSpeed = FixSpeed(moveDirection_Vertical, moveDirection_Horizontal, playerParameter.avoidParameter.AvoidSpeed);

      //      Debug.Log(avoidSpeed);

            playerAnimator.SetTrigger("Avoid");
          

        }
                          
       
    }


    #endregion

    #region 攻擊

    public void NormalAttack()
    {
        if (gravity.groundCheck.IsGround)
        {
            if ((playerState & PlayerState.CanAttack)!=0)
            {
             
                attackSystem.Attack("NormalAttack");
            }

        }

    }
    public void Shooting()
    {
        if (gravity.groundCheck.IsGround)
        {
            if ((playerState & PlayerState.CanAttack) != 0)
            {
                attackSystem.GetShtooingTargetPos = ShootingTargetPos;
                attackSystem.Attack("LongAttack");

            }

        }

    }

    public Vector3 ShootingTargetPos(int bulletID)
    {
        return MainCamera_New.mainCamera.GetAimTarget() + MainCamera_New.mainCamera.transform.rotation * new Vector3(0, 0, attackSystem.currentAttackInfo.shootingInfo.MaxDistance);
    }

    public void Skill()
    {
        if (gravity.groundCheck.IsGround)
        {
            if ((playerState & PlayerState.CanSkill) != 0)
            {             
                attackSystem.Attack("Skill");
            }

        }

    }

    public void AttackTimeScaleEffect()
    {
        StopCoroutine("attackTimeScaleEffect");
        StartCoroutine("attackTimeScaleEffect");
    }

    IEnumerator attackTimeScaleEffect()
    {
        playerAnimator.speed = 0.1f;

        yield return new WaitForSeconds(0.05f);
        playerAnimator.speed = 1;

    }

    #endregion

    #region 衝刺

    public void Dash(int moveDirection_Vertical, int moveDirection_Horizontal)
    {
       if (UI_HP.Ui_HP.SP >= 20)
       {

           
          if (moveDirection_Horizontal == 0 && moveDirection_Vertical == 0) 
          {
              Direction_Z = 1;
              Direction_X = 0;
          }
          else
          {
              if (moveDirection_Vertical < 0)
              {
                  Direction_Z = 0;
              }
              else
              {
                  Direction_Z = moveDirection_Vertical;
              }

              Direction_X = moveDirection_Horizontal;


          }
          if(moveDirection_Vertical>=0)
          if ((playerState & PlayerState.CanDash) != 0)
          {
              // Debug.Log("jjj");
              AnimationRotation(Direction_Z, Direction_X);
              playerAnimator.SetTrigger("Dash");
          }
       }

    }

    #endregion

    #region 死亡
    public void Dead()
    {
        if (playerState != PlayerState.Dead)
        {
            damageCollider.enabled = false;
          /*  IdlePhysicsCollider.SetActive(false);
            MediumPhysicsCollider.SetActive(false);
            SmallPhysicsCollider.SetActive(false);*/
            playerState = PlayerState.Dead;
            playerAnimator.SetTrigger("Dead");
        }
        

    }


    #endregion


    #region AnimationEvent
    public void ChangePlayerState(int ChangePlayerState)
    {    
        switch (ChangePlayerState)
        {
            case (int)PlayerState.Move:        
                
                playerState = PlayerState.Move;

                SwitchCollider(0);
               // CanTriggerNextAttack = true;
                break;

            case (int)PlayerState.Jump:
                SwitchCollider(1);
                playerState = PlayerState.Jump;           
                break;

            case (int)PlayerState.DoubleJump:
                SwitchCollider(2);
                jumpCount += 1;
                //gravity.StopUseGravity();

                playerState = PlayerState.DoubleJump;                  
                break;

            case (int)PlayerState.Avoid:
                playerState = PlayerState.Avoid;
                SwitchCollider(2);
                damageCollider.enabled = false;
                UI_HP.Ui_HP.Consumesp(20);
                Displacement(playerRigidbody, transform.rotation, avoidSpeed, playerParameter.avoidParameter.AvoidDistance, Direction_X, 0, Direction_Z);

                break;

            case (int)PlayerState.Falling:             
                if (((playerState & PlayerState.CanFalling) != 0) && canfall)
                {
                    playerState = PlayerState.Falling;
                    playerAnimator.ResetTrigger("Falling");
                    StartLandingCheck();
                    SwitchCollider(1);
                }
                break;

            case (int)PlayerState.Attack:

                playerState = PlayerState.Attack;
                StopResetToIdleState();
                playerRigidbody.velocity = playerRigidbody.velocity*0.01f;
                SwitchMove(0);
             //   Debug.Log(playerState);
                DetectForceExitAttack();

                break;

            case (int)PlayerState.Dash:
                playerState = PlayerState.Dash;
                PlayerShader.enabled = true;
                canfall = false;
                damageCollider.enabled = false;
                UI_HP.Ui_HP.Consumesp(20);
                
                StartCoroutine("DetcetExitDash");
                StopCoroutine("LandingCheck");

                break;
            case (int)PlayerState.Skill:
                playerState = PlayerState.Skill;
                StopResetToIdleState();
                playerRigidbody.velocity = new Vector3(0,0,0);
                DetectForceExitAttack();

                SwitchMove(0);
                break;

            case (int)PlayerState.Damage:
                playerState = PlayerState.Damage;
                playerRigidbody.velocity = new Vector3(0, 0, 0);

                break;

        }
    }

    public void EffectPlay(string Id)//------
    {
        if (preFx != null)
        {
            particleManager.GetParticle(preFx).Stop();
        }
        preFx = Id;
        ParticlePlay(particleManager.GetParticle(Id));
    }

    public void SwitchMove(int onOff)
    {
        switch (onOff)
        {
            case 0:
                ForceMove = false;
                break;
            case 1:
                ForceMove = true;
                break;
        }
    }

    public void SwitchCollider(int colliderSize)
    {
        switch (colliderSize)
        {
            case 0:
                IdlePhysicsCollider.SetActive(true);            
                MediumPhysicsCollider.SetActive(false);
                SmallPhysicsCollider.SetActive(false);
            /*--暫時Damage--*/
                damageCollider.height = IdlePhysicsCollider.GetComponent<CapsuleCollider>().height;
                damageCollider.center = IdlePhysicsCollider.GetComponent<CapsuleCollider>().center;
                break;
            /*--暫時Damage--*/

            case 1:
                IdlePhysicsCollider.SetActive(false);
                MediumPhysicsCollider.SetActive(true);
                SmallPhysicsCollider.SetActive(false);

                /*--暫時Damage--*/
                damageCollider.height = MediumPhysicsCollider.GetComponent<CapsuleCollider>().height;
                damageCollider.center = MediumPhysicsCollider.GetComponent<CapsuleCollider>().center;
                /*--暫時Damage--*/
                break;

            case 2:
                IdlePhysicsCollider.SetActive(false);
                MediumPhysicsCollider.SetActive(false);
                SmallPhysicsCollider.SetActive(true);

                /*--暫時Damage--*/
                damageCollider.height = SmallPhysicsCollider.GetComponent<CapsuleCollider>().height;
                damageCollider.center = SmallPhysicsCollider.GetComponent<CapsuleCollider>().center;
                /*--暫時Damage--*/

                break;
        }

    }

    public void ChangeToIdle(int curState)
    {
        if ((int)playerState == curState)
        {
            if (gravity.groundCheck.IsGround)
            {
                //  Debug.Log((int)PlayerState.Damage);
                //Debug.Log("hhh");

                playerAnimator.ResetTrigger("Falling");
                playerAnimator.SetTrigger("Idle");
                ChangePlayerState(1);
                damageCollider.enabled = true;

            }
            else
            {
                playerAnimator.ResetTrigger("Idle");

                playerAnimator.SetTrigger("Falling");
                damageCollider.enabled = true;

            }
        }

    }

   

    #region 跳躍動畫
    public void AddForce(int JumpState)
    {
        switch (JumpState)
        {
            case (int)PlayerState.Jump:
                Keyframe jumpEndKey = playerParameter.jumpParameter.JumpCurve.keys[playerParameter.jumpParameter.JumpCurve.keys.Length - 1];
                gravity.StopUseGravity();

                StopRigiBodyMoveWithAniamtionCurve_Y();
                RigiBodyMoveWithAniamtionCurve_Y(playerRigidbody,playerParameter.jumpParameter.JumpCurve, 0, jumpEndKey.time, 12, playerParameter.jumpParameter.JumpPerIntervalTime);
                StopCoroutine("StartUseGravity");
                StartCoroutine("StartUseGravity");
                JumpDoExitGround();
                break;

            case (int)PlayerState.DoubleJump:
                Keyframe doubleJumpEndKey = playerParameter.jumpParameter.DoubleJumpCurve.keys[playerParameter.jumpParameter.DoubleJumpCurve.keys.Length - 1];
                StopRigiBodyMoveWithAniamtionCurve_Y();
                gravity.StopUseGravity();
                // playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, 0, playerRigidbody.velocity.z);
                RigiBodyMoveWithAniamtionCurve_Y(playerRigidbody, playerParameter.jumpParameter.DoubleJumpCurve, 0, doubleJumpEndKey.time, 12, playerParameter.jumpParameter.JumpPerIntervalTime);
                StopCoroutine("StartUseGravity");

                StartCoroutine("StartUseGravity");
             //   StartLandingCheck();
                
                break;
        }
       
    }

    IEnumerator OpenGroundedCheck()
    {
        yield return new WaitUntil(() => (playerRigidbody.velocity.y < 0));
        gravity.StartGroundCheck();
       
    }

    IEnumerator DoExitGround()
    {
        yield return new WaitUntil(() => !gravity.groundCheck.IsGround);

        gravity.StopGroundCheck();
        StopCoroutine("OpenGroundedCheck");
        StartCoroutine("OpenGroundedCheck");
        StartLandingCheck();
    }

    public void JumpDoExitGround()
    {
        StopCoroutine("DoExitGround");
        StartCoroutine("DoExitGround");
    }

    public void StartLandingCheck()
    {
       
        StopCoroutine("LandingCheck");
        StartCoroutine("LandingCheck");


    }

    IEnumerator StartUseGravity()
    {
        yield return new WaitUntil(() => !RigibodyAnimationCurveIsRunning);
       // Debug.Log(playerState);
        gravity.StartUseGravity();
    }

    
    IEnumerator LandingCheck()
    {
        yield return new WaitUntil(() => gravity.groundCheck.IsGround);
        
        if (animationHash.GetCurrentAnimationState("Idle_Run")) 
        {
            //  Debug.Log("Idle");
            // Debug.Log(playerState);
            playerAnimator.ResetTrigger("DoubleJump");
            ChangePlayerState(1);

            playerAnimator.ResetTrigger("Idle");
            StopRigiBodyMoveWithAniamtionCurve_Y();             
            FallindAniamtion_Horizontal = 0;
            jumpCount = 0;
        }
        else
        {
            
            playerAnimator.SetTrigger("Idle");

            //  Debug.Log(playerState);

            StopCoroutine("LandingCheck");
            StartCoroutine("LandingCheck");
            
        }
    

    }

    IEnumerator ForceExitJump()
    {
        yield return new WaitUntil(() => playerState != PlayerState.Jump || playerState != PlayerState.DoubleJump);

    }


    #endregion

    #region 攻擊動畫

    public void AttackMoveSwitch()
    {
        if (!attackSystem.isTriggerAttack)
        {
          //  Debug.Log("kkk");
            SwitchMove(1);
        }
    }

    public void StopResetToIdleState()
    {
        /*if (detectAnimationStateNotAttack != null)
        {
            StopCoroutine(detectAnimationStateNotAttack);
        }*/
        if (attackStateResetToIdle != null)
        {
            StopCoroutine(attackStateResetToIdle);

        }
        playerAnimator.ResetTrigger("Idle");

    }

    public void ResetToIdleState(int currentState)
    {
        StopCoroutine("detectForceExitAttack");

        if (attackStateResetToIdle != null)
        {
            StopCoroutine(attackStateResetToIdle);

        }
        attackStateResetToIdle = resetToIdleState(currentState);
        StartCoroutine(attackStateResetToIdle);

      
     
    }

    IEnumerator resetToIdleState(int currentState)
    {

        yield return new WaitUntil(() => !attackSystem.IsAttack);

        ChangeToIdle(currentState);
       // Debug.Log("tttttt");
       

        ForceMove = false;

    }


    public void AttackDisplacement(int AttackId)
    {
      //  Debug.Log("Attack hhh");
        if (playerState == PlayerState.Attack) 
        {
            Displacement(playerRigidbody,
               transform.rotation,
               attackSystem.AttackCollection[AttackId].moveInfo.MoveSpeed,
               attackSystem.AttackCollection[AttackId].moveInfo.MoveDistance,
               attackSystem.AttackCollection[AttackId].moveInfo.MoveDirection_X,
               attackSystem.AttackCollection[AttackId].moveInfo.MoveDirection_Y,
               attackSystem.AttackCollection[AttackId].moveInfo.MoveDirection_Z
               );
        }              
    }

    public void DetectForceExitAttack()
    {
        StopCoroutine("detectForceExitAttack");
        StartCoroutine("detectForceExitAttack");

    }

    IEnumerator detectForceExitAttack()
    {
      
        yield return new WaitUntil(() => (playerState & PlayerState.AttackBehaviour)==0);
        Debug.Log(playerState);
        attackSystem.ForceExitAttack();

    }

    #endregion

    #region 衝刺
    public void EndDash()
    {
        
        StopCoroutine("DetcetExitDash");
        PlayerShader.enabled = false;      
        gravity.StartUseGravity();     
        canfall = true;
        ChangeToIdle(16);     
    }

    IEnumerator DetcetExitDash()
    {
      //  Debug.Log(animationHash.GetCurrentAnimationState("Dash"));

        yield return new WaitUntil(() => (playerState!=PlayerState.Dash));


        PlayerShader.enabled = false;
        if (!attackSystem.isTriggerAttack && playerState!=PlayerState.DoubleJump)
        {
            gravity.StartUseGravity();
            Debug.Log(playerState);

        }
        canfall = true;
    }


    public void DashMove()
    {
        StopCoroutine("StartUseGravity");

        StopRigiBodyMoveWithAniamtionCurve_Y();
        gravity.StopUseGravity();
        playerRigidbody.velocity = new Vector3(0, 0, 0);
        Displacement(playerRigidbody, transform.rotation, playerParameter.dashParameter.DashSpeed, playerParameter.dashParameter.DashDistance, Direction_X, 0, Direction_Z);


    }

    #endregion

    #endregion

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "EnemyAttack_Small":
                playerAnimator.SetTrigger("Damage");
                UI_HP.Ui_HP.ConsumeHp(10);
                break;
            case "EnemyAttack_Big":
                playerAnimator.SetTrigger("Damage");
                UI_HP.Ui_HP.ConsumeHp(20);
                break;

            case "BlackRain":
                inBlackRain = true;
                UI_HP.Ui_HP.OpenPoisoningSpot();
                Debug.Log(other.transform.name);
                StopCoroutine("InBlackRain");
                StartCoroutine("InBlackRain");
                break;
            case "BlackRain_Dead":
                UI_HP.Ui_HP.ConsumeHp(UI_HP.Ui_HP.HP_Max);

                break;
            case "Level2":
                MainMenu.mainMenu.LoadGame(5);

                break;
            case "Bone":
                playerAnimator.SetTrigger("Damage");
                UI_HP.Ui_HP.ConsumeHp(20);
                break;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("BlackRain"))
        {
            inBlackRain = false;
        }

    }
    
    private bool BlackRainConsumHP()
    {
        if (inBlackRain)
        {
            UI_HP.Ui_HP.ConsumeHp(5);

            return false;
        }
        else
        {

            return true;

        }

    }

    IEnumerator InBlackRain()
    {
        yield return new WaitWhile(() => BlackRainConsumHP());
        UI_HP.Ui_HP.ClosePoisoningSpot();
    }

    public void DamageFX(float stopTime)
    {
        switch (playerState)
        {
            case PlayerState.Dead:
                Time.timeScale = 0.1f;
                MainCamera_New.mainCamera.CameraShake(0.3f, 0.08f);

                break;

            case PlayerState.Damage:
                playerAnimator.speed = 0.1f;
                MainCamera_New.mainCamera.CameraShake(0.3f, 0.07f);

                break;
        }

        UI_HP.Ui_HP.OpenBloodSpot();

        if (damageStopEffect != null)
        {
            StopCoroutine(damageStopEffect);
        }
        damageStopEffect = DamageStopEffect(stopTime);
        StartCoroutine(damageStopEffect);


    }

    IEnumerator DamageStopEffect(float stopTime)
    {
        yield return new WaitForSeconds(stopTime);//0.006
        playerAnimator.speed = 1f;
        Time.timeScale = 1;

    }

}