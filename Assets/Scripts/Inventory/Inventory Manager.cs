using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

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

    [Header("Tools")]
    //Tool slots
    private ItemSlotData[] toolSlots = new ItemSlotData[8];
    //Item in the players hand
    private ItemSlotData equippedToolSlots = null;

    [Header("Items")]
    //Item slots
    private ItemSlotData[] itemSlots = new ItemSlotData[8];
    //Item in the players hand
    private ItemSlotData equippedItemSlots = null;

    public Transform handPoint;

    // Equipping

    //Handles movement from inventory to hand

    public void InventoryToHand(int slotIndex, InventorySlot.InventoryType inventoryType)
    {
        /*
        if (inventoryType == InventorySlot.InventoryType.Item)
        {
            //Cache the inventory slot itemData from InventoryManager
            ItemSlotData itemToEquip = itemSlots[slotIndex];

            //Change the inventory Slot to the hands
            itemSlots[slotIndex] = equippedItemSlots;

            //Change the hands Slot to the inventory slot
            equippedItemSlots = itemToEquip;

            RenderHand();
        }
        else
        {
            //Cache the inventory slot itemData from InventoryManager
            ItemSlotData toolToEquip = toolSlots[slotIndex];

            //Change the inventory Slot to the hands
            toolSlots[slotIndex] = equippedToolSlots;

            //Change the hands Slot to the inventory slot
            equippedToolSlots = toolToEquip;
        }

        //Update the changes to the ui
        UIManager.Instance.RenderInventory();*/
        
    }

    //Handles movement of item from hand to inventory

    public void HandToInventory(InventorySlot.InventoryType inventoryType)
    {
        /*
        if (inventoryType == InventorySlot.InventoryType.Item)
        {
            //Iterate through each inventory slot and find an empty slot

            for (int i = 0; i < itemSlots.Length; i++)
            {
                if (itemSlots[i] == null)
                {
                    //Sends the equipped item over to its new slot
                    itemSlots[i] = equippedItemSlots;

                    //Remove the item from the hand
                    equippedItemSlots = null;

                    break;
                }
            }

            RenderHand();
        }
        else
        {
            for (int i = 0; i < toolSlots.Length; i++)
            {
                if (toolSlots[i] == null)
                {
                    //Sends the equipped Tool over to its new slot
                    toolSlots[i] = equippedToolSlots;

                    //Remove the Tool from the hand
                    equippedToolSlots = null;

                    break;
                }
            }

        }

        UIManager.Instance.RenderInventory(); */

    }

    //Render the players equipped item in the scene
    public void RenderHand()
    {
        //Resets objects on the hand
        if (handPoint.childCount > 0)
        {
            Destroy(handPoint.GetChild(0).gameObject);
        }

       if(equippedItemSlots != null)
       {
            Instantiate(GetEquippedSlotItem(InventorySlot.InventoryType.Item).gameModel, handPoint);
       }
    }

    //Inventory Slot Data

    public ItemData GetEquippedSlotItem(InventorySlot.InventoryType inventoryType)
    {
        if(inventoryType == InventorySlot.InventoryType.Item)
        {
            return equippedItemSlots.itemData;
        }
        return equippedItemSlots.itemData;
    }

    public ItemSlotData GetEquippedSlot(InventorySlot.InventoryType inventoryType)
    {
        if(inventoryType == InventorySlot.InventoryType.Item)
        {
            return equippedItemSlots;
        }
        return equippedItemSlots;
    }

}
