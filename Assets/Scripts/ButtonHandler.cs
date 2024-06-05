using UnityEngine;
using UnityEngine.SceneManagement; // Required for SceneManager

public class TutorialButtonHandler : MonoBehaviour
{
    public string sceneName;

    public void StartGame()
    {
        // Load the given scene
        PlayerPrefs.SetInt(sceneName, 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene(sceneName);
    }
}