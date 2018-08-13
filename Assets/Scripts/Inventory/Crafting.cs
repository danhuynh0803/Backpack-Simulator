﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class Crafting : MonoBehaviour {

    public Inventory inventory;
    public Recipe testRecipe;
    private string[] componentNames;
    private int[] slotIndices;

    public void Craft(Recipe recipe)    
    {        
        if (inventory == null)
        {
            inventory = FindObjectOfType<Inventory>();
        }
        slotIndices = new int[recipe.components.Count];
        componentNames = recipe.components.ToArray();
        // Check that player has the prerequisite items for the recipe
        if (HasAllComponents())
        {
            Debug.Log("Has all componenets");
            // Remove the items at the slots 
            // Make sure to sort first by lowest since each item will have a new slot index 
            // that will be one less as we shift items left
            Array.Sort(slotIndices);
            inventory.RemoveItem(slotIndices[0]);            
            for (int i = 1; i < slotIndices.Length; ++i)
            {
                // Shift the second item onward by one to take into account the shifting 
                // of slot indices when calling remove item
                inventory.RemoveItem(slotIndices[i]-1);
            }

            // Add the newly crafted item into inventory
            inventory.AddItem(recipe.recipeItem);
        }        
    }

    public bool HasAllComponents()
    {
        bool[] hasAllComponents = new bool[slotIndices.Length];

        for (int i = 0; i < componentNames.Length; ++i)
        {
            string componentName = componentNames[i];
            componentName = Regex.Replace(componentName, @"\s+", "");
            //Debug.Log(componentName);
            for (int j = 0; j < inventory.itemList.Count; ++j)
            {                
                string itemName = inventory.itemList[j].name;
                itemName = Regex.Replace(itemName, @"\s+", "");
                if (itemName.ToLower().Equals(componentName.ToLower()))
                {
                    slotIndices[i] = j;
                    hasAllComponents[i] = true;
                }
            }            
        }

        bool canCraft = true;
        foreach (bool hasComponent in hasAllComponents)
        {
            if (!hasComponent)
                canCraft = false;
        }

        return canCraft;
    }
}