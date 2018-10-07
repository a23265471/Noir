using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class PlayVideo : MonoBehaviour {
   
    void Start()
    {
      
    }
    void Update()
    {
        
        StartCoroutine(LoadScene());
    
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(10f);//括號內填入影片時間
     
        SceneManager.LoadScene("Main");//載入場景
    }
   public void SkipMovie()
    {
        SceneManager.LoadScene("Main");
    }




}
