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
    private CharacterController characterController;

    void Update(){

        inventoryObjects = GameObject.FindGameObjectsWithTag("InvItem");

        if (Input.GetKeyDown("e")){

            hasCarrot = InventoryManager.Instance.loopThroughList("[E] Carrot"); //set to true if carrot in inventory
            if (hasCarrot == true){
                player.maxSpeed += 0.02f;
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
                if (player.stamina + 15f > player.maxStamina){
                    player.stamina = player.maxStamina;
                }
                else {
                    player.stamina += 15f;
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
                player.maxSpeed += 0.05f;
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
                    useKB();//IMPLEMENT!
                    break;
                }
            }
        }

        if (Input.GetKeyDown("t")){
            foreach (GameObject obj in inventoryObjects){
                if (obj.transform.Find("ItemName").GetComponent<Text>().text == "[T] Teleporter"){
                    Destroy(obj);
                    teleport();//IMPLEMENT!
                    break;
                }
            }
        }

    }

    void Start()
    {
        // Get the Rigidbody component
        characterController = GetComponent<CharacterController>();
    }

    public void useKB(){
        Debug.Log("Pow!");
    }

    public float teleportDistance = 200f;
    public LayerMask layerMask; //used with raycasts, things in the mask can be "hit" by them

    public void teleport(){

        Debug.Log("Zoop!");

        Vector3 destination = transform.position + transform.forward * teleportDistance;
        Debug.Log(transform.position);
        Debug.Log(transform.forward);
        Debug.Log(teleportDistance);
        Debug.Log(destination);

        if (Physics.Raycast(transform.position, transform.forward, teleportDistance, layerMask)){
            Debug.Log("Destination obstructed!");
        }
        else{
            //if nothing in way, can teleport
            characterController.Move(destination);
            Debug.Log(transform.position);
        }

    }

}
