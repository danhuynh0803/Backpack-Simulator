using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingUI : MonoBehaviour {

    public GameObject craftingInventorySlot;
    public GameObject componentSlot;
    public Text itemName;
    public Text itemDescriptionText;
    public Image itemDescriptionIcon;
    public Text itemComponentText;

    public GameObject componentGrid;

    private Recipe selectedRecipe;
    public GameObject craftingDescriptionPanel;

    public void SetItemDescription(Recipe recipe)
    {
        selectedRecipe = recipe;
        Item item = recipe.recipeItem;
        itemName.text = item.name;
        itemDescriptionText.text = item.description;
        itemDescriptionIcon.sprite = item.icon;

        foreach (Transform child in componentGrid.transform)
        {
            Destroy(child.gameObject);
        }

        // Evaluate for any duplicate components
        
        Dictionary<Item, int> componentItems = new Dictionary<Item, int>();
        for (int i = 0; i < recipe.componentItems.Count; ++i)
        {
            Item cItem = recipe.componentItems[i];
            if (componentItems.ContainsKey(cItem))
            {
                componentItems[cItem]++;
            }
            else
            {
                componentItems.Add(cItem, 1);
            }
            
        }
        

        // Display each unique component needed to create the recipe
   
        foreach (KeyValuePair<Item, int> entry in componentItems)
        {
            Debug.Log("Add Comp Slot for Crafting");
            GameObject compSlot = Instantiate(componentSlot, this.transform) as GameObject;
            compSlot.GetComponent<ComponentSlot>().AddItem(entry.Key, entry.Value);
            compSlot.transform.SetParent(componentGrid.transform);
        }
    }

    public void AddItemToUI(Recipe recipe)
    {
        //Debug.Log("AddItemToUI");
        GameObject invSlot = Instantiate(craftingInventorySlot, this.transform) as GameObject;
        invSlot.GetComponent<CraftingInventorySlot>().AddItem(recipe);
        invSlot.transform.SetParent(gameObject.transform);
    }

    public void ClickButton()
    {
        FindObjectOfType<Crafting>().Craft(selectedRecipe);
    }
}
