using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameStateManager : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Timer;
    private PlayerController player;
    private Timer timer;
    [SerializeField] private GameObject winText, winPanel, deathPanel;
    [SerializeField] private AudioSource gameWon, gameLost;
    private int gameState;

    void Start()
    {
        winText.SetActive(false);
        winPanel.SetActive(false);
        deathPanel.SetActive(false);
        gameState = 0;
        player = Player.GetComponent<PlayerController>();
        timer = Timer.GetComponent<Timer>();
    }

    public void SetGameState(string state)
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

    private void Win()
    {
        Debug.Log("You completed the maze!");
        gameWon.Play();
        winText.SetActive(true);
        winPanel.SetActive(true);
        StartCoroutine(TimeTitleScreen(5.0f));
        gameState = 1;
    }

    private void Lose()
    {
        Debug.Log("Womp womp, you died...");
        gameLost.Play();
        player.Dies();
        deathPanel.SetActive(true);
        StartCoroutine(TimeTitleScreen(3.0f));
        gameState = -1;
    }

    public int GetState()
    {
        return gameState;
    }

    IEnumerator TimeTitleScreen(float timer)
    {
        yield return new WaitForSeconds(timer);
        SceneManager.LoadScene("TitleScreen");
    }

}
