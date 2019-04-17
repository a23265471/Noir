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

    }



    [System.Serializable]
    public struct EnemyInfo
    {
        public MoveInfo moveInfo;

    }

    public EnemyInfo enemyInfo;

}
