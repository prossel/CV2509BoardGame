using UnityEngine;
using UnityEngine.SceneManagement;

/* 
    This script supersedes the Target tag based behavior in the Ball script.
    Don't use the Target tag on game objects with this script attached.
*/
public class Target : MonoBehaviour
{

    // When hit, we may choose different options:
    // - Load the next level
    // - Enable another target
    // - Disable this target
    // - Play a sound

    public bool disableOnHit = false;
    public Target targetToEnableOnHit;
    public bool loadNextLevelOnHit = true;

    // Target states (disabled, enabled, hit)
    public enum TargetState
    {
        Disabled,
        Enabled,
        Hit
    }

    public TargetState currentState = TargetState.Enabled;

    // Target change materials based on state
    public Material disabledMaterial;
    public Material enabledMaterial;
    public Material hitMaterial;

    public AudioClip hitSoundWhenEnabled;
    public AudioClip hitSoundWhenDisabled;
    public AudioClip hitSoundWhenHit;


    private void OnCollisionEnter(Collision collision)
    {
        // Check if hit by a ball
        if (collision.gameObject.GetComponent<Ball>() != null)
        {
            Debug.Log("Target hit!");

            // Play hit sound based on current state
            AudioClip clipToPlay = null;
            switch (currentState)
            {
                case TargetState.Enabled:
                    clipToPlay = hitSoundWhenEnabled;
                    break;
                case TargetState.Disabled:
                    clipToPlay = hitSoundWhenDisabled;
                    break;
                case TargetState.Hit:
                    clipToPlay = hitSoundWhenHit;
                    break;
            }

            if (clipToPlay != null)
            {
                AudioSource.PlayClipAtPoint(clipToPlay, transform.position);
            }

            if (currentState == TargetState.Enabled)
            {
                currentState = TargetState.Hit;

                // Disable this target
                if (disableOnHit)
                {
                    currentState = TargetState.Disabled;

                    // Change material to disabled material
                    if (disabledMaterial != null)
                    {
                        GetComponent<Renderer>().material = disabledMaterial;
                    }
                }
                else
                {
                    // Change material to hit material
                    if (hitMaterial != null)
                    {
                        GetComponent<Renderer>().material = hitMaterial;
                    }
                }

                // Enable another target
                if (targetToEnableOnHit != null)
                {
                    targetToEnableOnHit.gameObject.SetActive(true);
                    targetToEnableOnHit.currentState = TargetState.Enabled;

                    // Change material to enabled material
                    if (enabledMaterial != null)
                    {
                        targetToEnableOnHit.GetComponent<Renderer>().material = enabledMaterial;
                    }
                }

                // Load next level
                if (loadNextLevelOnHit)
                {
                    // Load next scene in 2 seconds (so that the sound can play)
                    Invoke("LoadNextScene", 2f);
                }
            }
        }
    }

    private void LoadNextScene()
    {
        // Load next scene or the first scene if we are at the last scene
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(nextSceneIndex);
    }

}
