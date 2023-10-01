using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameButton : MonoBehaviour
{
    public string nextSceneName; // Name of the next scene to load

    public void StartGame()
    {
        // Check if the scene name is provided
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            // Load the specified scene
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            // Load the next scene (assuming the game scene is the next one)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
