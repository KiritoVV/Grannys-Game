using System.Collections;
using System.Collections.Generic;
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

}
