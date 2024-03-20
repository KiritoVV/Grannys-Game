using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    ItemData itemToDisplay;

    public Image itemDisplayImage;
    public void Display(ItemData itemToDisplay)
    {
        //check if there is an item to display
        if (itemToDisplay != null)
        {
            itemDisplayImage.sprite = itemToDisplay.thumbnail;
            this.itemToDisplay = itemToDisplay;
            itemDisplayImage.gameObject.SetActive(true);

            return;

        }

        itemDisplayImage.gameObject.SetActive(false);
    }
}
