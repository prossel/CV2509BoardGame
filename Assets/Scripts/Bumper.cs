using UnityEngine;

public class Bumper : MonoBehaviour
{

    public float forceMagnitude = 5f; // adjust this value to change the strength of the force


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // detect collision with the ball
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Ball>() != null)
        {
            Debug.Log("Bumper hit the ball!");

            // Add a force to the ball in the direction of the bumper'y axis
            Rigidbody ballRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 forceDirection = transform.up; // y axis of the bumper
            ballRigidbody.AddForce(forceDirection * forceMagnitude, ForceMode.Impulse);

            // Play sound effect (if any)
            AudioSource audioSource = GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.Play();
            }

        }

    }

}