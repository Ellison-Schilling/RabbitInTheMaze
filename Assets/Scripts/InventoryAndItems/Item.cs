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

public enum itemType{Carrot, Powerup, Tool}
//carrots do something tbd
//powerups alter the behavior of the player
//tools are items that are used to affect enemies
