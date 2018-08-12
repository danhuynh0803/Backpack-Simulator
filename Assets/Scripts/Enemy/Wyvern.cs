using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wyvern : Enemy {

    public override void Attack()
    {
        Player player = FindObjectOfType<Player>();
        int damageDealt = player.DecrementHealth(damage - player.armor);
        SoundController.Play((int)SFX.Wyvern, 0.5f);
        string[] sentences =
            {
                "Wyvern's turn",
                "Wyvern deals " + damageDealt + " damage.",
            };
        dialogManager.PrintEnemyNextSentence(sentences);
    }

    public override void Death()
    {
    }
}
