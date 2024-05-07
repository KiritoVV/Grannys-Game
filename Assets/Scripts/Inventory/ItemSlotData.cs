using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlotData 
{
    public ItemData itemData;
    public int quantity;

    public ItemSlotData(ItemData itemData, int quantity)
    {
        this.itemData = itemData;
        this.quantity = quantity;
        ValidateQuantity();
    }

    public ItemSlotData(ItemData itemData)
    {
        this.itemData = itemData;
        quantity = 1;
        ValidateQuantity();
    }

    public void AddQuantity()
    {
        AddQuantity(1);
    }

    public void AddQuantity(int amountToAdd)
    {
        quantity += amountToAdd;
    }

    public void Remove()
    {
        quantity--;
        ValidateQuantity();
    }

    private void ValidateQuantity()
    {
        if(quantity <= 0 || itemData == null)
        {
            Empty();
        }
    }

    public void Empty()
    {
        itemData = null;
        quantity = 0;
    }


     
}
