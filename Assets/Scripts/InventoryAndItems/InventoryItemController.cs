using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemController : MonoBehaviour
{
    public Item item;

    public void RemoveItem(){
        InventoryManager.Instance.Remove(item);
        Destroy(gameObject);
        //change to remove at index
    }

    public void AddItem(Item newItem){
        item = newItem;
    }

}