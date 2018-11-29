using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levels : MonoBehaviour {
    private Animator LevelSelect_anim;
    public GameObject LevelSelect_1;
    public GameObject LevelSelect_2;
    // Use this for initialization
    void Start () {
        LevelSelect_anim = GetComponent<Animator>();
        LevelSelect_1.SetActive(true);
        LevelSelect_2.SetActive(false);
	}
    public void OpenLevel()
    {
        LevelSelect_anim.SetBool("Level2_Open", true);    
    }
    
    public void CloseLevelAnim()
    {
        LevelSelect_anim.SetBool("Level2_Open", false);
    }
    public void CloseLevel()
    {
        LevelSelect_2.SetActive(false);
        LevelSelect_1.SetActive(true);

    }

}
