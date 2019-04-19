using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroPages : MonoBehaviour
{
    public GameObject[] Pages;
    public GameObject Page_Active;
   // public Text Page_Num;
    public int x;
    public int Page_CurrentNum
    {
        get//存取子可擷取資料成員值
        {
            return x;
        }
        set//存取子可讓資料成員被指派
        {
            x = value;
            if (Page_CurrentNum < 0)
                Page_CurrentNum = Pages.Length - 1;
            else if (Page_CurrentNum > Pages.Length - 1)
                Page_CurrentNum = 0;
            SetActivePage();
        }
    }

   

    private void Start()
    {
        
        if (Pages.Length == 0)
            return;//return :直接跳出結束那個函式，不會繼續執行迴圈外的程式

        Page_Active = Pages[Page_CurrentNum];
        SetActivePage();
        
    }

  

    public void Prev()
    {
        Page_CurrentNum--;
    }

    public void Next()
    {
        Page_CurrentNum++;
    }

    private void SetActivePage()
    {
        Page_Active.SetActive(false);
        Page_Active = Pages[Page_CurrentNum];
        Page_Active.SetActive(true);
        //Page_Num.text = "" + (Page_CurrentNum + 1) + "/" + Pages.Length.ToString();
    }
}
