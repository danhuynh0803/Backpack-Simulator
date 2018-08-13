using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Siren : Enemy {

    public int specialCooldownMax = 3;
    private int specialCooldown;
    public int healAmount;

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

    void NormalAttack()
    {
        Player player = FindObjectOfType<Player>();
        SoundController.Play((int)SFX.Siren, 0.5f);
        int damageDealt = player.DecrementHealth(damage - player.GetArmor());
        string[] sentences =
            {
                "Siren's turn",
                "Siren deals " + damageDealt + " damage.",
            };
        dialogManager.PrintEnemyNextSentence(sentences);
    }
    void SpecialAttack()
    {
        health += healAmount;
        string[] sentences =
            {
                "Siren's turn",
                "Siren sings Song of the Sea and restores " +
                health + " health.",
            };
        dialogManager.PrintEnemyNextSentence(sentences);
    }

    public override void Death()
    {
    }
}
