using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUsage : MonoBehaviour
{

    //private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update(){
        if (Input.GetAxis("Eat") == 1){
            //Debug.Log(player.giveMaxSpeed());
        }
    }
}
