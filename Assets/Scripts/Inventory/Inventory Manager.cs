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
    public ItemData[] tool = new ItemData[8];
    //Item in the players hand
    public ItemData equippedTool = null;

    [Header("Items")]
    //Item slots
    public ItemData[] items = new ItemData[8];
    //Item in the players hand
    public ItemData equippedItem = null;

    public Transform handPoint;

    // Equipping

    //Handles movement from inventory to hand

    public void InventoryToHand(int slotIndex, InventorySlot.InventoryType inventoryType)
    {
        if (inventoryType == InventorySlot.InventoryType.Item)
        {
            //Cache the inventory slot itemData from InventoryManager
            ItemData itemToEquip = items[slotIndex];

            //Change the inventory Slot to the hands
            items[slotIndex] = equippedItem;

            //Change the hands Slot to the inventory slot
            equippedItem = itemToEquip;

            RenderHand();
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

    public void HandToInventory(InventorySlot.InventoryType inventoryType)
    {
        if (inventoryType == InventorySlot.InventoryType.Item)
        {
            //Iterate through each inventory slot and find an empty slot

            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] == null)
                {
                    //Sends the equipped item over to its new slot
                    items[i] = equippedItem;

                    //Remove the item from the hand
                    equippedItem = null;

                    break;
                }
            }

            RenderHand();
        }
        else
        {
            for (int i = 0; i < tool.Length; i++)
            {
                if (tool[i] == null)
                {
                    //Sends the equipped Tool over to its new slot
                    tool[i] = equippedTool;

                    //Remove the Tool from the hand
                    equippedTool = null;

                    break;
                }
            }

        }

        UIManager.Instance.RenderInventory();

    }

    //Render the players equipped item in the scene
    public void RenderHand()
    {
        //Resets objects on the hand
        if (handPoint.childCount > 0)
        {
            Destroy(handPoint.GetChild(0).gameObject);
        }

       if(equippedItem != null)
       {
            Instantiate(equippedItem.gameModel, handPoint);
       }
    }

}
