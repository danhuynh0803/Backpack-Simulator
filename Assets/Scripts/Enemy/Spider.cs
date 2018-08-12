using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy {

    public override void Attack()
    {
        Player player = FindObjectOfType<Player>();
        SoundController.Play((int)SFX.Spider, 0.5f);
        int damageDealt = player.DecrementHealth(damage - player.armor);
        float rate = UnityEngine.Random.Range(0.0f, 5.0f);
        if (rate > 3.0f)
        {
            //player.isPoisoned = true;
            player.AddStatusEffect(Status.Poison, 2);
            string[] sentences =
            {
                "Spider's turn",
                "Spider splits deadly poison!",
                "Spider deals " + damageDealt + " damage.",
                "Player got poisoned!"
            };
            dialogManager.PrintEnemyNextSentence(sentences);
        }
        else
        {
            string[] sentences =
            {
                "Spider's turn",
                "Spider deals " + damageDealt + " damage.",
            };
            dialogManager.PrintEnemyNextSentence(sentences);
        }
    }

    public override void Death()
    {
        Debug.Log("Enemy Death");
    }
}
