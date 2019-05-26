using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InventoryUI : MonoBehaviour {

    #region Singleton
    public static InventoryUI instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion

    public GameObject itemDescriptionPanel;
    public Text itemName;
    public Text itemDescriptionText;
    public Image itemDescriptionIcon;
    public GameObject inventorySlot;
    public Scrollbar inventoryScrollBar;
    private Item selectedItem;
    private int selectedItemIndex;
    
    public void Start()
    {
        Debug.Log("UI Start called");

    }

    private void Update()
    {
        // For testing 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HealthPotion temp = new HealthPotion();
            AddItemToUI(temp, 0);
        }
    }

    public void SetItemDescription(Item item, int slotIndex)
    {
        selectedItem = item;
        itemName.text = item.name;
        itemDescriptionText.text = item.description;
        itemDescriptionIcon.sprite = item.icon;
        selectedItemIndex = slotIndex;
    }

    public void UseItemButton()
    {
        if (selectedItem.ActivateEffect())
        {
            Inventory.instance.RemoveItem(selectedItem);
        }
        else
        {
            Debug.Log("Item cannot be used");
        }
    }

    public void DiscardItemButton()
    {
        // TODO
    }
    
	// Update is called once per frame
	public void AddItemToUI (Item item, int slotIndex)    
    {
        //Debug.Log("AddItemToUI");
        GameObject invSlot = Instantiate(inventorySlot, this.transform) as GameObject;
        invSlot.GetComponent<InventorySlot>().AddItem(item, slotIndex);
        invSlot.transform.SetParent(gameObject.transform);       
	}

    public void DeleteAllInventoryUI()
    {
        foreach (Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void UpdateInventoryUI()
    {
        // Delete all objects then rebuild, so that each object shifts over
        DeleteAllInventoryUI();

        // Create the inventory slots based on what is currently stored in the inventory
        int slotIndex = 0;
        foreach (KeyValuePair<Item, int> entry in Inventory.instance.inventory)
        {
            AddItemToUI(entry.Key, slotIndex++);
        }      
    }
}
