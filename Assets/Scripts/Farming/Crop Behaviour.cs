using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropBehvaviour : MonoBehaviour
{

    //Information on what the crop will grow into
    SeedData seedToGrow;

    [Header("Stages of Life")]
    public GameObject seed;
    private GameObject seedling;
    private GameObject harvestable;

    //The growth points of the crop
    int growth;
    //How many growth points it takes before it becomes harvestable
    int maxGrowth;
    public enum CropState
    {
        Seed, Seedling, Harvestable
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

        //Set the inital state to seed
        SwitchState(CropState.Seed);
    }

    public void Grow()
    {
        //Increase the growth point by 1
        growth++;

        //Fully grown
        if (growth >= maxGrowth)
        {
            SwitchState(CropState.Harvestable);
        }
    }

    void SwitchState (CropState stateToSwitch)
    {
        //Reset everything and set all GameObjects to inactive
        seed.SetActive(false);
        seedling.SetActive(false);
        harvestable.SetActive(false);

        switch (stateToSwitch)
        {
            case CropState.Seed:
                //Enable the seed GameObject
                seed.SetActive(true);
                break;
            case CropState.Seedling:
                //Enable the seedling GameObject
                seedling.SetActive(true);
                break;
            case CropState.Harvestable:
                //Enable the harvestable GameObject
                harvestable.SetActive(true);
                break;
        }
    }
}

