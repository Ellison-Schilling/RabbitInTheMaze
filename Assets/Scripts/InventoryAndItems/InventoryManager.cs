using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> Items = new List<Item>();
    public int itemsInInventory = 0;
    public int capacity = 7;

    public Transform ItemContent;
    public GameObject InventoryItem;

    public InventoryItemController[] InventoryItems;

    private void Awake(){
        Instance = this;
    }

    public void Add(Item item){
        Items.Add(item);
        itemsInInventory++;
        GameObject obj = Instantiate(InventoryItem, ItemContent);
        var itemName = obj.transform.Find("ItemName").GetComponent<Text>();
        var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
        string tag = GameObject.Find("ItemName").GetComponent<Text>().text;
        itemName.text = item.itemName;
        itemIcon.sprite = item.icon;
        obj.tag = "InvItem";
    }

    public void Remove(Item item){
        Items.Remove(item);
        itemsInInventory--;
    }

    public void RemoveAtIndex(int index){
        Items.RemoveAt(index);
        itemsInInventory--;
    }

    public bool loopThroughList(string itemID){
        int index = 0;
        foreach(Item item in Items){
            if (item.itemName == itemID){
                RemoveAtIndex(index);
                return true;
            }
            index++;
        }
        return false;
    }

    public void setInventoryItems(){

        InventoryItems = ItemContent.GetComponentsInChildren<InventoryItemController>();

        for(int i = 0; i < Items.Count; i++){
            InventoryItems[i].AddItem(Items[i]);
        }

    }

    void Update(){
        //setInventoryItems(); 
    }

}
