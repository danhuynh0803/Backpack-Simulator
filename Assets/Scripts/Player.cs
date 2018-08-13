using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Status
{
    Encumbered,
    Poison,
    Frost,
    ElementalDamage
};

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

    public int preBattleDamage;
    int damage;

    public int elementalDamage;

    public int preBattleArmor;
    private int armor;

    private int weight;
    private Inventory inventory;
    private DialogManager dialogManager;
    public bool isPoisoned = false;
    public bool isEncumbered = false;
    public bool isDemoralized = false;
  
    // Four status effects: 
    // 1) encumbered, 2) poison, 3) frost, 4) elemental damage
    private int[] statusEffects = new int[4];

    public void AddStatusEffect(Status status, int statusAmount)
    {
        statusEffects[(int)status] += statusAmount;
    }

    public void RemoveStatusEffect(Status status, int statusAmount)
    {
        statusEffects[(int)status] -= statusAmount;
        // Set to zero to avoid going negative (resulting in a buff)
        if (statusEffects[(int)status] <= 0)
        {
            statusEffects[(int)status] = 0;
        }
    }

    public void SetStatusEffect(Status status, int statusAmount)
    {
        statusEffects[(int)status] = statusAmount;
    }

    public void RemoveStatusEffect(Status status)
    {
        statusEffects[(int)status] = 0;
    }

    public int GetStatusEffect(Status status)
    {
        return statusEffects[(int)status];
    }

    private void Awake()
    {
        health = maxHealth;
        weight = 0;
        //Debug.Log("Player health: " + health);
    }
    public void Start()
    {
        dialogManager = FindObjectOfType<DialogManager>();
        ApplyStatusEffects();
    }
  
    public void ApplyStatusEffects()
    {
        SetStatusEffect(Status.Encumbered, ((GetWeight() - 50) / 10));
        armor = preBattleArmor - GetStatusEffect(Status.Frost);
        damage = preBattleDamage - GetStatusEffect(Status.Encumbered);
        elementalDamage = GetStatusEffect(Status.ElementalDamage);
    }

    public void ApplyHealthDebuffs()
    {
        DecrementHealth(GetStatusEffect(Status.Poison));
    }

    // Attack the enemy that is set in the combat encounter in gamecontroller
    public void Attack(Enemy enemy)
    {
        // Set encumbered
        //SetStatusEffect(Status.Encumbered, ((GetWeight() - 50) % 10));
        ApplyStatusEffects();
         
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
            SoundController.Play((int)SFX.Attack, 0.5f);
        }
        enemy.health = Mathf.Clamp(enemy.health - damageDealt, 0, enemy.maxHealth);

        if (enemy.name == "Sand Golem" && damageDealt > 0)
        {
            string[] sentences =
            {
                "Player's turn",
                "Player deals " + damageDealt + " damage.",
                CheckEnemyHealth(enemy.health, enemy),
                "Obtain an iron ore!"
            };
            Inventory.instance.AddItem(enemy.itemDropList[0]);
            PrintNextSentence(sentences);
        }
        else
        if (enemy.name == "Bandit" && damageDealt > 0)
        {
            string[] sentences =
            {
                "Player's turn",
                "Player deals " + damageDealt + " damage.",
                CheckEnemyHealth(enemy.health, enemy),
                "Obtain an gold ore!"
            };
            Inventory.instance.AddItem(enemy.itemDropList[0]);
            PrintNextSentence(sentences);
        }
        else
        {
            string[] sentences =
            {
                "Player's turn",
                "Player deals " + damageDealt + " damage.",
                CheckEnemyHealth(enemy.health, enemy)
            };
            PrintNextSentence(sentences);
        }

        ApplyHealthDebuffs();
    }

    public void PrintNextSentence(string[] sentences)
    {
        Dialog playerTurn = new Dialog("enemy turn", sentences);
        dialogManager.isInDialog = true;
        dialogManager.StartDialog(playerTurn);
        dialogManager.ContinueToNextSentence();
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

    public int GetDamage()
    {
        return damage;
    }

    public void IncrementArmor(int addArmor)
    {
        armor += addArmor;
    }

    public void IncrementElemental(int addElemental)
    {
        elementalDamage += addElemental;
    }

    public int GetArmor()
    {
        return armor;
    }

    public void DecrementArmor(int decArmor)
    {
        armor -= decArmor;
    }

    public void IncrementHealth(int recovery)
    {
        health = Mathf.Clamp((health + recovery), 0, maxHealth);
    }

    public int DecrementHealth(int damage)
    {
        //making sure armor calculation is included when this method is called
        int totalDamage = Mathf.Clamp(damage, 0, 99999);
        if(totalDamage == 0)
            SoundController.Play((int)SFX.Defend, 0.5f);
        health = Mathf.Clamp((health - damage), 0, maxHealth);
        if (isPoisoned)
        {
            health = Mathf.Clamp((health - 2), 0, maxHealth);
            return totalDamage + 2;
        }
        return totalDamage;
    }

    public void AddPermanentWeight(int itemWeight)
    {
        weight += itemWeight;
    }

    public int GetWeight()
    {
        int bagWeight = 0;
        Inventory inventory = FindObjectOfType<Inventory>();
        if (inventory != null)
        {
            foreach (Item item in inventory.itemList)
            {
                if (item != null)
                {
                    bagWeight += item.weight;
                }
            }
        }
        return bagWeight + weight;
    }
}