using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class MapTile
{
    public float yOffest;
    public Sprite sprite;
}

public class RaymondRaodMapController : MonoBehaviour
{
    public List<string> test;
    //public int branches;
    public int levels;
    public int pathTwistingLevel;
    //public float branchAngleOffset;
    //public float levelAngleOffset;
    //public float roadMapGapX;
    //public float roadMapGapY;
    public Vector2[] hexagonBasePosisiton;
    //public Vector2[] hexagonBasePosisitonRandomize;
    public EnemyButtonEncounters root;
    public GameObject[] encounterTypes;
    public GameObject rootEncounterType;
    public GameObject nodePrefab;
    public Transform encounterButtonParnent;
    public GameObject enemyEncounterMenu;
    public Transform enemyObjectParent;

    [SerializeField]
    public MapTile[] mapTile;

    void Start()
    {
        pathTwistingLevel = Math.Min(pathTwistingLevel, levels);
        levels = Math.Max(pathTwistingLevel, levels);
        //int enemeyType = Random.Range(0, encounterTypes.Length);
        GameObject rootEncounter = Instantiate(rootEncounterType, enemyObjectParent);
        root.GetComponent<EnemyButtonEncounters>().enemyObject = rootEncounter;
        int rootMapTileID = GetMapTileIDAndSetSprite(root.gameObject, rootEncounter);
        root.transform.localPosition += new Vector3(0f, mapTile[rootMapTileID].yOffest,0f);
        root.GetComponent<EnemyButtonEncounters>().enemy = rootEncounter.GetComponent<Enemy>();
        if (root != null)
        {
            for (int i = 0; i < 6; i++)
            {
                int firstEnemyType = UnityEngine.Random.Range(0, encounterTypes.Length);
                GameObject enemyObject = Instantiate(encounterTypes[firstEnemyType], enemyObjectParent);
                enemyObject.GetComponent<SpriteRenderer>().enabled = false;
                GameObject firstEncounter = Instantiate(nodePrefab, enemyObjectParent);
                int mapTileID = GetMapTileIDAndSetSprite(firstEncounter, enemyObject);
                firstEncounter.GetComponent<Transform>().localPosition = new Vector3(hexagonBasePosisiton[i].x, hexagonBasePosisiton[i].y + mapTile[mapTileID].yOffest, hexagonBasePosisiton[i].y + mapTile[mapTileID].yOffest);
                firstEncounter.GetComponent<EnemyButtonEncounters>().enemyEncounterMenu = enemyEncounterMenu;
                firstEncounter.GetComponent<EnemyButtonEncounters>().enemyObject = enemyObject;
                root.AddNextEncounter(firstEncounter.GetComponent<EnemyButtonEncounters>());
                GameObject newRoot = firstEncounter;
                for (int j = 0; j < levels; j++)
                {
                    int nextEnemeyType = UnityEngine.Random.Range(0, encounterTypes.Length);
                    GameObject nextEnemyObject = Instantiate(encounterTypes[nextEnemeyType], enemyObjectParent);
                    nextEnemyObject.GetComponent<SpriteRenderer>().enabled = false;
                    GameObject nextEncounter = Instantiate(nodePrefab, enemyObjectParent);
                    int nextmMapTileID = GetMapTileIDAndSetSprite(nextEncounter, nextEnemyObject);
                    Vector3 offset = new Vector3(newRoot.GetComponent<Transform>().localPosition.x, newRoot.GetComponent<Transform>().localPosition.y, newRoot.GetComponent<Transform>().localPosition.z);
                    if(j < pathTwistingLevel)
                    {
                        nextEncounter.GetComponent<Transform>().localPosition = offset + new Vector3(hexagonBasePosisiton[i].x, hexagonBasePosisiton[i].y + mapTile[nextmMapTileID].yOffest, hexagonBasePosisiton[i].y + mapTile[nextmMapTileID].yOffest);
                    }
                    else
                    {
                        int k = GetRandomDirection(i);
                        nextEncounter.GetComponent<Transform>().localPosition = offset + new Vector3(hexagonBasePosisiton[k].x, hexagonBasePosisiton[k].y + mapTile[nextmMapTileID].yOffest, hexagonBasePosisiton[k].y + mapTile[nextmMapTileID].yOffest);
                    }
                    nextEncounter.GetComponent<EnemyButtonEncounters>().enemyObject = nextEnemyObject;
                    nextEncounter.GetComponent<EnemyButtonEncounters>().enemyEncounterMenu = enemyEncounterMenu;
                    newRoot.GetComponent<EnemyButtonEncounters>().AddNextEncounter(nextEncounter.GetComponent<EnemyButtonEncounters>());
                    newRoot = nextEncounter;
                }
            }
        }
    }

    int GetRandomDirection(int i)
    {
        float random = UnityEngine.Random.Range(0f, 1f);
        if(random > 0.66f)
        {
            return (i + 1 + hexagonBasePosisiton.Length) % hexagonBasePosisiton.Length;
        }
        else
        if(random > 0.33f)
        {
            return (i + hexagonBasePosisiton.Length) % hexagonBasePosisiton.Length;
        }
        else
        {
            return (i - 1 + hexagonBasePosisiton.Length) % hexagonBasePosisiton.Length;
        }

    }
    int GetMapTileIDAndSetSprite(GameObject encounter, GameObject enemy)
    {
        int mapTileID = 0;
        switch(enemy.GetComponent<Enemy>().mapTileType)
        {
            case (MapTileType.Bandit):
                mapTileID = (int)MapTileType.Bandit;
                break;
            case (MapTileType.Root):
                mapTileID = (int)MapTileType.Root;
                break;
            case (MapTileType.Siren):
                mapTileID = (int)MapTileType.Siren;
                break;
            case (MapTileType.SmileyPerson):
                mapTileID = (int)MapTileType.SmileyPerson;
                break;
            case (MapTileType.Spider):
                mapTileID = (int)MapTileType.Spider;
                break;
            default:
                break;
        }
        encounter.GetComponent<SpriteRenderer>().sprite = mapTile[mapTileID].sprite;
        mapTile[mapTileID].yOffest = (mapTile[mapTileID].sprite.bounds.size.y - 2.56f) / 2f;
        encounter.AddComponent<PolygonCollider2D>();
        return mapTileID;
    }
}
