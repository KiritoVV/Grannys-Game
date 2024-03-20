using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Inventory system")]
    public InventorySlot[] toolSlots;
    public InventorySlot[] itemSlots;

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
}
