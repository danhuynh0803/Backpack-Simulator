using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public abstract class Item : MonoBehaviour {
    
    public string name;
    public string description;
    public int weight;
    public Sprite icon;

    public void WriteDescription()
    {
        
    }

    // Provides some form of effect
    public abstract void ActivateEffect();
        
}
