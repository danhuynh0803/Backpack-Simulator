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

    private void Start()
    {
        index = 0;
        timeLeft = 0f;
    }

    IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        index++;
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            if (index >= 4)
            {
                startGame.SetActive(true);
            }
            else
            {
                dialogText.text = "";
                StartCoroutine(Type());
                timeLeft = waitTime;
            }
        }

    }
}