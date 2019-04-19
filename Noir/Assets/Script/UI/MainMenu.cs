using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour {

    public Animator LevelChanger_anim;

    public static MainMenu mainMenu;  
    public static int num;

    private void Awake()
    {
        mainMenu = this;

    }

    public void LoadGame(int sceneIndex)
    {
        num = sceneIndex;
        Time.timeScale = 1;
        SceneManager.LoadScene("Loading");
    }

    public void FadeToLevel()
    {
        LevelChanger_anim.SetTrigger("MainFadeOut");
    }
    public void FadeToSetting()
    {
        LevelChanger_anim.SetTrigger("ToGameSetting");
    }
    public void ChooseLevel()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene("ChooseLevel");
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
