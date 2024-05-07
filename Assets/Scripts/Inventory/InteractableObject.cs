using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    //The item information of the game object
    public ItemData item;

    public virtual void Pickup()
    {
        InventoryManager.Instance.equippedItemSlots = item;

        InventoryManager.Instance.RenderHand();

        Destroy(gameObject);
    }
}
