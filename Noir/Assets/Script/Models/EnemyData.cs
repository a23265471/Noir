using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyData : ScriptableObject
{

    [System.Serializable]
    public struct MoveInfo
    {
        public float ChaseProbability;
        public float ChaseDis;
        public float Acceleration;
        public float SlowDownAcceleration;
        public float BufferDis;
    }

    [System.Serializable]
    public struct AttackDisInfo
    {
        public string Name;
        public int ID;
        public float AttackDis;
        public float AttackProbability;
    }

    [System.Serializable]
    public struct EnemyInfo
    {
        public MoveInfo moveInfo;
        public AttackDisInfo[] attackDisInfo;
    }
    
    public EnemyInfo enemyInfo;


}
