using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingInventorySlot : MonoBehaviour {

    private Recipe recipe;
    public Text nameText;    
    public Image itemIcon;

    public void AddItem(Recipe recipe)
    {
        if (recipe != null)
        {
            this.recipe = recipe;
            Item item = recipe.recipeItem;

            itemIcon.sprite = item.icon;
            nameText.text = item.name;
        }
        else
        {
            Debug.LogError("Null recipe added");
        }
    }

    public void ClickItemButton()
    {
        GameObject itemDescriptionPanel = FindObjectOfType<CraftingUI>().craftingDescriptionPanel;
        itemDescriptionPanel.SetActive(true);
        FindObjectOfType<CraftingUI>().SetItemDescription(recipe);
    }

}
