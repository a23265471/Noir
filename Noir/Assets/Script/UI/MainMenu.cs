using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    public Animator LevelChanger_anim;
    public GameObject loadingScene;
    public Slider loadingSlider;
    public Text progressText;

    // Use this for initialization

    public void LoadGame(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
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


    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        loadingScene.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            loadingSlider.value = progress;
            progressText.text = Mathf.Round(progress * 100f)+"%";
            yield return null;
        }
    }
}
