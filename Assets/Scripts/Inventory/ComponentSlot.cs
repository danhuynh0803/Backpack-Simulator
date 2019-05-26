using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComponentSlot : MonoBehaviour
{
    public Text craftText;
    public Text itemName;
    private int amountNeeded;
    private Item item;
    public Image itemIcon;
    
    private GameObject itemDescriptionPanel;

    public void AddItem(Item newItem, int amountNeeded)
    {
        item = newItem;
        itemName.text = newItem.name;
        itemIcon.sprite = newItem.icon;
        this.amountNeeded = amountNeeded;        
    }
    
    void Update()
    {
        // Display the amount we have for this componenent within the inventory   
        int amount = 0;
        if (Inventory.instance.inventory.ContainsKey(item))
        {
            amount = Inventory.instance.inventory[item];
        }
        
        craftText.text = amount + "/" + amountNeeded;
    }
}
