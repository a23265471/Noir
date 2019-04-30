using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {

    public static ObjectPool objectPool;

    // public GameObject LongAttack;

    public ObjectPoolList objectPoolList;
    public Dictionary<int,GameObject> ObjectsPoolItem;

    private void Awake()
    {
        objectPool = this;
        CreatObjectPool();
    }

    void Start ()
    {
      
    }

    private void CreatObjectPool()
    {
        ObjectsPoolItem = new Dictionary<int, GameObject>();

        for (int i=0;i< objectPoolList.ObjectPool.Length; i++)
        {
            for(int j = 0; j < objectPoolList.ObjectPool[i].Amount; j++)
            {
                GameObject gameObject = Instantiate(objectPoolList.ObjectPool[i].gameObject);
                gameObject.SetActive(false);
                int id = objectPoolList.ObjectPool[i].Id + j;
                ObjectsPoolItem[id] = gameObject;
              //  Debug.Log(id);
            }

        }


    }

    public GameObject GetObject(int gameObjectId)
    {
     //   int itemId=0;
        for(int i=0;i< objectPoolList.ObjectPool[gameObjectId].Amount; i++)
        {
            int id = gameObjectId + i;
            if (!ObjectsPoolItem[id].activeInHierarchy)
            {
                return ObjectsPoolItem[id];
            }

        }


        return null;
    }

/*	public GameObject LongAttackObj()
    {
        for (int i = 0; i < LongAttacks.Count; i++)
        {
            if (!LongAttacks[i].activeInHierarchy)
            {
                return LongAttacks[i];
            }
        }

        return null;

    }*/
}
