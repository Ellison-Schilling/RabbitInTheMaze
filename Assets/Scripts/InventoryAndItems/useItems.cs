using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class useItems : MonoBehaviour
{

    public bool hasCarrot;
    public PlayerMovement player;
    public InventoryItemController IIC; //or use InventoryManager.Instance.InventoryItems ?
    public GameObject[] inventoryObjects;

    void Update(){

        inventoryObjects = GameObject.FindGameObjectsWithTag("InvItem");

        if (Input.GetKeyDown("e")){

            hasCarrot = InventoryManager.Instance.loopThroughList("Carrot"); //set to true if carrot in inventory
            if (hasCarrot == true){
                player.max_speed += 0.05f;
                hasCarrot = false; //set to false until can check again}
            }

            foreach (GameObject obj in inventoryObjects){
                if (obj.transform.Find("ItemName").GetComponent<Text>().text == "Carrot"){
                    Destroy(obj);
                    break;
                }
            }

        }

        if (Input.GetKeyDown("t")){
            foreach (GameObject obj in inventoryObjects){
                Debug.Log(obj.transform.Find("ItemName").GetComponent<Text>().text);
            }
        }

    }
}
