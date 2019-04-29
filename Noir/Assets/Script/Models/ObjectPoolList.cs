using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ObjectPoolList : ScriptableObject
{
    [System.Serializable]
    public struct Object
    {
        public string Name;
        public int Id;
        public GameObject gameObject;
        public int Amount;

    }

    public Object[] ObjectPool;

}
