using UnityEngine;
using UnityEngine.InputSystem;

public class Board : MonoBehaviour
{
    private Rigidbody rb;

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

    void FixedUpdate()
    {
        if (Pointer.current != null && rb != null)
        {
            // Vector2 pos = Pointer.current.position.ReadValue();
            // Debug.Log("Pointer position: " + pos);


            // rotate the board around the x axis when the mouse is moved up and down
            // and around the z axis when the mouse is moved left and right
            float rotationSpeed = 0.05f;
            float rotationX = Pointer.current.delta.y.ReadValue() * rotationSpeed;
            float rotationZ = -Pointer.current.delta.x.ReadValue() * rotationSpeed;

            // Apply rotation using Rigidbody for proper physics integration
            Quaternion deltaRotation = Quaternion.Euler(rotationX, 0, rotationZ);
            rb.MoveRotation(rb.rotation * deltaRotation);

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
