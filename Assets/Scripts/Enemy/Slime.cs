using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    public int specialCooldownMax = 2;
    private int specialCooldown;

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
            NomralAttack();
    }

    public void SpecialAttack()
    {
        Player player = FindObjectOfType<Player>();
        armor += 2;
        player.DecrementArmor(2);
        string[] sentences =
        {
           "Frost Slime uses Frosty Curse",
           "Frost Slime's armor is increases by 2!",
           "Player's armor is decreased by 2!"
        };
        dialogManager.PrintEnemyNextSentence(sentences);
    }

    public void NomralAttack()
    {
        Player player = FindObjectOfType<Player>();
        int damageDealt = player.DecrementHealth(damage - player.armor);
        string[] sentences =
        {
           "Frost Slime's turn",
           "Frost Slime deals " + damageDealt + " damage.",
        };
        dialogManager.PrintEnemyNextSentence(sentences);
    }

    public override void Death()
    {
        Debug.Log("Enemy Death");
    }
}
