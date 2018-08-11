using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Enemy {

    public override void Attack()
    {
        Debug.Log("Enemy Attack");
        Player player = FindObjectOfType<Player>();
        player.DecrementHealth(damage - player.armor);
    }

    public override void Death()
    {
        Debug.Log("Enemy Death");
    }
}
