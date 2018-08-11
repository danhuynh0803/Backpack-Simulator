using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Handles all UI related functions
// TODO extract UI element from the GameController
public class UI : MonoBehaviour {

    [Header("UI Settings")]
    public Text scoreText;
    public Text highScoreText;
    public Text retryText;
    public Text livesText;

    void Update()
    {
        UpdateAllText();
    }

    public void UpdateAllText()
    {
        UpdateLivesText();
    }

    public void UpdateLivesText()
    { 

    }

    public void UpdateScoresText()
    {

    }
}
