using UnityEngine;
using UnityEngine.InputSystem;

public class Board : MonoBehaviour
{

    public float rotationSpeed = 0.05f;

    private Rigidbody rb;
    private Vector2 accumulatedDelta;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        // Lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    void OnDisable()
    {
        // Unlock the cursor
        Cursor.lockState = CursorLockMode.None;
    }

    void Update()
    {
        // Accumulate pointer delta each frame (render rate)
        if (Pointer.current != null)
        {
            accumulatedDelta += Pointer.current.delta.ReadValue();
        }
    }

    void FixedUpdate()
    {
        if (rb != null)
        {
            // Consume and reset accumulated input in the physics step
            Vector2 delta = accumulatedDelta;
            accumulatedDelta = Vector2.zero;

            // rotate the board around the x axis when the mouse is moved up and down
            // and around the z axis when the mouse is moved left and right
            float rotationX = delta.y * rotationSpeed;
            float rotationZ = -delta.x * rotationSpeed;

            // Apply rotation using Rigidbody for proper physics integration
            if (rotationX != 0f || rotationZ != 0f)
            {
                Quaternion deltaRotation = Quaternion.Euler(rotationX, 0, rotationZ);
                rb.MoveRotation(rb.rotation * deltaRotation);
            }

        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // get the collider that was hit on this object
        Collider hitCollider = collision.contacts[0].thisCollider;

        // if the collider's gameobject is not the board itself, forward the collision to that gameobject
        if (hitCollider.gameObject != this.gameObject)
        {
            // forward the collision to the other gameobject
            hitCollider.gameObject.SendMessage("OnCollisionEnter", collision, SendMessageOptions.DontRequireReceiver);
        }
    }

}
