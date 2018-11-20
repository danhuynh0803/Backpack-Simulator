using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class NewDialog : MonoBehaviour
{
    public Text dialogText;
    public string[] sentences;
    private int index;
    public float typingSpeed;
    private float waitTime = 8f;
    float timeLeft;
    public GameObject startGame;
    public GameObject next;

    private void Start()
    {
        index = 0;
        timeLeft = 0f;
        next.GetComponent<Button>().interactable = false;
        StartCoroutine(Type());
    }

    IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        next.GetComponent<Button>().interactable = true;
        if(index >= 3)
        {
            startGame.SetActive(true);
            next.SetActive(false);
        }
    }
    public void PrintNextSentence()
    {
        SoundController.Play((int)SFX.Cathedral, 0.5f);
        index++;
        if (index < 4)
        {
            dialogText.text = "";
            StartCoroutine(Type());
            next.GetComponent<Button>().interactable = false;
        }
    }
}