using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Land : MonoBehaviour, ITimeTracker
{
    public enum LandStatus
    {
        Soil, Farmland, Watered
    }

    public LandStatus landStatus;

    public Material soilMat, farmlandMat, wateredMat;
    new Renderer renderer;

    // The selection gameobject to enable when the player is selecting the land
    public GameObject select;

    //Cache the time the land was watered
    GameTimeStamp timeWatered;
    void Start()
    {
        renderer = GetComponent<Renderer>();
        // Set the soil material to default
        SwitchLandStatus(LandStatus.Soil);
        //Deselect the land by default
        Select(false);

    }

    //Decide what material to switch to
    public void SwitchLandStatus(LandStatus statusToSwitch)
    {
        landStatus = statusToSwitch;
        Material materialToSwitch = soilMat;

         


        switch (statusToSwitch)
        {
            case LandStatus.Soil:
                materialToSwitch = soilMat;
                break;
            case LandStatus.Farmland:
                materialToSwitch = farmlandMat;
                break;
            case LandStatus.Watered:
                materialToSwitch = wateredMat;
                timeWatered = TimeManager.Instance.GetGameTimeStamp();
                break;

              
        }

        //Get the renderer to apply the changes
        renderer.material = materialToSwitch;
    }

    public void Select(bool toggle)
    {
        select.SetActive(toggle);
    }

    public void Interact()
    {
        //Checks the players tool slot
        ItemData toolSlot = InventoryManager.Instance.equippedTool;

        EquipmentData equipmentTool = toolSlot as EquipmentData;

        //Check if it is of type equipment
        if( equipmentTool != null )
        {
            //get the tool type
            EquipmentData.ToolType toolType = equipmentTool.toolType;

            switch (toolType)
            {
                case EquipmentData.ToolType.Hoe:
                    SwitchLandStatus (LandStatus.Farmland);
                    break;
                case EquipmentData.ToolType.Wateringcan:
                    SwitchLandStatus(LandStatus.Watered);
                    break;
            }
        }
    }

    public void ClockUpdate(GameTimeStamp timeStamp)
    {
        //Check if 24 hours has passed since last watered
        if(landStatus == LandStatus.Watered)
        {

        }
    }
}
