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

    [Header("Crops")]
    //The crop prefab to instantiate
    public GameObject cropPrefab;

    //The crop currently planted on the land
    CropBehvaviour cropPlanted = null;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        // Set the soil material to default
        SwitchLandStatus(LandStatus.Soil);
        //Deselect the land by default
        Select(false);


        //Add this to timeManager Listeners list
        TimeManager.Instance.Registertracker(this);
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

        if (toolSlot == null)
        {
            return;
        }

        EquipmentData equipmentTool = toolSlot as EquipmentData;

        //Check if it is of type equipment
        if (equipmentTool != null)
        {
            //get the tool type
            EquipmentData.ToolType toolType = equipmentTool.toolType;

            switch (toolType)
            {
                case EquipmentData.ToolType.Hoe:
                    SwitchLandStatus(LandStatus.Farmland);
                    break;
                case EquipmentData.ToolType.Wateringcan:
                    SwitchLandStatus(LandStatus.Watered);
                    break;
            }
            return;
        }
        //Try casting the itemdata in the toolslot as SeedData
        SeedData seedTool = toolSlot as SeedData;

        ///Conditions for the player to be able to plant a seed
        ///1: he is holding a tool of type SeedData
        ///2: The land State must be either watered or farmland
        ///3: There isnt already a crop that has been planted
        if (seedTool != null && landStatus != LandStatus.Soil && cropPlanted == null)
        {
            GameObject cropObject = Instantiate(cropPrefab, transform);
            //Move the crop obje t to the top of the land gameObject
            cropObject.transform.position = new Vector3(transform.position.x, 1.2f, transform.position.z);

            //Access the CropBehaviour of our newly planted crop
            cropPlanted = cropObject.GetComponent<CropBehvaviour>();
            //Plant it
            cropPlanted.Plant(seedTool);
        }

    }

    public void ClockUpdate(GameTimeStamp timestamp)
    {
        //Check if 24 hours has passed since last watered
        if (landStatus == LandStatus.Watered)
        {
            //Hours since the land was watered
            int hoursElapsed = GameTimeStamp.CompareTimestamps(timeWatered, timestamp);
            Debug.Log(hoursElapsed + " hours since this was watered");

            //Grow the planted crop
            if (cropPlanted != null)
            {
                cropPlanted.Grow();
            }

            if (hoursElapsed > 24)
            {
                SwitchLandStatus(LandStatus.Farmland);
            }

        }
        if(landStatus == LandStatus.Watered && cropPlanted != null)
        {
            //If the crop has already germinated, start the withering
            if(cropPlanted.cropState != CropBehvaviour.CropState.Seed)
            {
                cropPlanted.Wither();
            }
        }


    }

}
