using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SkillList : ScriptableObject
{
    [System.Serializable]
    public struct AttackParameter
    {
        public string Name;
        public int Id;
        public ChangeToDeputyAttack[] NextAttack;
        public MoveInfo moveInfo;
        public Shooting shootingInfo;

        public GameObject AttackRang;
        //public AudioClip AudioClip_Attack;

        public float AttackPower;
        public int Combo;
        public string DamageAnimator;

    }

    [System.Serializable]
    public struct DeputyAttackCollection
    {
        public string Name;
        public AttackParameter[] DeputyAttack;
    }

    [System.Serializable]
    public struct ChangeToDeputyAttack
    {
        public string Name;      
        public string AnimatorTriggerName;
        public KeyCode keyCode;
    }

    [System.Serializable]
    public struct Shooting
    {
        public float MaxDistance;
        public float ShootingSpeed;

        public int PenetrateCount;
        public string[] TargetTag;
    }

    [System.Serializable]
    public struct MoveInfo
    {
        public float MoveSpeed;
        public float MoveDistance;
        public int MoveDirection_X;
        public int MoveDirection_Y;
        public int MoveDirection_Z;
        public bool UseGravity;
    }



    public AttackParameter[] normalAttack;  
    public DeputyAttackCollection[] deputyAttackCollections;
    public AttackParameter[] specialAttack;


   // public ShootingAttack[] shootingAttack;
}
