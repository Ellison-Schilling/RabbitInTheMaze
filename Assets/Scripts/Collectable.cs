using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{

    public AudioSource collect_noise;
    public Item item;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float rotationSpeed = 50f;
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);
    }

    void Pickup(){
        if(InventoryManager.Instance.itemsInInventory < InventoryManager.Instance.capacity){
            //We only want to add to the inventory if it isn't full
            InventoryManager.Instance.Add(item);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //collect_noise.Play();
            Pickup();
        }
    }

}
