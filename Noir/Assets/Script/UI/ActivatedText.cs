using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivatedText : MonoBehaviour {
    public GameDialogue textBox;
    public TextAsset textDialogue;
    public int startLine;
    public int endLine;
    public bool destroyWhenActivated;
    public bool requireButtonPress;

    private bool waitForPress;



    void Start () {
        textBox = FindObjectOfType<GameDialogue>();
	}	
	void Update () {
        if (waitForPress && Input.GetMouseButtonDown(1))
        {
            textBox.ReloadScript(textDialogue);
            textBox.currentLine = startLine;
            textBox.LineCount = endLine;
            textBox.EnableTextBox();
            if (destroyWhenActivated)
            {
                Destroy(gameObject);
            }
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (requireButtonPress)
            {
                waitForPress = true;
                return;
            }
            textBox.ReloadScript(textDialogue);
            textBox.currentLine = startLine;
            textBox.LineCount = endLine;
            textBox.EnableTextBox();
           if (destroyWhenActivated)
            {
                Destroy(gameObject);
            }
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            waitForPress = false;
        } 

    }

}
