using UnityEngine;

[CreateAssetMenu(menuName = "Item/Seed")]
public class SeedData : ItemData
{
    //Time it takes before the seeds matures into a crop
    public int DaysToGrow;

    //The crop the seed will yield 
    public ItemData cropToYield;

    //The seedling GameObject
    public GameObject seedling;
}
