using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class Crafting : MonoBehaviour {

    public List<GameObject> craftingCanvases = new List<GameObject>();
    private Inventory inventory;
    public List<Recipe> recipes = new List<Recipe>();
    private string[] componentNames;
    private int[] slotIndices;
    private bool isCrafting;

    private void Start()
    {
        CraftingUI craftingUI = FindObjectOfType<CraftingUI>();
        inventory = FindObjectOfType<Inventory>();
        if (craftingUI != null)
        {
            foreach (Recipe recipe in recipes)
            {
                if (recipes != null)
                {
                    craftingUI.AddItemToUI(recipe);
                }
            }
        }
    }

    public void ToggleCraftingCanvases()
    {
        isCrafting = !isCrafting;
        if (isCrafting)
        {
            //inventoryBlockRayCast.SetActive(false);
            foreach (GameObject canvas in craftingCanvases)
            {
                if (canvas != null)
                    canvas.SetActive(true);
            }
        }
        else
        {
            //inventoryBlockRayCast.SetActive(true);
            foreach (GameObject canvas in craftingCanvases)
            {
                if (canvas != null)
                    canvas.SetActive(false);
            }
        }
    }

    public bool HasAllComponents(Recipe recipe)
    {
        Dictionary<Item, int> componentDict = new Dictionary<Item, int>();
        for (int i = 0; i < recipe.componentItems.Count; ++i)
        {
            Item cItem = recipe.componentItems[i];
            if (componentDict.ContainsKey(cItem))
            {
                componentDict[cItem]++;
            }
            else
            {
                componentDict.Add(cItem, 1);
            }

        }

        foreach (KeyValuePair<Item, int> entry in componentDict)
        {
            if (!Inventory.instance.inventory.ContainsKey(entry.Key) ||
                Inventory.instance.inventory[entry.Key] < entry.Value)
            {
                return false;
            }
        }

        return true;
    }

    public void Craft(Recipe recipe)    
    {               
        if (HasAllComponents(recipe))
        {      
            // Add the newly crafted item into inventory
            SoundController.Play((int)SFX.Crafting, 0.5f);
            inventory.AddItem(recipe.recipeItem);

            foreach (Item compItem in recipe.componentItems)
            {
                inventory.RemoveItem(compItem);
            }                   
        }
        else
        {
            Debug.Log("Do not have all componenets to make " + recipe.recipeItem.name);
        }       
    }

    // Used previously when components were strings, Now no longer used
    public bool HasAllComponents()
    {
        bool[] hasAllComponents = new bool[slotIndices.Length];

        for (int i = 0; i < componentNames.Length; ++i)
        {
            string componentName = componentNames[i];
            componentName = Regex.Replace(componentName, @"\s+", "");
            //Debug.Log(componentName);
            for (int j = 0; j < inventory.inventory.Count; ++j)
            {
                //string itemName = inventory.inventory[j].name;
                string itemName = "";
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
