using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    #region Singleton
    /*
    public static Player player;
    
    private void Awake()
    {
        if (player == null)
        {
            player = this;
        }
    }
    */
    #endregion

    public int maxHealth = 100;
    private int health;
    public int damage;
    public int armor;
    private int weight;
    private Inventory inventory;

    private void Awake()
    {
        health = maxHealth;
        Debug.Log("Player health: " + health);
    }

    // Attack the enemy that is set in the combat encounter in gamecontroller
    public void Attack()
    {
        Enemy enemy = GameController.instance.enemy;
        if (enemy == null)
        {
            Debug.LogError("ERROR: there is no enemy to battle");
            return;
        }

        Debug.Log("Player Attack");

        int damageDealt = damage - enemy.armor;
        if (damageDealt > 0)
        {
            // Play a damage sound
        }
        else
        {
            // Play a block sound
        }
        Debug.Log("Player deals " + damageDealt + " damage.");
        enemy.health -= damageDealt;
    }



    public void AddItemsToInventory(List<Item> droppedItems)
    {
        inventory.AddItems(droppedItems);
    }

    public int GetHealth()
    {
        return health;
    }

    public void IncrementArmor(int addArmor)
    {
        armor += addArmor;
    }

    public void DecrementArmor(int decArmor)
    {
        armor += decArmor;
    }

    public void IncrementHealth(int recovery)
    {
        Mathf.Clamp((health += recovery), 0, maxHealth);
    }

    public void DecrementHealth(int damage)
    {
        Mathf.Clamp((health -= damage), 0, maxHealth);     
    }
}
