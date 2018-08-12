using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Handles all UI related functions
// TODO extract UI element from the GameController
public class EnemyUI : MonoBehaviour {

    [Header("UI Settings")]
    public GameObject enemyStatusPanel;
    public Text name;
    public Text health;
    public Text damage;
    public Text armor;    
    public Text enemyStatus;

    public Slider healthSlider;

    private Enemy enemy;
    private GameController gameController;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
        enemy = gameController.GetEnemyForUI();
        if (enemy == null)
        {
            enemyStatusPanel.SetActive(false);
        }
        else if (enemy.maxHealth <= 0)
        {
            enemyStatusPanel.SetActive(false);
        }
    }

    void Update()
    {
        enemy = gameController.GetEnemyForUI();
        if (enemy == null)
        {
            enemyStatusPanel.SetActive(false);
        }
        else if (enemy.maxHealth <= 0)
        {
            enemyStatusPanel.SetActive(false);
        }
        else
        {
            enemyStatusPanel.SetActive(true);
            UpdateAllText();
        }       
    }

    public void UpdateAllText()
    {
        name.text = enemy.name;
        health.text = enemy.health + "/" + enemy.maxHealth;
        healthSlider.maxValue = enemy.maxHealth;
        healthSlider.value = enemy.health;
        damage.text = "Attack: " + enemy.damage.ToString();
        armor.text = "Armor: " + enemy.armor.ToString();
        UpdateStatusText();
    }

    public void UpdateStatusText()
    {

    }

}
