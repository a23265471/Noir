using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDialogue : MonoBehaviour {
    public TextAsset textFile;
    public GameObject textBox;
    public Text textDialogue;

    public int LineCount;
    public int startLine;
    public int currentLine;
    public int endLine;
    public string[] textLines;
    public bool textBox_Active;
    public bool destroyWhenActivated;


    void Start () {
        
        if (textFile != null)
        {
            textLines = (textFile.text.Split('\n')); 
        }
        if (LineCount == 0)
        {
            LineCount = textLines.Length-1 ;
        }
        if (textBox_Active)
        {
            EnableTextBox();
        }
        else
        {
            DisableTextBox();
        }
        
    }


    void Update () {
        if (!textBox_Active)
        {
            return;
        }
        textDialogue.text = textLines[currentLine];
        if (Input.GetMouseButtonDown(0) && currentLine < LineCount)
        {
            currentLine += 1;

        }
        if (currentLine >= LineCount)
        {
            DisableTextBox();
        }

    }
    public void EnableTextBox()
    {
        textBox.SetActive(true);
        textBox_Active=true;
    }
    public void DisableTextBox()
    {
        textBox.SetActive(false);
        textBox_Active = false;
    }
    public void ReloadScript(Text textDialogue)
    {
        if (textDialogue != null)
        {
            textLines = new string[1];
            textLines = (textDialogue.text.Split('\n'));
        }

    }
   void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ReloadScript(textDialogue);
            currentLine = startLine;
            LineCount = endLine;
            if (destroyWhenActivated)
            {
                Destroy(gameObject);
            }
        }
    }
}
