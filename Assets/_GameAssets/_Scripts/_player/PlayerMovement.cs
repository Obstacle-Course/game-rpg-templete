using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the player movement

    void Update()
    {
        // Read input for movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical).normalized * moveSpeed * Time.deltaTime;

        // Move the player
        transform.Translate(movement, Space.World);

        // Rotate the player based on movement direction
        //if (movement.magnitude > 0)
        //{
        //    transform.rotation = Quaternion.LookRotation(movement);
        //}
    }
}
