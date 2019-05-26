using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    #region Singleton
    public static Inventory instance;    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion

    public List<Item> startingItems;
    private InventorySlot[] slots = new InventorySlot[30];
    public Dictionary<Item, int> inventory = new Dictionary<Item, int>(); 
    
    private void Start()
    {        
        foreach (Item item in startingItems)
        {
            if (item != null)
            {
                AddItem(item);                               
            }
        }   
    }

    public void AddItem(Item newItem)
    {
        // Check if the item already exists in the inventory                
        // if so, just increment the count 
        if (inventory.ContainsKey(newItem))
        {            
            inventory[newItem]++;
            //Debug.Log(inventory[newItem]);
        }
        // else, create a list of size one for that new item
        else
        {
            inventory.Add(newItem, 1);         
        }                  
    }

    public void AddItems(List<Item> droppedItems)
    {
        if (droppedItems == null)
        {
            Debug.Log("List of dropped items is null");
        }
        else if (droppedItems.Count <= 0)
        {
            Debug.Log("No items were dropped?");
        }

        foreach (Item item in droppedItems)
        {
            if (item != null)
            {
                AddItem(item);          
            }            
        }        
    }

    public void RemoveItem(Item item)
    {
        inventory[item]--;

        if (inventory[item] <= 0)
        {
            inventory.Remove(item);
        }
    }

    public int GetTotalItems()
    {
        // Remove later, not needed
        return 0;
    }
    
}
