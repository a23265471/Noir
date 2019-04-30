using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyData : ScriptableObject
{

    [System.Serializable]
    public struct MoveInfo
    {
        public float ChaseDis;
        public float Acceleration;

       
    }

    [System.Serializable]
    public struct AttackDisInfo
    {
        public float ShortAttackDis;
        public float LongAttackDis;
    }

    [System.Serializable]
    public struct EnemyInfo
    {
        public MoveInfo moveInfo;
        public AttackDisInfo attackDisInfo;
    }

    public EnemyInfo enemyInfo;

}
