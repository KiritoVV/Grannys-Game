using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Land : MonoBehaviour, ITimeTracker
{
    public int id;
    public enum LandStatus
    {
        Soil, Farmland, Watered
    }

    public LandStatus landStatus;

    public Material soilMat, farmlandMat, wateredMat;
    new Renderer renderer;

    //The selection gameobject to enable when the player is selecting the land
    public GameObject select;

    //Cache the time the land was watered 
    GameTimeStamp timeWatered;

    [Header("Crops")]
    //The crop prefab to instantiate
    public GameObject cropPrefab;

    //The crop currently planted on the land
    CropBehaviour cropPlanted = null;

    // Start is called before the first frame update
    void Start()
    {
        //Get the renderer component
        renderer = GetComponent<Renderer>();

        //Set the land to soil by default
        SwitchLandStatus(LandStatus.Soil);

        //Deselect the land by default
        Select(false);

        //Add this to TimeManager's Listener list
        TimeManager.Instance.Registertracker(this);
    }

    public void LoadLandData(LandStatus statusToSwitch, GameTimeStamp lastWatered)
    {
        //Set land status accordingly
        landStatus = statusToSwitch;
        timeWatered = lastWatered;

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
                break;

        }

        
        renderer.material = materialToSwitch;

    }

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

        LandManager.Instance.OnLandStateChange(id, landStatus, timeWatered);
    }

    public void Select(bool toggle)
    {
        select.SetActive(toggle);
    }

    
    public void Interact()
    {
        
        ItemData toolSlot = InventoryManager.Instance.GetEquippedSlotItem(InventorySlot.InventoryType.Tool);

        
        if (!InventoryManager.Instance.SlotEquipped(InventorySlot.InventoryType.Tool))
        {
            return;
        }

        
        EquipmentData equipmentTool = toolSlot as EquipmentData;

         
        if (equipmentTool != null)
        {
            //Get the tool type
            EquipmentData.ToolType toolType = equipmentTool.toolType;

            switch (toolType)
            {
                case EquipmentData.ToolType.Hoe:
                    SwitchLandStatus(LandStatus.Farmland);
                    break;
                case EquipmentData.ToolType.Wateringcan:
                    //The land must be tilled first
                    if (landStatus != LandStatus.Soil)
                    {
                        SwitchLandStatus(LandStatus.Watered);
                    }

                    break;

                case EquipmentData.ToolType.Shovel:

                    //Remove the crop from the land
                    if (cropPlanted != null)
                    {
                        cropPlanted.RemoveCrop();
                    }
                    break;
            }

            
            return;
        }

        
        SeedData seedTool = toolSlot as SeedData;

        ///Conditions for the player to be able to plant a seed
        ///1: He is holding a tool of type SeedData
        ///2: The Land State must be either watered or farmland
        ///3. There isn't already a crop that has been planted
        if (seedTool != null && landStatus != LandStatus.Soil && cropPlanted == null)
        {
            SpawnCrop();
            //Plant it with the seed's information
            cropPlanted.Plant(id, seedTool);

            //Consume the item
            InventoryManager.Instance.ConsumeItem(InventoryManager.Instance.GetEquippedSlot(InventorySlot.InventoryType.Tool));

        }
    }

    public CropBehaviour SpawnCrop()
    {
        //Instantiate the crop object parented to the land
        GameObject cropObject = Instantiate(cropPrefab, transform);
        //Move the crop object to the top of the land gameobject
        cropObject.transform.position = new Vector3(transform.position.x, 1.2f, transform.position.z);

        //Access the CropBehaviour of the crop we're going to plant
        cropPlanted = cropObject.GetComponent<CropBehaviour>();
        return cropPlanted;
    }

    public void ClockUpdate(GameTimeStamp timestamp)
    {
        //Checked if 24 hours has passed since last watered
        if (landStatus == LandStatus.Watered)
        {
            //Hours since the land was watered
            int hoursElapsed = GameTimeStamp.CompareTimestamps(timeWatered, timestamp);
            Debug.Log(hoursElapsed + " hours since this was watered");

            //Grow the planted crop, if any
            if (cropPlanted != null)
            {
                cropPlanted.Grow();
            }

            if (hoursElapsed > 24)
            {
                //Dry up (Switch back to farmland)
                SwitchLandStatus(LandStatus.Farmland);
            }
        }

        //Handle the wilting of the plant when the land is not watered
        if (landStatus != LandStatus.Watered && cropPlanted != null)
        {
            //If the crop has already germinated, start the withering
            if (cropPlanted.cropState != CropBehaviour.CropState.Seed)
            {
                cropPlanted.Wither();
            }
        }
    }
}
