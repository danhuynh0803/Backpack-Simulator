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
    public Enemy enemy; // make public for testing. 
                        //Later use a function to set from button
    private bool isGameOver;
    public float turnDelay; // Seconds before moving to next turn;

    [Header("Battle canvases")]
    public GameObject battleCanvas;

    private void Start()
    {
        isGameOver = false;
        //player = Player.player;
        player = FindObjectOfType<Player>();
        if (player == null)
        {
            Debug.LogError("ERROR: No player instance was found");
        }

        // testing combat
        //EnterCombat(enemy);
    }

    public void EnterCombat(Enemy newEnemy)
    {
        // Set the current enemy that player is battling
        enemy = newEnemy;

        if (enemy != null)
        {
            // Display any entering dialog we feel we need
            // "Player is battling a <enemy_name>"

            battleCanvas.SetActive(true);
            CombatLoop();
        }
        else
        {
            Debug.LogError("No enemy was set for encounter!");
        }       
    }

    // Controls combat loop
    public void CombatLoop()
    {
        if (enemy == null)
        {
            Debug.LogError("ERROR: Entered combat without an enemy set");
        }
  
        int turnCount = 0;

        Debug.Log("Enemy health=" + enemy.health + "\tplayer health=" + player.GetHealth());
        while (enemy.health > 0 && player.GetHealth() > 0)
        {          
            // Output to dialog the turn
            if (turnCount % 2 == 0)
            {
                // Players turn                
                Debug.Log("Player's turn");
                player.Attack();       
            }
            else
            {
                // Enemy's turn
                Debug.Log("Enemy's turn");
                enemy.Attack();
            }

            turnCount++;
            //StartCoroutine(Wait(turnDelay));
        }

        EndCombat();
    }

    IEnumerator Wait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
    }

    public void EndCombat()
    {
        if (enemy.health <= 0)
        {
            enemy.Death();

            Debug.Log("Player now has " + player.GetHealth() + " health remaining.");

            Inventory.instance.AddItems(enemy.itemDropList);
        }
        else // player health reached zero so end game
        {
            GameOver();
        }        
    }

    private void GameOver()
    {

    }

}
