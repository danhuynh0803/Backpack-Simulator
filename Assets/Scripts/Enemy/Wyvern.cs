using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wyvern : Enemy {

    public override void Attack()
    {
        Player player = FindObjectOfType<Player>();
        int damageDealt = player.DecrementHealth(damage - player.GetArmor());
        player.AddStatusEffect(Status.Poison, 1);
        SoundController.Play((int)SFX.Wyvern, 0.5f);
        string[] sentences =
            {
                "Wyvern's turn",
                "Wyvern splits midly poison!",
                "Wyvern deals " + damageDealt + " damage.",
                "Player got poisoned!"
            };
        dialogManager.PrintEnemyNextSentence(sentences);
    }

    public override void Death()
    {
    }
}
