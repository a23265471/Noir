using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private bool CursorLocked;
    private bool GamePause;

   // public GameObject END_Panel;

    // Use this for initialization
    void Start()
    {

        CursorLocked = true;
        Screen.lockCursor = true;
       // END_Panel.SetActive(false);

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

        if (PlayerBehaviour.playerBehaviour.playerState == PlayerBehaviour.PlayerState.Dead)
        {
            StartCoroutine("GoToMainScene");
           // END_Panel.SetActive(true);
        }
       
    }

    IEnumerator GoToMainScene()
    {
        yield return new WaitForSeconds(2);
        Time.timeScale = 0;
        
       
        BackToMainMenu();
    }

    private void CursorLock()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.Escape)) 
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
        Screen.lockCursor = false;
        SceneManager.LoadScene("Lighting");
    }
    public void ChooseLevel()
    {
        Screen.lockCursor = false;
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
        Screen.lockCursor = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
    public void Back()
    {
        
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

}
