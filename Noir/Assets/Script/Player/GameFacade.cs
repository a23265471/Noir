using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[RequireComponent(typeof(playerController))]

public class GameFacade : MonoBehaviour
{
    private static GameFacade instance;
    public static GameFacade GetInstance()
    {
        if (instance == null)
        {
            instance = GameObject.FindObjectOfType<GameFacade>();
            if (instance == null)
            {
                throw new System.Exception("GameFacade不存在於場景中，請在場景中添加");
            }
            instance.Initialize();
        }
        return instance;
    }

    #region Controller
    
    #endregion

    #region Model
    public PlayerDataCollection PlayerDatas;

    #endregion

    private void Initialize()
    {
       

    }
    private void Awake()
    {
        GetInstance();
    }
}
