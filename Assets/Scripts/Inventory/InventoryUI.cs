﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour {

    #region Singleton
    public static InventoryUI instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion

    Inventory inventory;
    public GameObject inventorySlot;

	// Use this for initialization
	void Start () {
        inventory = Inventory.instance;
	} 
    
	// Update is called once per frame
	public void AddItemToUI (Item item, int slotIndex)    
    {
        //Debug.Log("AddItemToUI");
        GameObject invSlot = Instantiate(inventorySlot, this.transform) as GameObject;
        invSlot.GetComponent<InventorySlot>().AddItem(item, slotIndex);
        invSlot.transform.parent = gameObject.transform;       
	}

    public void DeleteAllInventoryUI()
    {
        foreach (Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void UpdateInventoryUI(List<Item> itemList)
    {
        // Delete all objects then rebuild, so that each object shifts over
        DeleteAllInventoryUI();

        Debug.Log(itemList.Count);
        int slotIndex = 0;
        foreach (Item item in itemList)
        {         
            if (item != null)
            {
                //Debug.Log("Item name:" + item.name);
                AddItemToUI(item, slotIndex);
            }
        }
    }
}