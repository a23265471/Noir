using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour {

    private ObjectPool objectPool;

    [System.Serializable]
    public struct objectPoolItemInfo
    {
        public string Name;
        public int ID;
        public Transform ObjectPoolItemStartTransform;
    }

    public objectPoolItemInfo[] ObjectPoolItemInfo;

    private void Awake()
    {
        objectPool = GameFacade.GetInstance().objectPool;

    }

    public GameObject GetObjectPool(int objectID)
    {

        if (objectPool.GetObject(objectID) == null)
        {
            Debug.Log("jjj");

            return null;
        }
        objectPool.GetObject(objectID).transform.position = ObjectPoolItemInfo[objectID].ObjectPoolItemStartTransform.position;
        objectPool.GetObject(objectID).transform.rotation = ObjectPoolItemInfo[objectID].ObjectPoolItemStartTransform.rotation;
        objectPool.GetObject(objectID).SetActive(true);
        return objectPool.GetObject(objectID);


    }

  /*  private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            GetObjectPool(0);
        }
    }*/


}
