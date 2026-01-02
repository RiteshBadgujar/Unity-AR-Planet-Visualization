using UnityEngine;

public class ARObjectController : MonoBehaviour
{
    public float rotationSpeed = 0.2f;
    public float zoomSpeed = 0.01f;
    public float minScale = 0.3f;
    public float maxScale = 3f;

    void Update()
    {
        HandleRotation();
        HandleZoom();
    }

    // ---------------- ROTATION ----------------
    void HandleRotation()
    {
        // One finger rotate
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                float rotX = touch.deltaPosition.y * rotationSpeed;
                float rotY = -touch.deltaPosition.x * rotationSpeed;

                transform.Rotate(rotX, rotY, 0, Space.World);
            }
        }
    }

    // ---------------- ZOOM ----------------
    void HandleZoom()
    {
        // Two-finger pinch zoom (Scaler)
        if (Input.touchCount == 2)
        {
            Touch t1 = Input.GetTouch(0);
            Touch t2 = Input.GetTouch(1);

            // Previous distance between touches
            float prevDist = (t1.position - t1.deltaPosition - (t2.position - t2.deltaPosition)).magnitude;

            // Current distance
            float currentDist = (t1.position - t2.position).magnitude;

            // Difference
            float diff = currentDist - prevDist;

            // Scale object
            Vector3 newScale = transform.localScale + Vector3.one * (diff * zoomSpeed);
            newScale.x = Mathf.Clamp(newScale.x, minScale, maxScale);
            newScale.y = Mathf.Clamp(newScale.y, minScale, maxScale);
            newScale.z = Mathf.Clamp(newScale.z, minScale, maxScale);

            transform.localScale = newScale;
        }
    }
}
