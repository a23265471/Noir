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

    public Dictionary<int, objectPoolItemInfo> ObjectPoolItemInfoCollection;

    private void Awake()
    {
        objectPool = GameFacade.GetInstance().objectPool;
        CreatObjectPoolItemInfoCollection();
    }

    private void CreatObjectPoolItemInfoCollection()
    {
        ObjectPoolItemInfoCollection = new Dictionary<int, objectPoolItemInfo>();

        for (int i=0;i< ObjectPoolItemInfo.Length; i++)
        {
            ObjectPoolItemInfoCollection[ObjectPoolItemInfo[i].ID] = ObjectPoolItemInfo[i];

        }

    }



    public GameObject GetObjectPool(int objectID)
    {
        GameObject currentGameObject;

        if (objectPool.GetObject(objectID) == null)
        {

            return null;
        }
        currentGameObject = objectPool.GetObject(objectID);
        currentGameObject.transform.position = ObjectPoolItemInfoCollection[objectID].ObjectPoolItemStartTransform.position;
        currentGameObject.transform.rotation = ObjectPoolItemInfoCollection[objectID].ObjectPoolItemStartTransform.rotation;
       // Debug.Log("jjj");

        return currentGameObject;


    }



  /*  private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            GetObjectPool(0);
        }
    }*/


}
