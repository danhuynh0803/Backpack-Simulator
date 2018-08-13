using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class cathedralDialog : MonoBehaviour
{
    public Text dialogText;
    public string[] sentences;
    private int index;
    public float typingSpeed;

    private void Start()
    {
        StartCoroutine(Type());
    }

    private void Awake()
    {
        index = 0;
    }

    IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
            SoundController.Play((int)SFX.Cathedral, 0.5f);
        }
    }

    void Update()
    {
    }
}