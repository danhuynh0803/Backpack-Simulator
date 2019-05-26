using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnePath : MonoBehaviour
{
    [Header("Enemies to spawn")]
    public GameObject[] enemies;

    private GameObject thisButton;
    int randEnemy;
    public GameObject spawnPoint;
    public GameObject oneBehind;
    public GameObject oneAhead;
    public GameObject twoAhead;


    // Make sure this matches what is in the inspector
    enum Encounters
    {
        One,
        Two,
        Three
    };

    void Start()
    {
        SpawnButton();
    }

    void SpawnButton()
    {
        SpawnEnemyRate();
        thisButton =
            Instantiate(enemies[randEnemy], spawnPoint.transform, spawnPoint.transform) as GameObject;
        thisButton.GetComponent<Transform>().transform.position = spawnPoint.transform.position;
        thisButton.GetComponent<Route>().isPathGood = false;
    }

    void SpawnEnemyRate()
    {
        float rate = Random.Range(0.0f, 100.0f);

        // TODO: clean up and streamline this so that we dont need to keep updating these values

        if (rate > 66.0f) //9%
        {
            randEnemy = (int)Encounters.One;
        }
        else if (rate > 33.0f) //9%
        {
            randEnemy = (int)Encounters.Two;
        }
        else
        {
            randEnemy = (int)Encounters.Three;
        }
    }

    private void Update()
    {
        if (oneBehind.GetComponent<ViablePath>().isPathGood == false)
        {
            BadBehind();
        }
        if (oneBehind.GetComponent<ViablePath>().isPathGood == true)
        {
            GoodBehind();
        }
        if (thisButton.GetComponent<Route>().isPathGood == false)
        {
            BadAhead();
        }
        if (thisButton.GetComponent<Route>().isPathGood == true)
        {
            GoodAhead();
        }
    }

    public void BadBehind()
    {
        thisButton.GetComponent<Button>().interactable = false;
        thisButton.GetComponent<Button>().interactable = false;
    }
    public void GoodBehind()
    {
        thisButton.GetComponent<Button>().interactable = true;
        thisButton.GetComponent<Button>().interactable = true;
    }
    public void BadAhead()
    {
        oneAhead.GetComponent<ViablePath>().isPathGood = false;
        twoAhead.GetComponent<ViablePath>().isPathGood = false;
    }
    public void GoodAhead()
    {
        oneAhead.GetComponent<ViablePath>().isPathGood = true;
        twoAhead.GetComponent<ViablePath>().isPathGood = true;
    }
}