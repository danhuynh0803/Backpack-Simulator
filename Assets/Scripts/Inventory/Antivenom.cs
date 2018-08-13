using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Antivenom : Item {

    
    public override bool ActivateEffect()
    {
        Player player = FindObjectOfType<Player>();
        if (player.GetStatusEffect(Status.Poison) > 0)
        {
            player.RemoveStatusEffect(Status.Poison);
            player.isPoisoned = false;
            //Debug.Log("Player is cured");
            SoundController.Play((int)SFX.Potion, 0.5f);
            return true;
        }
        else
            return false;        
    }
}
