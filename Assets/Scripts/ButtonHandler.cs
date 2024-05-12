using UnityEngine;
using UnityEngine.SceneManagement; // Required for SceneManager

public class TutorialButtonHandler : MonoBehaviour
{
    public string sceneName;
    public void StartGame()
    {
        // Load the tutorial scene
        SceneManager.LoadScene(sceneName);
    }
}
