using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RougeElemental : Enemy
{

    public override void Attack()
    {
        Player player = FindObjectOfType<Player>();
        int damageDealt = player.DecrementHealth(damage - player.armor);
        string[] sentences =
        {
           "Rouge Elemental's turn",
           "Rouge Elemental deals " + damageDealt + " damage.",
        };
        dialogManager.PrintEnemyNextSentence(sentences);
    }

    public override void Death()
    {
        Debug.Log("Enemy Death");
    }
}
