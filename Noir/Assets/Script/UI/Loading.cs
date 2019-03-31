using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Loading : MonoBehaviour {
    public static Loading loading;
    public Slider loadingSlider;
    public Text progressText;
   
     void Start()
    {

        int sceneIndex = MainMenu.num;
             
        StartCoroutine(LoadAsynchronously(sceneIndex));
        
    }
   
    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            loadingSlider.value = progress;
            progressText.text = Mathf.Round(progress * 100f) + "%";
            yield return null;

        }
    }
}
