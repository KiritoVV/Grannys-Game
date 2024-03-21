using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Inventory system")]
    
    //The item slot ui
    public InventorySlot[] toolSlots;

    // The tool slots ui
    public InventorySlot[] itemSlots;

    //The inventory Panel
    public GameObject inventoryPanel;

    //Item infro box
    public Text itemNameText;
    public Text itemDescriptionText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            // Set the static instance to this instance
            Instance = this;
        }
    }

    private void Start()
    {
        RenderInventory();
    }

    public void RenderInventory()
    {
        // Get the inventory tool slots from inventory manager
        ItemData[] inventoryToolSlot = InventoryManager.Instance.tool;

        // Get the inventory item slots from the inventory manager
        ItemData[] inventoryItemSlot = InventoryManager.Instance.items;


        // Render the tool section
        RenderInventoryPanel(inventoryToolSlot, toolSlots);

        //Render the item section
        RenderInventoryPanel(inventoryItemSlot, itemSlots);
    }

    void RenderInventoryPanel(ItemData[]slots, InventorySlot[] uiSlots)
    {
        for(int i = 0; i < uiSlots.Length; i++)
        {
            uiSlots[i].Display(slots[i]);
        }
    }

    public void ToggleInventoryPanel()
    {
       //If the panel is hidden, show it and vice versa
       inventoryPanel.SetActive(!inventoryPanel.activeSelf);

        RenderInventory();
    }

    //Display item info on the item infobox
    public void DisplayeItemInfo(ItemData data)
    {
       if(data == null)
       {
            itemNameText.text = "";
            itemDescriptionText.text = "";
            return;
       }
        
        itemNameText.text = data.name;
        itemDescriptionText.text = data.description;
    }
}
