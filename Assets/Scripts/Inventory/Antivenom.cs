using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Antivenom : Item {

    
    public override bool ActivateEffect()
    {
        Player player = FindObjectOfType<Player>();
        player.isPoisoned = false;
        Debug.Log("Player is cured");
        SoundController.Play((int)SFX.Potion, 0.5f);
        return true;
    }
}
