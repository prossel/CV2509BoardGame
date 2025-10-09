using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("racine: " + Mathf.Sqrt(4));
    }

    // detect collision with the target
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            Debug.Log("Ball hit the target!");

            // Load next scene in 2 seconds (so that the sound can play)
            Invoke("LoadNextScene", 2f);
        }

        if (collision.gameObject.CompareTag("BallLoss"))
        {
            Debug.Log("Ball is lost");

            // Reload current scene in 2 seconds (so that the sound can play)
            Invoke("ReloadScene", 2f);
        }
    }

    private void ReloadScene()
    {
        // Load next scene or the first scene if we are at the last scene
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;       
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void LoadNextScene()
    {
        // Load next scene or the first scene if we are at the last scene
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(nextSceneIndex);
    }
}
