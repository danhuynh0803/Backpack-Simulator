﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorPotion : Item {

    public int armor;

    public override void ActivateEffect()
    {
        FindObjectOfType<Player>().IncrementArmor(armor);      
    }
}
