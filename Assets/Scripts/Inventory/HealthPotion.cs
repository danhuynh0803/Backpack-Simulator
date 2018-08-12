using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : Item {

    public int healAmount;
    
    public override bool ActivateEffect()
    {
        Player player = FindObjectOfType<Player>();
        if (player.GetHealth() < player.maxHealth)
        {
            FindObjectOfType<Player>().IncrementHealth(healAmount);
            SoundController.Play((int)SFX.Potion, 0.5f);
            return true;
        }
        else
        {
            Debug.Log("Player is already at max health");
            return false;
        }
    }
    
}
