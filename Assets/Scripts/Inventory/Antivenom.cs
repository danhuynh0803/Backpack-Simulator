using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Antivenom : Item {

    
    public override void ActivateEffect()
    {
        Player player = FindObjectOfType<Player>();
        player.isPoisoned = false;
        Debug.Log("Player is cured");
    }
}
