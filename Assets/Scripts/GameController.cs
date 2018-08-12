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
    public float turnDelay; // Seconds before moving to next turn;
    private MenuController menuController;
    private DialogManager dialogManager;
    [Header("Battle canvases")]
    public GameObject battleCanvas;

    public bool isInCombat;

    private void Start()
    {
        isInCombat = false;
        isGameOver = false;
        //player = Player.player;
        player = FindObjectOfType<Player>();
        menuController = FindObjectOfType<MenuController>();
        dialogManager = FindObjectOfType<DialogManager>();
        if (player == null)
        {
            Debug.LogError("ERROR: No player instance was found");
        }

        //SetupCombat(enemyObject);
    }

    public void SetupCombat(GameObject enemyObject)
    {
        // Make a new reference to the Enemy script to avoid overwriting the values
        GameObject newEnemyReference = Instantiate(enemyObject);
        newEnemyReference.GetComponent<Enemy>().dialogManager = FindObjectOfType<DialogManager>();
        EnterCombat(newEnemyReference.GetComponent<Enemy>());
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
            default:
                break;
        }
    }

    void PrintStartComatText()
    {
        string[] sentences =
        {
            "========Start Combat========",
            enemy.name + " health = " + enemy.health + "\tplayer health =" + player.GetHealth()
        };
        Dialog startCombatDialog = new Dialog("start combat", sentences);
        dialogManager.StartDialog(startCombatDialog);
        dialogManager.isInDialog = true;
        dialogManager.ContinueToNextSentence();
        StartCoroutine(WaitUntilDialogIsOver("start combat"));
    }

    void PrintEndCombatText()
    {
        string[] sentences =
        {
            "========End Combat========",
            "\n" + "Please circle your next destination on the map."
        };
        Dialog endCombatDialog = new Dialog("end combat", sentences);
        dialogManager.StartDialog(endCombatDialog);
        dialogManager.isInDialog = true;
        StartCoroutine(WaitUntilDialogIsOver("end combat"));
    }

    void PrintDeathCombatText()
    {
        string[] sentences =
        {
            "=====Player Death=====",
        };
        Dialog deathCombatDialog = new Dialog("end combat", sentences);
        dialogManager.StartDialog(deathCombatDialog);
        dialogManager.isInDialog = true;
    }

    void AddDropItemToPlayer()
    {
        Inventory.instance.AddItems(enemy.itemDropList);
    }

    public void EndCombat()
    {
        if (enemy.health <= 0)
        {
            enemy.Death();
            PrintEndCombatText();
            isInCombat = false;
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
    }

}
