using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegrowableHarvest : InteractableObject
{
    CropBehvaviour parentCrop;

    //Sets the parent crop
    public void SetParent(CropBehvaviour parentCrop)
    {
        this.parentCrop = parentCrop;
    }

    public override void Pickup()
    {
        //Set the player's inventory to the item
        InventoryManager.Instance.EquipHandSlot(item);

        //Update the changes in the scene
        InventoryManager.Instance.RenderHand();

        //Set the parent crop back to seedling to regrow it
        parentCrop.Regrow();

    }
}
