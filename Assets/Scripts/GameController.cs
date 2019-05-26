using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controls the combat and other mechanics
public class GameController : MonoBehaviour
{
    #region Singleton
    public static GameController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    #endregion

    private Player player;
    public GameObject enemyObject; // make public for testing. 
    private Enemy enemy;
                        //Later use a function to set from button
    private bool isGameOver;
    private float turnDelay = 1f; // Seconds before moving to next turn;
    public MenuController menuController;
    private DialogManager dialogManager;
    [Header("Battle canvases")]
    public GameObject battleCanvas;
    public GameObject GameOverCanvas;
    public GameObject inventoryPanel;
    public EnemyButtonEncounters currentEncounter;
    public bool isInCombat;

    private void Start()
    {
        enemy = null;
        isInCombat = false;
        isGameOver = false;
        //player = Player.player;
        player = FindObjectOfType<Player>();
        dialogManager = FindObjectOfType<DialogManager>();
        if (player == null)
        {
            Debug.LogError("ERROR: No player instance was found");
        }

        //SetupCombat(enemyObject);
    }

    public Enemy GetEnemyForUI()
    {
        return enemy;
    }

    public void SetupCombat(GameObject enemyObject, EnemyButtonEncounters enemyButtonEncounters)
    {
        currentEncounter = enemyButtonEncounters;
           // Make a new reference to the Enemy script to avoid overwriting the values
        enemyObject.GetComponent<Enemy>().dialogManager = FindObjectOfType<DialogManager>();

        EnterCombat(enemyObject.GetComponent<Enemy>());
    }

    public void EnterCombat(Enemy newEnemy)
    {
        // Set the current enemy that player is battling
        enemy = newEnemy;

        if (enemy != null)
        {     
            // Display any entering dialog we feel we need
            // "Player is battling a <enemy_name>"
            menuController.ToggleBattleCanvas();
            menuController.inventorReturnButton.SetActive(false);
            // Find treaure
            if (enemy.health <= 0)
            {
                FindTreasure();
                return;
            }
            isInCombat = true;
            PrintStartComatText();
        }
        else
        {
            Debug.LogError("No enemy was set for encounter!");
        }       
    }


    // Controls combat loop
    IEnumerator CombatLoop()
    {
        if (enemy == null)
        {
            Debug.LogError("ERROR: Entered combat without an enemy set");
        }
  
        int turnCount = 0;

        while (enemy.health > 0 && player.GetHealth() > 0 )
        {
            //StartDialog()
            // Output to dialog the turn
            if (turnCount % 2 == 0)
            {
                // Players turn                
                player.Attack(enemy);
            }
            else
            {
                // Enemy's turn
                enemy.Attack();
            }
            yield return new WaitUntil(() => dialogManager.isInDialog == false);
            turnCount++;
        }

        EndCombat();
    }
    
    IEnumerator Wait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
    }
    IEnumerator WaitUntilDialogIsOver(string type)
    {
        yield return new WaitUntil(() => dialogManager.isInDialog == false);
        switch(type)
        {
            case "start combat":
                StartCoroutine(CombatLoop());
                break;
            case "end combat":
                AddDropItemToPlayer();
                break;
            case "leave combat":
                menuController.inventorReturnButton.SetActive(true);
                menuController.ToggleBattleCanvas();
                break;
            case "find treasure":
                AddDropItemToPlayer();
                break;
            default:
                break;
        }
    }

    void PrintStartComatText()
    {
        string[] sentences =
        {
            "========Start Combat========",
            enemy.name + " health = " + enemy.health + "\tplayer health = " + player.GetHealth(),
            "Good luck!"
        };
        Dialog startCombatDialog = new Dialog("start combat", sentences);
        dialogManager.StartDialog(startCombatDialog);
        dialogManager.isInDialog = true;
        dialogManager.ContinueToNextSentence();
        StartCoroutine(WaitUntilDialogIsOver("start combat"));
    }

    void PrintEndCombatText()
    {
        string dropList = "";
        foreach(Item item in enemy.itemDropList)
        {
            dropList += "Obtain 金" + item.name + "右 x1"+ "\n";
        }
        string[] sentences =
        {
            "=========End Combat=========",
            dropList,
            "\n" + "Please circle your next destination on the map."
        };
        Dialog endCombatDialog = new Dialog("end combat", sentences);
        dialogManager.StartDialog(endCombatDialog);
        dialogManager.isInDialog = true;
        dialogManager.ContinueToNextSentence();
        StartCoroutine(WaitUntilDialogIsOver("end combat"));
    }

    void PrintDeathCombatText()
    {
        string[] sentences =
        {
            "=========Player Death=========",
        };
        Dialog deathCombatDialog = new Dialog("end combat", sentences);
        dialogManager.StartDialog(deathCombatDialog);
        dialogManager.isInDialog = true;
    }

    void EnableNextEncounter()
    {
        if(currentEncounter != null)
        {
            currentEncounter.GetComponent<EnemyButtonEncounters>().EnableNextEncounters();
        }
    }

    void AddDropItemToPlayer()
    {
        EnableNextEncounter();
        //inventoryPanel.SetActive(true);
        FindObjectOfType<InventoryUI>().inventoryScrollBar.value = 0;
        Inventory.instance.AddItems(enemy.itemDropList);
        FindObjectOfType<InventoryUI>().inventoryScrollBar.value = 0;
        enemy = null;
        Dialog leaveCombatDialog = new Dialog("leave combat", new string[] {""});
        dialogManager.StartDialog(leaveCombatDialog);
        dialogManager.isInDialog = true;
        StartCoroutine(WaitUntilDialogIsOver("leave combat"));
    }

    public void FindTreasure()
    {
        string dropList = "";
        foreach (Item item in enemy.itemDropList)
        {
            dropList += "Obtain 金" + item.name + "右 x1" + "\n";
        }
        string[] sentences =
        {
            "==========Found Treasure=========",
            "Oh wow... more treasure....... or just more junk.",
            dropList
        };
        Dialog findTreasureDialog = new Dialog("find treasure", sentences);
        dialogManager.StartDialog(findTreasureDialog);
        dialogManager.isInDialog = true;
        dialogManager.ContinueToNextSentence();
        StartCoroutine(WaitUntilDialogIsOver("find treasure"));
    }

    public void EndCombat()
    {
        if (enemy.health <= 0)
        {
            enemy.Death();
            PrintEndCombatText();
            isInCombat = false;
            // reset so that the enemy status panel disappears after combat  
        }
        else // player health reached zero so end game
        {
            GameOver();
        }
        isInCombat = false;     
    }

    private void GameOver()
    {
        PrintDeathCombatText();
        GameOverCanvas.SetActive(true);
    }

    public void Restart()
    {
        menuController.LoadScene("DannyTestCrafting");
    }
}
