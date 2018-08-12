using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoEffectItem : Item
{
    public override bool ActivateEffect()
    {
        return false;
    }
}
