using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalPotion : Item {

    public int elemental;

    public override bool ActivateEffect()
    {
        FindObjectOfType<Player>().AddStatusEffect(Status.ElementalDamage, elemental);
        SoundController.Play((int)SFX.Potion, 0.5f);
        return true;
    }
}
