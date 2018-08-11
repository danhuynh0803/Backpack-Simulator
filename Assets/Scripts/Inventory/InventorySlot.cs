using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {
    
    public Text weightText;

    private Item item;
    public Image itemIcon;
    public int itemSlotIndex;

    public void AddItem(Item newItem, int slotIndex)
    {
        item = newItem;
        itemIcon.sprite = newItem.icon;
        weightText.text = newItem.weight.ToString();
        itemSlotIndex = slotIndex;
        itemIcon.enabled = true;
    }

    public void ClickItemButton()
    {
        Debug.Log("ClickItemButton at slot:" + itemSlotIndex);
        
        GameObject itemDescriptionPanel = FindObjectOfType<InventoryUI>().itemDescriptionPanel;
        itemDescriptionPanel.SetActive(true);

        FindObjectOfType<InventoryUI>().SetItemDescription(this.item);
    }

    public void ClickDeleteButton()
    {
        Inventory.instance.RemoveItem(itemSlotIndex);
    }

}
