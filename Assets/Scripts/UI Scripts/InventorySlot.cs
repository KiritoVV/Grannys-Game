using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Pool;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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

    public void OnPointerEnter(PointerEventData eventdata)
    {
        UIManager.Instance.DisplayeItemInfo(itemToDisplay);
    }

    public void OnPointerExit(PointerEventData eventdata)
    {
       
    }
}
