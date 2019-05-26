using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class Shopping : MonoBehaviour {

    public List<GameObject> shoppingCanvases = new List<GameObject>();
    private Inventory inventory;
    public List<Item> merchandises = new List<Item>();
    private int[] slotIndices;
    private bool isShopping;
    private Player player;
    private void Start()
    {
        player = FindObjectOfType<Player>();
        ShoppingUI shoppingUI = FindObjectOfType<ShoppingUI>();
        inventory = FindObjectOfType<Inventory>();
        if (shoppingUI != null)
        {
            foreach (Item merchandise in merchandises)
            {
                if (merchandise != null)
                {
                    shoppingUI.AddItemToUI(merchandise);
                }
            }
        }
    }

    public void ToggleShoppingCanvases()
    {
        isShopping = !isShopping;
        if (isShopping)
        {
            //inventoryBlockRayCast.SetActive(false);
            foreach (GameObject canvas in shoppingCanvases)
            {
                if (canvas != null)
                    canvas.SetActive(true);
            }
        }
        else
        {
            //inventoryBlockRayCast.SetActive(true);
            foreach (GameObject canvas in shoppingCanvases)
            {
                if (canvas != null)
                    canvas.SetActive(false);
            }
        }
    }

    public bool HasEnoughGold(Item item)
    {
        if (inventory == null)
        {
            inventory = FindObjectOfType<Inventory>();
        }

        if (player.GetGold() >= item.cost)
            return true;
        return false;
    }

    public void Buy(Item item)    
    {        
        if (inventory == null)
        {
            inventory = FindObjectOfType<Inventory>();
        }
        
        if (HasEnoughGold(item))
        {
            player.DecrementGold(item.cost);
            inventory.AddItem(item);
        }
        else
        {
            Debug.Log("Do not have enough gold");
        }       
    }
}
