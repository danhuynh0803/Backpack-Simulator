using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstStep : MonoBehaviour
{

    [Header("Enemies to spawn")]
    public GameObject[] enemies;

    GameObject spawnedObject;
    int randEnemy;
    public GameObject spawnPoint;
    public GameObject pathOne;
    public GameObject pathTwo;

    // Make sure this matches what is in the inspector
    enum Encounters
    {
        One,
        Two,
        Three
    };

    void Start()
    {
        SpawnEnemyRate();
        SpawnButton();
    }

   void SpawnButton()
        {
            spawnedObject =
                Instantiate(enemies[randEnemy], spawnPoint.transform, spawnPoint.transform);
        spawnedObject.GetComponent<Transform>().transform.position = spawnPoint.transform.position;
        spawnedObject.GetComponent<Route>().isPathGood = false;
    }

    private void Update()
    {
        if(spawnedObject.GetComponent<Route>().isPathGood == false)
        {
            pathOne.GetComponent<ViablePath>().isPathGood = false;
            pathTwo.GetComponent<ViablePath>().isPathGood = false;
        }
        if (spawnedObject.GetComponent<Route>().isPathGood == true)
        {
            pathOne.GetComponent<ViablePath>().isPathGood = true;
            pathTwo.GetComponent<ViablePath>().isPathGood = true;
        }
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
}