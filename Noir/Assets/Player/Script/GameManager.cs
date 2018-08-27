using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private bool CursorLocked;
    
    // Use this for initialization
    void Start () {

        CursorLocked = true;
        Screen.lockCursor = true;

    }
	
	// Update is called once per frame
	void Update ()
    {

        CursorLock();
    }

    private void CursorLock()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (Screen.lockCursor)
            {
                Screen.lockCursor = false;
                 
            }
            else
            {
                Screen.lockCursor = true;
                
            }
            
        }
        
    }
}
