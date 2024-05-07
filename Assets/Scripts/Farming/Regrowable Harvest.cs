using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegrowableHarvest : InteractableObject
{
    CropBehvaviour parentCrop;

    public void SetParent(CropBehvaviour parentCrop)
    {
        this.parentCrop = parentCrop;
    }

    public override void Pickup()
    {
        //Sets the players inventory to the item
        InventoryManager.Instance.equippedItemSlots = item;
        //Update the changes in the scene
        InventoryManager.Instance.RenderHand();

        //Set the  parents crop back to seedling to regrow it 
        parentCrop.Regrow();
    }
}
