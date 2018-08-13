using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingUI : MonoBehaviour {

    public GameObject craftingInventorySlot;
    public Text itemName;
    public Text itemDescriptionText;
    public Image itemDescriptionIcon;
    public Text itemComponentText;
    private Recipe selectedRecipe;
    public GameObject craftingDescriptionPanel;

    public void SetItemDescription(Recipe recipe)
    {
        selectedRecipe = recipe;
        Item item = recipe.recipeItem;
        itemName.text = item.name;
        itemDescriptionText.text = item.description;
        itemDescriptionIcon.sprite = item.icon;
        itemComponentText.text = "Made from: ";
        for (int i = 0; i < recipe.components.Count; ++i)
        {
            itemComponentText.text += recipe.components[i];
            if (i < recipe.components.Count - 1)
            {
                itemComponentText.text += ", ";
            }
        }
    }

    // Update is called once per frame
    public void AddItemToUI(Recipe recipe)
    {
        //Debug.Log("AddItemToUI");
        GameObject invSlot = Instantiate(craftingInventorySlot, this.transform) as GameObject;
        invSlot.GetComponent<CraftingInventorySlot>().AddItem(recipe);
        invSlot.transform.parent = gameObject.transform;
    }

    public void ClickButton()
    {
        FindObjectOfType<Crafting>().Craft(selectedRecipe);
    }
}
