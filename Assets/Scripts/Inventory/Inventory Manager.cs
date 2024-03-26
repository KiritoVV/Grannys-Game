using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
   public static InventoryManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null && Instance != this )
        {
            Destroy(this);
        }
        else
        {
            // Set the static instance to this instance
            Instance = this;
        }
    }

    [Header ("Tools")]
    //Tool slots
    public ItemData[] tool = new ItemData[8];
    //Item in the players hand
    public ItemData equippedTool = null;

    [Header ("Items")]
    //Item slots
    public ItemData[] items = new ItemData[8];
    //Item in the players hand
    public ItemData equippedItem = null;

    // Equipping

    //Handles movement from inventory to hand

    public void InventoryToHand(int slotIndex, InventorySlot.InventoryType inventoryType)
    {
       if(inventoryType == InventorySlot.InventoryType.Item)
        {
            //Cache the inventory slot itemData from InventoryManager
            ItemData itemToEquip = items[slotIndex];

            //Change the inventory Slot to the hands
            items[slotIndex] = equippedItem;

            //Change the hands Slot to the inventory slot
            equippedItem = itemToEquip;
        }
        else
        {
            //Cache the inventory slot itemData from InventoryManager
            ItemData toolToEquip = tool[slotIndex];

            //Change the inventory Slot to the hands
            tool[slotIndex] = equippedTool;

            //Change the hands Slot to the inventory slot
            equippedTool = toolToEquip;
        }

        //Update the changes to the ui
        UIManager.Instance.RenderInventory();
    }

    //Handles movement of item from hand to inventory

    public  void HandToInventory(InventorySlot.InventoryType inventoryType)
    {

    }

}
