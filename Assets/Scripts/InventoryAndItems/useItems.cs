using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class useItems : MonoBehaviour
{

    public bool hasCarrot;
    public bool hasLettuce;
    public bool hasGoldenCarrot;
    public PlayerController player;
    public GameObject[] inventoryObjects;

    void Update(){

        inventoryObjects = GameObject.FindGameObjectsWithTag("InvItem");

        if (Input.GetKeyDown("e")){

            hasCarrot = InventoryManager.Instance.loopThroughList("[E] Carrot"); //set to true if carrot in inventory
            if (hasCarrot == true){
                player.maxSpeed += 0.01f;
                hasCarrot = false; //set to false until can check again}
            }

            foreach (GameObject obj in inventoryObjects){
                if (obj.transform.Find("ItemName").GetComponent<Text>().text == "[E] Carrot"){
                    Destroy(obj);
                    break;
                }
            }

        }

        if (Input.GetKeyDown("f")){

            hasLettuce = InventoryManager.Instance.loopThroughList("[F] Lettuce"); 
            if (hasLettuce == true){
                if (player.stamina + 33f > player.maxStamina){
                    player.stamina = player.maxStamina;
                }
                else {
                    player.stamina += 33f;
                }
                hasLettuce = false; //set to false until can check again}
            }

            foreach (GameObject obj in inventoryObjects){
                if (obj.transform.Find("ItemName").GetComponent<Text>().text == "[F] Lettuce"){
                    Destroy(obj);
                    break;
                }
            }

        }

        if (Input.GetKeyDown("g")){

            hasGoldenCarrot = InventoryManager.Instance.loopThroughList("[G] Golden Carrot"); 
            if (hasGoldenCarrot == true){
                player.maxSpeed += 0.02f;
                hasGoldenCarrot = false; //set to false until can check again}
            }

            foreach (GameObject obj in inventoryObjects){
                if (obj.transform.Find("ItemName").GetComponent<Text>().text == "[G] Golden Carrot"){
                    Destroy(obj);
                    break;
                }
            }

        }

        if (Input.GetKeyDown("r")){
            foreach (GameObject obj in inventoryObjects){
                if (obj.transform.Find("ItemName").GetComponent<Text>().text == "[R] Knockback"){
                    Destroy(obj);
                    usePusher();//IMPLEMENT!
                    break;
                }
            }
        }

    }

    public void usePusher(){

    }

}
