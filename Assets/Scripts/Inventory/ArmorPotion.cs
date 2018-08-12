using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorPotion : Item {

    public int armor;

    public override bool ActivateEffect()
    {
        FindObjectOfType<Player>().IncrementArmor(armor);
        FindObjectOfType<Player>().AddPermanentWeight(this.weight);
        SoundController.Play((int)SFX.Potion, 0.5f);
        return true;
    }
}
