using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private bool CursorLocked;
    private bool GamePause;

    // Use this for initialization
    void Start()
    {

        CursorLocked = true;
        Screen.lockCursor = true;

    }

	// Update is called once per frame
	void Update ()
    {

        CursorLock();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!GamePause)
            {
                Time.timeScale = 0;
                GamePause = true;
            }
            else
            {
                Time.timeScale = 1;
                GamePause = false;

            }
            
        }
       
    }

    private void CursorLock()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.Escape)) 
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
    public void PlayGame()
    {
        Debug.Log("Play game");
        Time.timeScale = 1;
        SceneManager.LoadScene("Lighting");
    }
    public void ChooseLevel()
    {
        SceneManager.LoadScene("ChooseLevel");
        Time.timeScale = 1;
    }
    public void QuitGame()
    {
        Debug.Log("Quit game");
        Application.Quit();
    }
    public void BackToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
    public void Back()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

}
