using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Siren : Enemy {

    public override void Attack()
    {
        Player player = FindObjectOfType<Player>();
        int damageDealt = player.DecrementHealth(damage - player.armor);
        string[] sentences =
            {
                "Siren's turn",
                "Siren deals " + damageDealt + " damage.",
            };
        Dialog enemyTurn = new Dialog("enemy turn", sentences);
        dialogManager.isInDialog = true;
        dialogManager.StartDialog(enemyTurn);
    }

    public override void Death()
    {
    }
}
