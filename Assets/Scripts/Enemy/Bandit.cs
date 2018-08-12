using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandit : Enemy {

    public override void Attack()
    {
        Player player = FindObjectOfType<Player>();
        int damageDealt = player.DecrementHealth(damage - player.armor);
        string[] sentences =
            {
                "Bandit's turn",
                "Bandit deals " + damageDealt + " damage.",
            };
        Dialog enemyTurn = new Dialog("enemy turn", sentences);
        dialogManager.isInDialog = true;
        dialogManager.StartDialog(enemyTurn);
    }

    public override void Death()
    {
        Debug.Log("Enemy Death");
    }
}
