using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public abstract class Item : MonoBehaviour {
    
    public string name;
    public string description;
    public int weight;
    public Sprite icon;
    public int cost;
    // Provides some form of effect
    // return true if its a consumable and will disappear upon use
    // else return false to not have it disappear from list upon use
    public abstract bool ActivateEffect();
        
}
