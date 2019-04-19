using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class PlayVideo : MonoBehaviour {
       
   
    void Update()
    {
        
        StartCoroutine(LoadScene());
    
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(35f);//括號內填入影片時間

        SkipMovie();//載入場景
    }
   public void SkipMovie()
    {
        SceneManager.LoadScene("Main");
    }




}
