using System;
using UnityEngine;

public class GhostItem : MonoBehaviour
{
    //持续时间
    public float duration;
    //销毁时间
    public float deleteTime;

    public MeshRenderer meshRenderer;

    void Update()
    {
        
        float tempTime = deleteTime - Time.time;
        
        if (tempTime <= 0)
        {
            GameObject.Destroy(this.gameObject);
            /*this.gameObject.SetActive(false);
            Debug.Log(tempTime);
            Destroy(this.gameObject.GetComponent(GhostItem));*/
        }
        

    }
}