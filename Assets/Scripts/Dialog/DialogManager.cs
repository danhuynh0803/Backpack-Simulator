using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {

    public float typingSpeed;
    public string[] exampleDialog;
    public Text textGameObject;
    public Scrollbar scrollbar;
    public Button nextButton;
    Queue<string> dialogSentences;
    private bool isPrintingText;
    public bool isInDialog;
    private string printingSentece;

    public void PrintTextInDialogBox(string sentence)
    {
        isPrintingText = true;
        scrollbar.interactable = false;
        StartCoroutine(Type(sentence));
    }

    IEnumerator Type(string sentence)
    {
        printingSentece = sentence;
        textGameObject.text += "\n\n";
        textGameObject.text.Replace("\n", System.Environment.NewLine);
        // Print each character one by one
        foreach (char letter in sentence)
        {
            textGameObject.text += letter;
            printingSentece = printingSentece.Substring(1);
            scrollbar.value = 0f;
            //dont know why i have to set it to backward lol
            yield return new WaitForSeconds(typingSpeed);
        }
        isPrintingText = false;
        scrollbar.interactable = true;
    }

    IEnumerator PrintCurrentSentenceImmediately()
    {
        foreach (char letter in printingSentece)
        {
            textGameObject.text += letter;
            yield return new WaitForSeconds(0.01f);
            scrollbar.value = 0f;
        }
        nextButton.interactable = true;
        isPrintingText = false;
    }

    public void StartDialog(Dialog dialog)
    {
        dialogSentences.Clear();
        isInDialog = true;
        foreach (string sentence in dialog.sentences)
        {
            dialogSentences.Enqueue(sentence);
        }
        ContinueToNextSentence();
    }

    public void ContinueToNextSentence()
    {
        if (isPrintingText)
        {
            StopAllCoroutines();
            StartCoroutine(PrintCurrentSentenceImmediately());
            scrollbar.interactable = true;
            nextButton.interactable = false;
        }
        else
        {
            if (dialogSentences.Count == 0)
            {
                EndDialog();
                return;
            }
            string sentence = dialogSentences.Dequeue();
            PrintTextInDialogBox(sentence);
        }
    }

    void EndDialog()
    {
        isInDialog = false;
    }
    private void Awake()
    {
        dialogSentences = new Queue<string>();
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
