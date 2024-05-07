using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour, ITimeTracker
{
    public static UIManager Instance { get; private set; }

    [Header("Status Bar")]
    //Tool equip slot on the status
    public Image toolEquipSlot;

    //Time ui
    public Text timeText;
    public Text dateText;

    [Header("Inventory system")]

    //The item slot ui
    public InventorySlot[] toolSlots;

    //The tool equip slot UI on the inventory panel
    public HandInventorySlot toolHandSlot;

    // The tool slots ui
    public InventorySlot[] itemSlots;

    //The item equip slot UI on the inventory panel
    public HandInventorySlot itemHandSlot;

    //The inventory Panel
    public GameObject inventoryPanel;

    //Item infro box
    public Text itemNameText;
    public Text itemDescriptionText;

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

    private void Start()
    {
        RenderInventory();
        AssignSlotIndexes();

        //Add UiManager to the list of objects TimeManager will notify when the time update
        TimeManager.Instance.Registertracker(this);
    }

    //Iterate through the slot ui elements and assign its reference slot index
    public void AssignSlotIndexes()
    {
        for (int i = 0; i < toolSlots.Length; i++)
        {
            toolSlots[i].AssignIndex(i);
            itemSlots[i].AssignIndex(i);
        }
    }

    public void RenderInventory()
    {
        // Get the inventory tool slots from inventory manager
        ItemData[] inventoryToolSlot = InventoryManager.Instance.toolSlots;

        // Get the inventory item slots from the inventory manager
        ItemData[] inventoryItemSlot = InventoryManager.Instance.itemSlots;


        // Render the tool section
        RenderInventoryPanel(inventoryToolSlot, toolSlots);

        //Render the item section
        RenderInventoryPanel(inventoryItemSlot, itemSlots);

        //Tender the equipped slot
        toolHandSlot.Display(InventoryManager.Instance.equippedToolSlots);
        itemHandSlot.Display(InventoryManager.Instance.equippedItemSlots);



        //Get Tool Equip from InventoryManager
        ItemData equippedTool = InventoryManager.Instance.equippedToolSlots;
        //check if there is an item to display

        if (equippedTool != null)
        {
            toolEquipSlot.sprite = equippedTool.thumbnail;

            toolEquipSlot.gameObject.SetActive(true);

            return;

        }

        toolEquipSlot.gameObject.SetActive(false);
    }

    void RenderInventoryPanel(ItemData[] slots, InventorySlot[] uiSlots)
    {
        for (int i = 0; i < uiSlots.Length; i++)
        {
            uiSlots[i].Display(slots[i]);
        }
    }

    public void ToggleInventoryPanel()
    {
        //If the panel is hidden, show it and vice versa
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);

        RenderInventory();
    }

    //Display item info on the item infobox
    public void DisplayeItemInfo(ItemData data)
    {
        if (data == null)
        {
            itemNameText.text = "";
            itemDescriptionText.text = "";
            return;
        }

        itemNameText.text = data.name;
        itemDescriptionText.text = data.description;
    }

    public void ClockUpdate(GameTimeStamp timestamp)
    {
        //Handle the time 

        //Get the hours and minutes
        int hours = timestamp.hour;
        int minutes = timestamp.minute;

        //Am or Pm
        string prefix = "AM ";

        //Convert  hours to 12 hours clock
        if (hours > 12)
        {
            //Time becomes Pm
            prefix = "PM ";
            hours = 12;
        }

        timeText.text = prefix + hours + ":" + minutes.ToString("00");

        //Handle the date
        int day = timestamp.day;
        string season = timestamp.season.ToString();
        string dayOfTheWeek = timestamp.GetDayOfTheWeek().ToString();

        //Format it for the date text display
        dateText.text = season + " " + day + " (" + dayOfTheWeek + ") ";
    }
}
