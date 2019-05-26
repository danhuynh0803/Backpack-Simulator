using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : Enemy
{
    public bool isTreasure;

    public override void Attack()
    {
    }

    public override void Death()
    {
        Debug.Log("Enemy Death");
    }
}
