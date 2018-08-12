using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalPotion : Item {

    public int elemental;

    public override void ActivateEffect()
    {
        FindObjectOfType<Player>().IncrementElemental(elemental);      
    }
}
