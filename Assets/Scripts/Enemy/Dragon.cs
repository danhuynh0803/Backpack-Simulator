using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : Enemy {

    public override void Attack()
    {
        Player player = FindObjectOfType<Player>();
        SoundController.Play((int)SFX.Dragon, 0.5f);
        int damageDealt = player.DecrementHealth(damage - player.armor);
        string[] sentences =
            {
                "Dragon's turn",
                "Dragon deals " + damageDealt + " damage.",
                "Rawrrrrrrrrrrr"
            };
        dialogManager.PrintEnemyNextSentence(sentences);
    }

    public override void Death()
    {
        Debug.Log("Enemy Death");
    }
}
