using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {

    private float typingSpeed = 0.01f;
    public string[] exampleDialog;
    public Text textGameObject;
    public Scrollbar scrollbar;
    public Button nextButton;
    Queue<string> dialogSentences;
    private bool isPrintingText;
    private bool isReadyForNewDialog;
    public bool isInDialog;
    private string printingSentece;

    public void PrintTextInDialogBox(string sentence)
    {
        nextButton.interactable = false;
        isPrintingText = true;
        StartCoroutine(Type(sentence));
    }

    IEnumerator Type(string sentence)
    {
        printingSentece = sentence;
        textGameObject.text += "\n";
        // Print each character one by one
        foreach (char letter in sentence)
        {
            if (System.Char.IsNumber(letter))
            { 
                textGameObject.text += "<color=#ff0000ff>";
                textGameObject.text += letter;
                textGameObject.text += "</color>";
            }
            else
                textGameObject.text += letter;
            printingSentece = printingSentece.Substring(1);
            scrollbar.value = 0f;
            yield return new WaitForSeconds(typingSpeed);
        }
        isPrintingText = false;
        ContinueToNextSentence();
    }

    /*IEnumerator PrintCurrentSentenceImmediately()
    {
        foreach (char letter in printingSentece)
        {
            if (System.Char.IsNumber(letter))
            {
                textGameObject.text += "<color=#ff0000ff>";
                textGameObject.text += letter;
                textGameObject.text += "</color>";
            }
            else
                textGameObject.text += letter;
            yield return new WaitForSeconds(0.01f);
            scrollbar.value = 0f;
        }
        nextButton.interactable = true;
        isPrintingText = false;
    }*/

    public void StartDialog(Dialog dialog)
    {
        dialogSentences.Clear();
        isInDialog = true;
        isReadyForNewDialog = false;
        nextButton.interactable = true;
        foreach (string sentence in dialog.sentences)
        {
            dialogSentences.Enqueue(sentence);
        }
        //super printing text mode
        //ContinueToNextSentence();
    }

    public void ContinueToNextSentence()
    {
        if (dialogSentences.Count == 0)
        {
            if (GameController.instance.isInCombat)
            {
                
                if(isReadyForNewDialog ==true)
                {
                    //run after we click the next after last setence
                    isInDialog = false;
                    return;
                }
                else
                {
                    //run automatically after last sentence
                    EndDialog();
                    return;
                }
            }
            else
            {
                isInDialog = false;
                return;
            }
        }
        string sentence = dialogSentences.Dequeue();
        PrintTextInDialogBox(sentence);
    }

    void EndDialog()
    {
        textGameObject.text += "\n";
        textGameObject.text.Replace("\n", System.Environment.NewLine);
        nextButton.interactable = true;
        isReadyForNewDialog = true;
    }
    private void Awake()
    {
        dialogSentences = new Queue<string>();
    }

    public void PrintEnemyNextSentence(string[] sentences)
    {
        Dialog playerTurn = new Dialog("enemy turn", sentences);
        isInDialog = true;
        StartDialog(playerTurn);
        ContinueToNextSentence();
    }
}
[System.Serializable]
public class Dialog
{
    public string name;
    public string[] sentences;

    public Dialog(string name, string[] sentences)
    {
        this.name = name;
        this.sentences = new string[sentences.Length];
        for(int i = 0; i < sentences.Length; i++)
        {
            this.sentences[i] = sentences[i];
        }
    }

    public Dialog(string combatText)
    {
        name = "player";
        sentences = new string[1] { combatText };  
    }

}
