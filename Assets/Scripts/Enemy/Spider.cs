using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy {

    public override void Attack()
    {

        

        Player player = FindObjectOfType<Player>();
        int damageDealt = player.DecrementHealth(damage - player.armor);
        float rate = UnityEngine.Random.Range(0.0f, 5.0f);
        if (rate > 3.0f)
        {
            player.isPoisoned = true;
            string[] sentences =
            {
                "Spider's turn",
                "Spider deals " + damageDealt + " damage.",
                "Player got poisoned!"
            };
            Dialog enemyTurn = new Dialog("enemy turn", sentences);
            dialogManager.isInDialog = true;
            dialogManager.StartDialog(enemyTurn);
        }
        else
        {
            string[] sentences =
            {
                "Spider's turn",
                "Spider deals " + damageDealt + " damage.",
            };
            Dialog enemyTurn = new Dialog("enemy turn", sentences);
            dialogManager.isInDialog = true;
            dialogManager.StartDialog(enemyTurn);
        }
    }

    public override void Death()
    {
        Debug.Log("Enemy Death");
    }
}
