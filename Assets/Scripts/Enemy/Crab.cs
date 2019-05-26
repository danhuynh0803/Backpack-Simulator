using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Crab : Enemy
{
    public int specialCooldownMax = 3;
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
            NormaAttack();
    }

    private void SpecialAttack()
    {
        Player player = FindObjectOfType<Player>();
        int damageDealt = player.DecrementHealth(damage * 2 - player.GetArmor());
        SoundController.Play((int)SFX.Bandits, 0.5f);
        string[] sentences =
            {
                "Bandit's turn",
                "Bandit uses Double Stab!",
                "Bandit deals " + damageDealt + " damage.",
            };
        dialogManager.PrintEnemyNextSentence(sentences);
    }

    private void NormaAttack()
    {
        Player player = FindObjectOfType<Player>();
        int damageDealt = player.DecrementHealth(damage - player.GetArmor());
        SoundController.Play((int)SFX.Bandits, 0.5f);
        string[] sentences =
            {
                "Bandit's turn",
                "Bandit deals " + damageDealt + " damage.",
            };
        dialogManager.PrintEnemyNextSentence(sentences);
    }

    public override void Death()
    {
        Debug.Log("Enemy Death");
    }
}
