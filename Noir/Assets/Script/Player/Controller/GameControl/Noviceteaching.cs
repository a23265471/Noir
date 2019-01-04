using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Noviceteaching : MonoBehaviour
{
    public Sprite[] TeachImageSource;
    private Image TeachImge;

    IEnumerator changeAlpha = null;

    public bool aa;

    private void Awake()
    {
        TeachImge = GetComponent<Image>();
    }

    private void Start()
    {       
        TeachImge.color = new Color(TeachImge.color.r, TeachImge.color.b, TeachImge.color.g, 0);
        LoadImage(0);
    }

    private void Update()
    {
        if (aa)
        {
            UnLoadImage();
            aa = false;
        }
        
    }


    public void LoadImage(int loadSprite)
    {
        TeachImge.sprite = TeachImageSource[loadSprite];
        FadeInOut(TeachImge.color.a, 1);
    }

    public void UnLoadImage()
    {
        FadeInOut(TeachImge.color.a, 0);

    }


    private void StartSlowMotion()
    {
        Time.timeScale = 0.5f;

    }

    private void  FadeInOut(float imageAlpha,float targetAlpha)
    {
        
        changeAlpha = ChangeAlpha(imageAlpha, targetAlpha);
        StopCoroutine(changeAlpha);
        StartCoroutine(changeAlpha);

    }
         
    IEnumerator ChangeAlpha(float imageAlpha, float targetAlpha)
    {
        imageAlpha = Mathf.Lerp(imageAlpha, targetAlpha, 0.1f);
        if (imageAlpha <= targetAlpha + 0.1f && imageAlpha >= targetAlpha - 0.1f) 
        {
            imageAlpha = targetAlpha;
        }
        TeachImge.color = new Color(TeachImge.color.r, TeachImge.color.b, TeachImge.color.g, imageAlpha);

        yield return new WaitForSeconds(0.01f);

        if (imageAlpha == targetAlpha) 
        {
            StopCoroutine(changeAlpha);

        }
        else
        {
            changeAlpha = ChangeAlpha(imageAlpha, targetAlpha);
            StartCoroutine(changeAlpha);
            
        }
    }

}
