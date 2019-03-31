using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour {

    public Animator LevelChanger_anim;

    public static MainMenu mainMenu;  
    public static int num;
   
    public void LoadGame(int sceneIndex)
    {
        num = sceneIndex;
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
        SceneManager.LoadScene("ChooseLevel");
    }
    public void QuitGame()
    {
        Debug.Log("Quit game");
        Application.Quit();
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene(1);
    }
    public void Back()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
          
}
