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
        GameObject currentGameObject;

        if (objectPool.GetObject(objectID) == null)
        {

            return null;
        }
        currentGameObject = objectPool.GetObject(objectID);
        currentGameObject.transform.position = ObjectPoolItemInfo[objectID].ObjectPoolItemStartTransform.position;
        currentGameObject.transform.rotation = ObjectPoolItemInfo[objectID].ObjectPoolItemStartTransform.rotation;
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
