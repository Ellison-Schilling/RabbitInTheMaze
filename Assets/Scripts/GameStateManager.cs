using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameStateManager : MonoBehaviour
{
    [SerializeField] private GameObject winText, winPanel, deathPanel;
    [SerializeField] private AudioSource gameWon, gameLost;
    private int gameState;

    void Start()
    {
        winText.SetActive(false);
        winPanel.SetActive(false);
        deathPanel.SetActive(false);
        gameState = 0;
    }

    void SetGameState(string state)
    {
        switch (state)
        {
            case "Win":
            case "win":
            case "Won":
            case "won":
            case "Winner":
            case "winner":
                Win();
                break;
            case "Lose":
            case "lose":
            case "Loss":
            case "loss":
            case "Lost":
            case "lost":
            case "Loser":
            case "loser":
                Lose();
                break;
            default:
                Debug.Log("What just happened?!");
                return;
        }
    }

    void Win()
    {
        gameState = 1;
        Debug.Log("You completed the maze!");
        gameWon.Play();
        winText.SetActive(true);
        winPanel.SetActive(true);
        toTitleScreen(5.0f);
    }

    void Lose()
    {
        gameState = -1;
        Debug.Log("Womp womp, you died...");
        gameLost.Play();
        //animator.SetBool("Is_Dead", true);
        deathPanel.SetActive(true);
        toTitleScreen(3.0f);
    }

    public int GetState()
    {
        return gameState;
    }

    void toTitleScreen(float seconds)
    {
        float timer = 0;
        while (timer < seconds)
            timer += Time.deltaTime;
        SceneManager.LoadScene("TitleScreen");
    }

}
