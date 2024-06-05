using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.SceneManagement; // Required for SceneManager


public class TutorialButtonHandler : MonoBehaviour
{
    public string sceneName;
    public async void StartGame()
    {
        // Load the given scene
        await Task.Delay(500);
        SceneManager.LoadScene(sceneName);
    }
}
