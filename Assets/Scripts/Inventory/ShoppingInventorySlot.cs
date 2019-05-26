using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShoppingInventorySlot : MonoBehaviour {

    private Item merchandise;
    public Text nameText;    
    public Image itemIcon;

    private Color normalColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    public Color disabledColor = new Color(0.2f, 0.2f, 0.2f, 0.5f);

    private Shopping shopping;

    private void Update()
    {
        shopping = FindObjectOfType<Shopping>();
        if (shopping.HasEnoughGold(merchandise))
        {
            itemIcon.color = normalColor;
        }
        else
        {
            itemIcon.color = disabledColor;
        }
    }   

    public void AddItem(Item item)
    {
        if (item != null)
        {
            merchandise = item;
            itemIcon.sprite = item.icon;
            nameText.text = item.name;
        }
        else
        {
            Debug.LogError("Null item added");
        }
    }

    public void ClickItemButton()
    {
        GameObject itemDescriptionPanel = FindObjectOfType<ShoppingUI>().shoppingDescriptionPanel;
        itemDescriptionPanel.SetActive(true);
        FindObjectOfType<ShoppingUI>().SetItemDescription(merchandise);
    }

}
