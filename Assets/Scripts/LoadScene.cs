using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{  
    public void LoadNextScene()
    {
        // Load next scene or the first scene if we are at the last scene
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(nextSceneIndex);
    }
    
    // Load a scene by its name
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
