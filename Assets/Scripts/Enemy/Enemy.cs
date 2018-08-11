using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour{

    public string name;
    public int health;
    public int damage;
    public int armor;
    public List<Item> itemDropList = new List<Item>();

    public abstract void Attack();

    public abstract void Death();
}
