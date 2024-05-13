using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> Items = new List<Item>();
    public int itemsInList = 0;

    private void Awake(){
        Instance = this;
    }

    public void Add(Item item){
        Items.Add(item);
        itemsInList++;
    }

    public void Remove(Item item){
        Items.Remove(item);
        itemsInList--;
    }

}
