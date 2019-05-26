using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyButtonEncounters : MonoBehaviour {

    [Header("EnemyPanel Settings")]        
    public GameObject enemyObject;
    public GameObject enemyEncounterMenu;

    private GameObject encounterButton;
    private GameController gameController;
    private Image menuPicture;
    private Text menuDescription;
    private Text menuName, menuHealth, menuAttack, menuArmor;
    public Enemy enemy;
    private AudioSource menuSound;
    private Color originalColor;
    public EnemyButtonEncounters currentEncounter;
    public List<EnemyButtonEncounters> nextEncounters;
    public bool IsInteractable;
    public void Awake()
    {
        nextEncounters = new List<EnemyButtonEncounters>();
    }

    public void Start()
    {
        if(GetComponent<SpriteRenderer>() != null)
        {
            originalColor = GetComponent<SpriteRenderer>().color;
        }
        encounterButton = this.gameObject;                
        if(enemyObject!= null)
            enemy = enemyObject.GetComponent<Enemy>();
        if (enemy == null)
        {
            Debug.Log("Attached gameobject is not an enemy!");
        }
    }

    private void SetupPrivateFields()
    {        
        menuPicture = GameObject.Find("MenuPicture").GetComponent<Image>();
        menuDescription = GameObject.Find("MenuDescription").GetComponent<Text>();
        menuName = GameObject.Find("MenuName").GetComponent<Text>();
        menuHealth = GameObject.Find("MenuHealth").GetComponent<Text>();
        menuAttack = GameObject.Find("MenuAttack").GetComponent<Text>();
        menuArmor = GameObject.Find("MenuArmor").GetComponent<Text>();
        menuSound = GameObject.Find("EnemyCounterMenu").GetComponent<AudioSource>();
    }

    public void LoadEnemyDescriptionPanel()
    {
        enemyEncounterMenu.SetActive(true);
        SetupPrivateFields();
        enemyEncounterMenu.GetComponent<EnemyButtonEncounters>().enemyObject = enemyObject;
        enemyEncounterMenu.GetComponent<EnemyButtonEncounters>().currentEncounter = this;
        menuPicture.sprite = enemy.mugshot;
        menuDescription.text = enemy.description;
        menuName.text = enemy.name;
        menuHealth.text = "Health: " + enemy.maxHealth;
        menuAttack.text = "Attack: " + enemy.damage;
        menuArmor.text = "Armor: " + enemy.armor;
        menuSound.clip = enemy.menuSound;
        menuSound.Play();
    }

    public void ExitEnemyDescriptionPanel()
    {
        enemyEncounterMenu.SetActive(false);
    }

    public void StartEncounter()
    {
        gameController = FindObjectOfType<GameController>(); 
        if (enemyObject != null)
        {
            enemyEncounterMenu.SetActive(false);
            gameController.SetupCombat(enemyObject, currentEncounter);
        }
        else
        {
            Debug.LogError("No enemy object attached to this encounter");
        }
    }

    public void EnableNextEncounters()
    {
        IsInteractable = false;
        GetComponent<SpriteRenderer>().color = new Color(0.6f, 0.6f, 0.6f);
        Destroy(enemyObject);
        foreach (EnemyButtonEncounters buttons in nextEncounters)
        {
            buttons.IsInteractable = true;
        }
    }

    public void AddNextEncounter(EnemyButtonEncounters enemyButtonEncounters)
    {
        nextEncounters.Add(enemyButtonEncounters);
    }

    public void OnMouseDown()
    {
        if(IsInteractable)
        {
            LoadEnemyDescriptionPanel();
        }
    }

    public void OnMouseOver()
    {

        if (IsInteractable)
        {
            GetComponent<SpriteRenderer>().color = new Color(0.6f, 0.6f, 0.6f);
        }
        //float H, S, V;
        //Color.RGBToHSV(originalColor, out H, out S, out V);
        //GetComponent<SpriteRenderer>().color = Color.HSVToRGB(H, S, 0.7f * V);
    }

    public void OnMouseExit()
    {

        if (IsInteractable)
        {
            GetComponent<SpriteRenderer>().color = originalColor;
        }
    }
}
