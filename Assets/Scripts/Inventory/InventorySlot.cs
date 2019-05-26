using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {
    
    public Text totalAmountText;

    private Item item;
    public Image itemIcon;
    public int itemSlotIndex;
    private GameObject itemDescriptionPanel;

    private void Update()
    {  
        // Error check purpose
        if (item != null && Inventory.instance.inventory.ContainsKey(item))
        {
            totalAmountText.text = Inventory.instance.inventory[item].ToString();
        }
        else
        {
            Destroy(this.gameObject);
            itemDescriptionPanel.SetActive(false);
        }
    }

    public void AddItem(Item newItem, int slotIndex)
    {
        item = newItem;
        itemIcon.sprite = newItem.icon;            
        itemSlotIndex = slotIndex;
        itemIcon.enabled = true;       
    }

    public void OnClickItemButton()
    {
        //Debug.Log("ClickItemButton at slot:" + itemSlotIndex);
        //itemDescriptionPanel = FindObjectOfType<InventoryUI>().itemDescriptionPanel;
        //itemDescriptionPanel.SetActive(true);
        //FindObjectOfType<InventoryUI>().SetItemDescription(this.item, itemSlotIndex);
        itemDescriptionPanel.GetComponent<Image>().raycastTarget = true;
        Vector3 buttonPoint = GetComponent<RectTransform>().position;
        itemDescriptionPanel.GetComponent<RectTransform>().position = buttonPoint;
        FindObjectOfType<InventoryUI>().SetItemDescription(this.item, itemSlotIndex);
    }

    public void OnMouseEnterItemButton()
    {
        if (itemDescriptionPanel != null)
        {
            if (itemDescriptionPanel.GetComponent<Image>().raycastTarget == false)
            {
                itemDescriptionPanel.SetActive(true);
                FindObjectOfType<InventoryUI>().SetItemDescription(this.item, itemSlotIndex);
                Vector3 buttonPoint = GetComponent<RectTransform>().position;
                itemDescriptionPanel.GetComponent<RectTransform>().position = buttonPoint;
            }
        }
        else
        {
            itemDescriptionPanel = FindObjectOfType<InventoryUI>().itemDescriptionPanel;
            itemDescriptionPanel.SetActive(true);
            Vector3 buttonPoint = GetComponent<RectTransform>().position;
            itemDescriptionPanel.GetComponent<RectTransform>().position = buttonPoint;
            FindObjectOfType<InventoryUI>().SetItemDescription(this.item, itemSlotIndex);
        }
    }

    //unity has an issue with thing.......
    //On the boundary., pointerEnter is called immediately after 
    public void OnMouseExitItemButton()
    {
        if(itemDescriptionPanel != null)
        {
            itemDescriptionPanel.SetActive(false);
        }
    }
   
    public void ClickDeleteButton()
    {
        //Inventory.instance.RemoveItem(itemSlotIndex);
    }
    

}
