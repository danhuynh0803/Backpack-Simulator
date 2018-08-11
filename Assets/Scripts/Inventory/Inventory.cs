﻿using System.Collections;
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
        else
        {
            Debug.Log("Why are there two inventories?");
            return;
        }
    }
    #endregion

    private List<Item> itemList = new List<Item>(); 
    private InventoryUI inventoryUI;

    private void Start()
    {
        inventoryUI = InventoryUI.instance;
        if (instance.inventoryUI == null)
        {
            Debug.LogError("inventoryUI is null");
        }
    }

    private void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddItem(null);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            RemoveItem(0);
        }
        */
    }

    public void AddItem(Item item)
    {
        Debug.Log("AddItem");
        itemList.Add(item);

        if (inventoryUI == null)
        {
            inventoryUI = InventoryUI.instance;
        }

        if (inventoryUI != null)
            inventoryUI.AddItemToUI(item, GetTotalItems()-1);
        else
            Debug.Log("inventoryUI is null when adding");
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

    public void RemoveItem(int removeItemIndex)
    {
        itemList.RemoveAt(removeItemIndex);
        inventoryUI.UpdateInventoryUI(itemList);
    }

    public int GetTotalItems()
    {
        return itemList.Count;
    }
    
}