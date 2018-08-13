using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RougeElemental : Enemy
{
    public int specialCooldownMax = 3;
    private int specialCooldown;
    public int lightWeightMax;

    private void Start()
    {
        specialCooldown = specialCooldownMax;
    }

    public override void Attack()
    {
        specialCooldown--;
        if (specialCooldown <= 0)
        {
            SpecialAttack();
            specialCooldown = specialCooldownMax;
        }
        else
            NormalAttack();
    }
    public void NormalAttack()
    {
        Player player = FindObjectOfType<Player>();
        int damageDealt = player.DecrementHealth(damage);
        string[] sentences =
        {
           "Rouge Elemental's turn",
           "Rouge Elemental summons gale wind!",
           "Rouge Elemental deals " + damageDealt + " damage.",
        };
        dialogManager.PrintEnemyNextSentence(sentences);
    }
    public void SpecialAttack()
    {
        Player player = FindObjectOfType<Player>();
        int damageDealt = player.DecrementHealth(100 - player.GetWeight());
        string[] sentences =
        {
           "Rouge Elemental's turn",
           "Rouge Elemental summons a tornado!",
           "Rouge Elemental deals " + damageDealt + " damage.",
        };
        dialogManager.PrintEnemyNextSentence(sentences);
    }
    public override void Death()
    {
        Debug.Log("Enemy Death");
    }

}
