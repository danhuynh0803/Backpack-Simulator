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
    public Text statusText;
    public Text gold;
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
        damage.text = "Attack: " + player.GetDamage().ToString();
        armor.text = "Armor: " + player.GetArmor().ToString();
        weight.text = "Total Weight: " + player.GetWeight();
        gold.text = "Gold: " + player.GetGold().ToString();
        UpdateStatusText();
    }

    public void UpdateStatusText()
    {
        string playerStatuses = "Player Status\n";

        int encumbered = player.GetStatusEffect(Status.Encumbered);
        int poisoned = player.GetStatusEffect(Status.Poison);
        int frost = player.GetStatusEffect(Status.Frost);
        int elemental = player.GetStatusEffect(Status.ElementalDamage);        

        if (encumbered > 0) {
            playerStatuses += "Encumbered " +
                player.GetStatusEffect(Status.Encumbered) + "\n"; // + " (Lose " + encumbered + " damage)\n";
        }
        if (poisoned > 0)
        {
            playerStatuses += "Poisoned " +
                player.GetStatusEffect(Status.Poison) + "\n"; //" (Lose " + poisoned + " health)\n";
        }
        if (frost > 0)
        {
            playerStatuses += "Frost " +
                player.GetStatusEffect(Status.Frost) + "\n"; // " (Lose " + frost + " armor)\n";
        }
        if (elemental > 0)
        {
            playerStatuses += "Elemental Dmg +" +
                player.GetStatusEffect(Status.ElementalDamage) + "\n";
        }

        statusText.text = playerStatuses;
    }
}
