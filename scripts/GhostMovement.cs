using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    public float speed = 1f; // Adjust speed as needed
    public float horizontalRange = 0.2f; // Adjust horizontal range as needed

    private Vector3 startingPosition;
    private bool movingRight = true;

    private void Awake()
    {
        startingPosition = transform.position;
    }

    private void Update()
    {
        // Move the ghost horizontally in a back-and-forth motion
        float movement = speed * Time.deltaTime;

        if (movingRight)
        {
            transform.Translate(Vector3.right * movement);
        }
        else
        {
            transform.Translate(Vector3.left * movement);
        }

        // Check if the ghost has reached the limit of its horizontal range
        if (Mathf.Abs(transform.position.x - startingPosition.x) >= horizontalRange)
        {
            // Change direction
            movingRight = !movingRight;
        }
    }

}