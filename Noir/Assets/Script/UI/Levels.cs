using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levels : MonoBehaviour {
    private Animator LevelSelect_anim;
    public GameObject LevelSelect_1;
    public GameObject LevelSelect_2;
    public GameObject LevelSelect_2_Lv2;
    // Use this for initialization
    void Start () {
        LevelSelect_anim = GetComponent<Animator>();
        LevelSelect_1.SetActive(true);
        LevelSelect_2.SetActive(false);
        LevelSelect_2_Lv2.SetActive(false);

    }
    public void OpenLevel_Lv1()
    {
        LevelSelect_anim.SetBool("Level2_Open", true);    
    }
    public void OpenLevel_Lv2()
    {
        LevelSelect_anim.SetBool("OpenLv2", true);
    }

    public void CloseLevelAnim()
    {
        LevelSelect_anim.SetBool("Level2_Open", false);
        LevelSelect_anim.SetBool("OpenLv2", false);
    }
    public void CloseLevel_LV1()
    {
        LevelSelect_2.SetActive(false);
        LevelSelect_1.SetActive(true);

    }
    public void CloseLevel_LV2()
    {
        LevelSelect_2_Lv2.SetActive(false);
        LevelSelect_1.SetActive(true);

    }

}
