using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Item")]
public class ItemData : ScriptableObject
{
    public string description;

    //icon to be displated in ui
    public Sprite thumbnail;

    //Gameobject to be shown in the scene
    public GameObject gameModel;





}
