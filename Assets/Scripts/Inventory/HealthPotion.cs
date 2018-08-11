using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : Item {

    public int healAmount;
    
    public override void ActivateEffect()
    {
        FindObjectOfType<Player>().IncrementHealth(healAmount);
    }
    
}
