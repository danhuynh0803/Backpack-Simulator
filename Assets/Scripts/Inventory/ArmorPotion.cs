using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorPotion : Item {

    public int armor;

    public override bool ActivateEffect()
    {
        FindObjectOfType<Player>().IncrementArmor(armor);
        return true;
    }
}
