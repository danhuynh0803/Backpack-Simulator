using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Handles all UI related functions
// TODO extract UI element from the GameController
public class UI : MonoBehaviour {

    [Header("UI Settings")]
    public Text health;
    public Text damage;
    public Text armor;
    public Text weight;
    public Text playerStatuses;

    public Slider healthSlider;

    private Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        healthSlider.maxValue = player.maxHealth;
    }

    void Update()
    {
        UpdateAllText();
    }

    public void UpdateAllText()
    {
        health.text = player.GetHealth() + "/" + player.maxHealth;
        healthSlider.value = player.GetHealth();
        damage.text = "Attack: " + player.damage.ToString();
        armor.text = "Armor: " + player.armor.ToString();
        weight.text = "Total Weight: " + player.GetWeight();
    }

}
