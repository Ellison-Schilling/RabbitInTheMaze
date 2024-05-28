using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;
    public AudioSource collect_noise;

    void Pickup(){
        OldInventoryManager.Instance.Add(item);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player")){
            collect_noise.Play();
            Pickup();
        }
    }

}
