using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {
    public GameObject openESCPanel;
    private Animator ESCPanel_anim;
    private bool ESC_PanelActive;
    void Start()
    {
        openESCPanel.SetActive(false);
        ESC_PanelActive = false;
        ESCPanel_anim = openESCPanel.GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (ESC_PanelActive)
            {
                ESC_PanelActive = false;
                openESCPanel.SetActive(false);
                
            }
            else
            {
                ESC_PanelActive = true;
                openESCPanel.SetActive(true);
               
            }
            
        }
        
    }
    public void OpenIntro()
    {

    }
}

