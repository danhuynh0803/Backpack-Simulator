using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyButtonEncounters : MonoBehaviour {

    public GameObject encounterPanel;
    public GameObject encounterButton;
    public GameObject enemyObject;
    private GameController gameController;

    public void StartEncounter()
    {
        gameController = FindObjectOfType<GameController>(); 
        if (enemyObject != null)
        {
            encounterPanel.SetActive(false);
            encounterButton.SetActive(false);
            gameController.SetupCombat(enemyObject);
        }
        else
        {
            Debug.LogError("No enemy object attached to this encounter");
        }
    }
}
