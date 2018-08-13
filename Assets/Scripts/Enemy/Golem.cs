using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : Enemy {


    public override void Attack()
    {
        Player player = FindObjectOfType<Player>();
        int damageDealt = player.DecrementHealth(damage - player.GetArmor());
        SoundController.Play((int)SFX.StoneGolem, 0.5f);
        string[] sentences =
            {
                "Ancient Golem's turn",
                "Ancient Golem deals " + damageDealt + " damage.",
            };
        dialogManager.PrintEnemyNextSentence(sentences);
    }

    public override void Death()
    {
        Debug.Log("Enemy Death");
    }
}
