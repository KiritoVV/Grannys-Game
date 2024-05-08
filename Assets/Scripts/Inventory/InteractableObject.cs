using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    //The item information of the game object
    public ItemData item;

    public virtual void Pickup()
    {
        //Sets the players inventory to the item
        InventoryManager.Instance.EquipHandSlot(item);

        InventoryManager.Instance.RenderHand();

        Destroy(gameObject);
    }
}
