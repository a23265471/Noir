using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {

    public static ObjectPool objectPool;

    // public GameObject LongAttack;

    public ObjectPoolList objectPoolList;
    public Dictionary<int, GameObject> ObjectsPoolItem;
    public Dictionary<int, ObjectPoolList.Object> ObjectInfo;


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
        ObjectInfo = new Dictionary<int, ObjectPoolList.Object>();
        for (int i=0;i< objectPoolList.ObjectPool.Length; i++)
        {
            ObjectInfo[objectPoolList.ObjectPool[i].Id] = objectPoolList.ObjectPool[i];
            for (int j = 0; j < objectPoolList.ObjectPool[i].Amount; j++)
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
        for(int i=0;i< ObjectInfo[gameObjectId].Amount; i++)
        {
            int id = gameObjectId + i;
            if (!ObjectsPoolItem[id].activeInHierarchy)
            {
                return ObjectsPoolItem[id];
            }

        }


        return null;
    }


}
