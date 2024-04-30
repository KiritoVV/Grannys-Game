using UnityEngine;

public class CropBehvaviour : MonoBehaviour
{

    //Information on what the crop will grow into
    SeedData seedToGrow;

    [Header("Stages of Life")]
    public GameObject seed;
    public GameObject wilted;
    private GameObject seedling;
    private GameObject harvestable;

    //The growth points of the crop
    int growth;
    //How many growth points it takes before it becomes harvestable
    int maxGrowth;

    //The crop will stay alive without water for 48 hours
    int maxHealth = GameTimeStamp.HoursToMinutes(48);
    int health;
    public enum CropState
    {
        Seed, Seedling, Harvestable, Wilted
    }
    //The current stage in the crops growth
    public CropState cropState;

    //Initialisation for the crop GameObject
    //Called when the player plants a seed
    public void Plant(SeedData seedToGrow)
    {
        //Save the seed information
        this.seedToGrow = seedToGrow;

        //Instantiate the seedling and harvestable GameObjects
        seedling = Instantiate(seedToGrow.seedling, transform);

        //Access the crop item data
        ItemData cropToYield = seedToGrow.cropToYield;

        //Instantiate the harvestable crop
        harvestable = Instantiate(cropToYield.gameModel, transform);

        //Convert days to grow into hours
        int hoursToGrow = GameTimeStamp.DaysToHours(seedToGrow.DaysToGrow);

        maxGrowth = GameTimeStamp.HoursToMinutes(hoursToGrow);


        //Check if it is regrowable
        if (seedToGrow.regrowable)
        {
            //Get the RegrowableHarvest from the gameObject
            RegrowableHarvest  regrowableHarvest = harvestable.GetComponent<RegrowableHarvest>();

            //Initialise the harvestable
            regrowableHarvest.SetParent(this);
        }

        //Set the inital state to seed
        SwitchState(CropState.Seed);
    }

    //The crop will grow when watered
    public void Grow()
    {
        //Increase the growth point by 1
        growth++;

        //Restore the health of the plant when watered
        if(health  < maxHealth)
        {
            health++;
        }

        //The seed will sprout into a seedling when the growth is at 50%
        if(growth >= maxGrowth / 2 && cropState == CropState.Seed)
        {
            SwitchState(CropState.Seedling);
        }

        //Fully grown
        if (growth >= maxGrowth && cropState == CropState.Seedling)
        {
            SwitchState(CropState.Harvestable);
        }
    }

    void SwitchState(CropState stateToSwitch)
    {
        //Reset everything and set all GameObjects to inactive
        seed.SetActive(false);
        seedling.SetActive(false);
        harvestable.SetActive(false);
        wilted.SetActive(false);

        switch (stateToSwitch)
        {
            case CropState.Seed:
                //Enable the seed GameObject
                seed.SetActive(true);
                break;
            case CropState.Seedling:
                //Enable the seedling GameObject
                seedling.SetActive(true);

                //Give the  seed health 
                health = maxHealth;

                break;
            case CropState.Harvestable:
                //Enable the harvestable GameObject
                harvestable.SetActive(true);

                //If the seed is not regrowable, detach the harvestable from this crop gameObject and destroy it
                if(!seedToGrow.regrowable)
                {
                    //Unparent it to the crop
                    harvestable.transform.parent = null;
                    Destroy(gameObject);
                }
                
                break;
            case CropState.Wilted:
                //Enable  the wilted  GameObject
                wilted.SetActive(true);
                break;
        }

        //Set the current crop state to the state we are switching to 
        cropState = stateToSwitch;
    }

    public void Wither()
    {
        health--;
        //If the  health is below 0  and the crop  has germinated, kill it
        if(health <= 0 && cropState != CropState.Seed)
        {
            SwitchState(CropState.Wilted);
        }
    }

    public void Regrow()
    {
        //Reset the growth
        //Get the regrowth time in hours
        int hoursToReGrow = GameTimeStamp.DaysToHours(seedToGrow.daysToRegrow);
        growth = maxGrowth - GameTimeStamp.HoursToMinutes(hoursToReGrow);

        //Switch the state back to seedling
        SwitchState(CropState.Seedling);

    }

}

