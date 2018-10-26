using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {
    public GameObject ESCPanel;
    public GameObject IntroPanel;
    public GameObject Options;
    //----------------------------------
    private Animator ESCPanel_Anim;
    public Animator IntroPanel_Anim;
    //----------------------------------
    private bool ESCPanel_Active;
    void Start()
    {
        ESCPanel_Active = false;
        ESCPanel.SetActive(false);
        Options.SetActive(false);
        IntroPanel.SetActive(false);
        ESCPanel_Anim =GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (ESCPanel_Active)
            {
                ESCPanel_Active = false;
                ESCPanel.SetActive(false);
                ESCPanel_Anim.SetBool("OpenESCPanel", false);
                IntroPanel_Anim.SetBool("OpenIntroPanel", false);
                IntroPanel.SetActive(false);
               
            }
            else
            {
                Options.SetActive(true);
                ESCPanel_Active = true;
                ESCPanel.SetActive(true);
                ESCPanel_Anim.SetBool("OpenESCPanel", true);
               
            }

        }
        
    }
    public void OpenIntro()
    {
       
        Options.SetActive(false);
        IntroPanel.SetActive (true);
        IntroPanel_Anim.SetBool("OpenIntroPanel",true);
        
     }
    public void CloseIntro()
    {
       
        Options.SetActive(true);
        IntroPanel.SetActive(false);
        IntroPanel_Anim.SetBool("OpenIntroPanel", false);
    }
   
}

