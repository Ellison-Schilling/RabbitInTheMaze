using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerCollisions : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject GameStateManager;
    private PlayerInventory playerInventroy;
    private GameStateManager gameStateManager;
    private GameObject gate_swing_right;
    private GameObject gate_swing_left;

    void Start()
    {
        playerInventroy = Player.GetComponent<PlayerInventory>();
        gameStateManager = GameStateManager.GetComponent<GameStateManager>();
        gate_swing_left = GameObject.FindWithTag("RightGate");
        gate_swing_right = GameObject.FindWithTag("LeftGate");
    }

    private void OnTriggerEnter(Collider other)
    {
        switch(other.tag)
        {
            case "exit":
                if(playerInventroy.GetItemCount("key") > 0 && gameStateManager.GetState() == 0)
                    gameStateManager.SetGameState("win");
                break;
            case "enemy":
                if(gameStateManager.GetState() == 0)
                    gameStateManager.SetGameState("lose");
                break;
            case "Carrot":
                playerInventroy.AddItem("carrot");
                Destroy(other.transform.parent.gameObject);
                break;
            case "GoldenCarrot":
                playerInventroy.AddItem("goldenCarrot");
                Destroy(other.transform.parent.gameObject);
                break;
            case "Cabbage":
                playerInventroy.AddItem("cabbage");
                Destroy(other.transform.parent.gameObject);
                break;
            case "Key":
                playerInventroy.AddItem("key");
                gate_swing_left.transform.Rotate(0,-60,0);
                gate_swing_right.transform.Rotate(0,60,0);
                Destroy(other.gameObject);
                break;
            default:
                Debug.Log("Wonder what this does?");
                return;
        }
    }
}
