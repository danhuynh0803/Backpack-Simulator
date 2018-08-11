using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {

    public GameObject itemDescriptionPanel;
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
        //itemDescriptionPanel.SetActive(true);
        // Do something when you click the item
        // Maybe open a panel that shows description and tips and a use button?
    }

    public void ClickDeleteButton()
    {
        Inventory.instance.RemoveItem(itemSlotIndex);
    }

}
