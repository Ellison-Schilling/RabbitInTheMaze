using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Score2 : MonoBehaviour
{
    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("high_score2", 999999);
        PlayerPrefs.Save();
    }

    // Update is called once per frame
    void Update()
    {
        int title = PlayerPrefs.GetInt("titleScreen", 0);
        int scene = PlayerPrefs.GetInt("Level2", 0);
        if (title == 1 && scene == 1)
        {
            UpdateScore();
        }
    }

    private void UpdateScore()
    {
        Debug.Log("updating score2!");
        PlayerPrefs.SetInt("titleScreen", 0);
        PlayerPrefs.SetInt("Level2", 0);
        int new_score = PlayerPrefs.GetInt("score", 0);
        Debug.Log("new score is " + new_score);
        int high_score = PlayerPrefs.GetInt("high_score2", 0);
        Debug.Log("high score is " + high_score);
        if (new_score < high_score)
        {
            Debug.Log("new high score2!!");
            PlayerPrefs.SetInt("high_score2", new_score);
            text.text = new_score.ToString();
        }
        PlayerPrefs.Save();
    }
}
