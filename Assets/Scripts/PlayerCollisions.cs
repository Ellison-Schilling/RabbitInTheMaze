using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerCollisions : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        switch(other.tag)
        {
            case "exit":
                // if inventroy has key and state is 0, state to 1
                break;
            case "enemy":
                // if state is 0, state to -1
                break;
            case "collectable":
                // destroy and add to inventroy
                break;
            default:
                Debug.Log("Wonder what this does?");
                return;
        }
    }
}
