using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStageController : MonoBehaviour {

    private GameStageData gameStageData;

    public Transform playerStartPos;
  //  public GameObject player;
    public PlayerBehaviour playerBehaviour;

    public MainCamera_New mainCameraBehaviour;


    private bool CursorLocked;
    private bool GamePause;
    //public GameObject Weapon;

    private void Awake()
    {
        
        gameStageData = GameFacade.GetInstance().gameStageData;
        GameObject mainCamera = Instantiate(gameStageData.CurPlayerStageData.playerData.MainCamera, new Vector3(0,0,0), playerStartPos.rotation);
        GameObject player = Instantiate(gameStageData.CurPlayerStageData.playerData.Player, playerStartPos.position, playerStartPos.rotation);
        mainCameraBehaviour = mainCamera.GetComponent<MainCamera_New>();
        playerBehaviour = player.GetComponent<PlayerBehaviour>();
        
    }

    void Start ()
    {

    }

    void Update()
    {

    }

}
