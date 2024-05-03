using UnityEngine;

[CreateAssetMenu(menuName = "Item/Equipment")]
public class EquipmentData : ItemData
{
    public enum ToolType
    {
        Hoe, Wateringcan, Axe, Pickaxe, Shovel
    }

    public ToolType toolType;
}
