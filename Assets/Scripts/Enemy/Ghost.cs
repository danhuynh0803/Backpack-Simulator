using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Enemy
{
    public override void Attack()
    {
        Player player = FindObjectOfType<Player>();
        int damageDealt = player.DecrementHealth(damage - player.GetArmor());
        SoundController.Play((int)SFX.Ghost, 0.5f);
        string[] sentences =
        {
           "Ghost's turn",
           "Ghost deals " + damageDealt + " damage.",
        };
        dialogManager.PrintEnemyNextSentence(sentences);
    }

    public override void Death()
    {
        Debug.Log("Enemy Death");
    }
}
