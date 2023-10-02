using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameButton : MonoBehaviour
{

    public void StartGame()
    {
        
        // Load the scene with the name 'game'
        SceneManager.LoadScene("game");
    }
}
