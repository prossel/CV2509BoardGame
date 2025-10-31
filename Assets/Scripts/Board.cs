using UnityEngine;
using UnityEngine.InputSystem;

public class Board : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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

    // Update is called once per frame
    void Update()
    {
        if (Pointer.current != null)
        {
            // Vector2 pos = Pointer.current.position.ReadValue();
            // Debug.Log("Pointer position: " + pos);


            // rotate the board around the x axis when the mouse is moved up and down
            // and around the z axis when the mouse is moved left and right
            float rotationSpeed = 0.1f;
            float rotationX = Pointer.current.delta.y.ReadValue() * rotationSpeed;
            float rotationZ = -Pointer.current.delta.x.ReadValue() * rotationSpeed;
            transform.Rotate(rotationX, 0, rotationZ, Space.World);

        }
    }

}
