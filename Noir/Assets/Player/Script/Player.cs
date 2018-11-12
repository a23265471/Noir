using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private PlayerDataCollection playerData;

    private void Awake()
    {
        playerData = GameFacade.GetInstance().PlayerDatas;
    }

    private void Start()
    {
        
    }

    public override void Move()
    {
        throw new System.NotImplementedException();
        
    }


}
