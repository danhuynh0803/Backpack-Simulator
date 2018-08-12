using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour{

    public string name;
    public int maxHealth;
    public int health;
    public int damage;
    public int armor;
    public List<Item> itemDropList = new List<Item>();
    public DialogManager dialogManager;

    public abstract void Attack();

    private void Start()
    {
        health = maxHealth;
    }

    public abstract void Death();
}
