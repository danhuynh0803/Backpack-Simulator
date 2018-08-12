using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy {

    public override void Attack()
    {
        Debug.Log("Enemy Attack");
        Player player = FindObjectOfType<Player>();
        player.DecrementHealth(damage - player.armor);
        float rate = UnityEngine.Random.Range(0.0f, 5.0f);
        if (rate > 3.0f)
        {
            player.isPoisoned = true;
            Debug.Log("Player got poisoned");
        }
    }

    public override void Death()
    {
        Debug.Log("Enemy Death");
    }
}
