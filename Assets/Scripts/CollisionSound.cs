using UnityEngine;

public class CollisionSound : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Detect collision with another object
    private void OnCollisionEnter(Collision collision)
    {
        // Debug.Log("sound Collision detected with " + collision.gameObject.name, this);

        // Check if the object has an AudioSource component
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            // Play the sound with a volume based on the collision relative velocity
            audioSource.volume = Mathf.Clamp01(collision.relativeVelocity.magnitude / 10f);
            audioSource.Play();
        }
    }        
}
