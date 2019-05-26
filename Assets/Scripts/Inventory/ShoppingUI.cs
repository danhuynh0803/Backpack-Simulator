using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShoppingUI : MonoBehaviour {

    public GameObject shoppingInventorySlot;
    public Text itemName;
    public Text itemDescriptionText;
    public Text itemCost;
    public Image itemDescriptionIcon;
    private Item selectedMerchandise;
    public GameObject shoppingDescriptionPanel;

    public void SetItemDescription(Item item)
    {
        selectedMerchandise = item;
        itemName.text = item.name;
        itemDescriptionText.text = item.description;
        itemCost.text = "Cost: " + item.cost.ToString() + " gold";
        itemDescriptionIcon.sprite = item.icon;
    }

    // Update is called once per frame
    public void AddItemToUI(Item item)
    {
        //Debug.Log("AddItemToUI");
        GameObject invSlot = Instantiate(shoppingInventorySlot, this.transform) as GameObject;
        invSlot.GetComponent<ShoppingInventorySlot>().AddItem(item);
        invSlot.transform.SetParent(gameObject.transform);
    }

    public void ClickButton()
    {
        FindObjectOfType<Shopping>().Buy(selectedMerchandise);
    }
}
