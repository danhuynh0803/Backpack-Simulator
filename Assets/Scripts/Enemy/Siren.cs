using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Siren : Enemy {

    public override void Attack()
    {
        Player player = FindObjectOfType<Player>();
        SoundController.Play((int)SFX.Siren, 0.5f);
        int damageDealt = player.DecrementHealth(damage - player.armor);
        string[] sentences =
            {
                "Siren's turn",
                "Siren deals " + damageDealt + " damage.",
            };
        dialogManager.PrintEnemyNextSentence(sentences);
    }

    public override void Death()
    {
    }
}
