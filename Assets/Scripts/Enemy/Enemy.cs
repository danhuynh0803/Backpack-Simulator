using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public abstract class Enemy : MonoBehaviour{

    [Header("Enemy Menu related")]
    public Sprite mugshot;
    public string name;
    public string description;
    public AudioClip menuSound;
    public MapTileType mapTileType;

    [Header("Battle Settings")]
    public int maxHealth;
    public int health;
    public int damage;
    public int armor;
    [Header("Drop Settings")]
    public List<Item> itemDropList = new List<Item>();

    public DialogManager dialogManager;

    public abstract void Attack();

    private void Start()
    {
        health = maxHealth;
    }

    public abstract void Death();
}
