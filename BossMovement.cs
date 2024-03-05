using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public float horizontalSpeed = 5f;
    public float verticalSpeed = 2f;
    public float changeDirectionInterval = 5f;
    public float maxHeight = 10f;
    public float minHeight = 0f;

    private bool goingUp = false;
    private float nextDirectionChangeTime;
    private float originalY;

    void Start()
    {
        // Set initial direction change time
        nextDirectionChangeTime = Time.time + Random.Range(0f, changeDirectionInterval);
        originalY = transform.position.y;
    }

    void Update()
    {
        // Move horizontally
        float movementX = Mathf.Sin(Time.time * horizontalSpeed) * Time.deltaTime;
        transform.Translate(Vector3.right * movementX);

        // Check if it's time to change direction
        if (Time.time >= nextDirectionChangeTime)
        {
            // Change direction
            goingUp = !goingUp;

            // Update next direction change time
            nextDirectionChangeTime = Time.time + changeDirectionInterval;
        }

        // Move vertically if going up
        if (goingUp)
        {
            float movementY = verticalSpeed * Time.deltaTime;
            transform.Translate(Vector3.up * movementY);

            // Clamp position to avoid going beyond maxHeight
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, minHeight, maxHeight), transform.position.z);
        }
        else
        {
            // Move back to original height position
            float movementY = (originalY - transform.position.y) * 0.5f * Time.deltaTime;
            transform.Translate(Vector3.up * movementY);
        }
    }
}
