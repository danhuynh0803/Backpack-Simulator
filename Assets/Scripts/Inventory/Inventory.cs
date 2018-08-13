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

    public List<Item> itemList = new List<Item>(); 
    private InventoryUI inventoryUI;
    
    private void Start()
    {
        inventoryUI = FindObjectOfType<InventoryUI>();
        if (inventoryUI == null)
        {
            Debug.LogError("inventoryUI is null");
        }

        // Add initial items into the bag
        int count = 0;
        foreach (Item item in itemList)
        {
            if (item != null)
            {
                inventoryUI.AddItemToUI(item, count);
                count++;
            }
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
        //Debug.Log("AddItem");
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
        inventoryUI = FindObjectOfType<InventoryUI>();

        if (itemList == null)
        {
            Debug.Log("itemList is null");
        }

        if (inventoryUI == null)
        {
            Debug.Log("inventoryUI is null");
        }
        inventoryUI.UpdateInventoryUI(itemList);        
    }

    public int GetTotalItems()
    {
        return itemList.Count;
    }
    
}
