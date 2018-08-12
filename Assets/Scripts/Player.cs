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
    public int elementalDamage;
    public int armor;
    private int weight;
    private Inventory inventory;
    private DialogManager dialogManager;
    public bool isPoisoned = false;
    public bool isEncumbered = false;

    private void Awake()
    {
        health = maxHealth;
        Debug.Log("Player health: " + health);
    }
    public void Start()
    {
        dialogManager = FindObjectOfType<DialogManager>();
    }

    // Attack the enemy that is set in the combat encounter in gamecontroller
    public void Attack(Enemy enemy)
    {
        if (weight > 100)
        {
            isEncumbered = true;
        }
        if (enemy == null)
        {
            Debug.LogError("ERROR: there is no enemy to battle");
            return;
        }
        int damageDealt;
        if (isEncumbered)
        {
            damageDealt = Mathf.Clamp(damage + elementalDamage - enemy.armor - 5, 0, 99999);
        }
        else
        if (enemy.name == "Ghost")
        {
            damageDealt = Mathf.Clamp(elementalDamage - enemy.armor, 0, 99999);
        }
        else
        if (enemy.name == "Dragon")
        {
            damageDealt = Mathf.Clamp(damage + elementalDamage * 2 - enemy.armor, 0, 99999);
        }
        else
            damageDealt = Mathf.Clamp(damage + elementalDamage - enemy.armor, 0, 99999);
        if (damageDealt > 0)
        {
            // Play a damage sound
        }
        else
        {
            // Play a block sound
        }
        enemy.health -= damageDealt;
        if (enemy.name == "Sand Golem" && damageDealt > 0)
        {
            string[] sentences =
            {
                "Player's turn",
                "Player deals " + damageDealt + " damage.",
                CheckEnemyHealth(enemy.health, enemy),
                "Obtain an iron ore!"
            };
            Dialog playerTurn = new Dialog("enemy turn", sentences);
            Inventory.instance.AddItem(enemy.itemDropList[0]);
            dialogManager.isInDialog = true;
            dialogManager.StartDialog(playerTurn);
        }
        else
        {
            string[] sentences =
            {
                "Player's turn",
                "Player deals " + damageDealt + " damage.",
                CheckEnemyHealth(enemy.health, enemy)
            };
            Dialog playerTurn = new Dialog("enemy turn", sentences);
            dialogManager.isInDialog = true;
            dialogManager.StartDialog(playerTurn);
        }
    }
    public string CheckEnemyHealth(int enemyHealth, Enemy enemy)
    {
        if (enemyHealth <= 0)
            return enemy.name + " is defeated.";
        return enemy.name + " has " + enemy.health + " health left.";
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

    public void IncrementElemental(int addElemental)
    {
        elementalDamage += addElemental;
    }

    public void DecrementArmor(int decArmor)
    {
        armor += decArmor;
    }

    public void IncrementHealth(int recovery)
    {
        health = Mathf.Clamp((health + recovery), 0, maxHealth);
    }

    public int DecrementHealth(int damage)
    {
        int totalDamage = Mathf.Clamp((damage - armor), 0, 99999);
        health = Mathf.Clamp((health - damage - armor), 0, maxHealth);
        if (isPoisoned)
        {
            health = Mathf.Clamp((health - 2), 0, maxHealth);
            return totalDamage + 2;
        }
        return totalDamage;
    }

    public int GetWeight()
    {
        int totalWeight = 0;
        foreach (Item item in Inventory.instance.itemList)
        {
            if (item != null)
            {
                totalWeight += item.weight;
            }
        }
        return totalWeight;
    }
}