using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]

public class Item : ScriptableObject
{
    
    public string itemName;
    public itemType type;
    public Sprite icon;

}

public enum itemType{Carrot, Tool, Powerup}
//carrots do something tbd
//tools are items that are used to affect enemies
//powerups alter the behavior of the player
