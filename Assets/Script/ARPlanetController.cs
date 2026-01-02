using UnityEngine;

public class ARPlanetController : MonoBehaviour
{
    [Header("Camera Follow Settings")]
    public Transform cameraTransform;
    public float distance = 1.0f;
    public Vector3 offset;
    public float followSmooth = 10f;

    [Header("Touch Controls")]
    public float dragSpeed = 0.001f;
    public float rotateSpeed = 0.2f;
    public float pinchSpeed = 0.01f;
    public float minScale = 0.3f;
    public float maxScale = 3f;

    private Vector3 desiredPosition;

    void Start()
    {
        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;
    }

    void LateUpdate()
    {
        FollowCamera();
        HandleTouchControls();
    }

    // ---------------- Camera Follow ----------------
    void FollowCamera()
    {
        Vector3 forward = cameraTransform.forward.normalized;

        desiredPosition = cameraTransform.position + forward * distance;

        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * followSmooth);
    }

    // ---------------- Touch Controls ----------------
    void HandleTouchControls()
    {
        // Drag or Rotate with Single Finger
        if (Input.touchCount == 1)
        {
            Touch t = Input.GetTouch(0);

            if (t.phase == TouchPhase.Moved)
            {
                Vector2 delta = t.deltaPosition;

                // Rotate
                transform.Rotate(Vector3.up, -delta.x * rotateSpeed, Space.World);
                transform.Rotate(Vector3.right, delta.y * rotateSpeed, Space.World);
            }
        }

        // Pinch Zoom
        else if (Input.touchCount == 2)
        {
            Touch t0 = Input.GetTouch(0);
            Touch t1 = Input.GetTouch(1);

            Vector2 p0Prev = t0.position - t0.deltaPosition;
            Vector2 p1Prev = t1.position - t1.deltaPosition;

            float prevDist = (p0Prev - p1Prev).magnitude;
            float currDist = (t0.position - t1.position).magnitude;

            float delta = currDist - prevDist;

            float scaleFactor = 1 + delta * pinchSpeed;

            Vector3 newScale = transform.localScale * scaleFactor;

            float clamped = Mathf.Clamp(newScale.x, minScale, maxScale);

            transform.localScale = new Vector3(clamped, clamped, clamped);
        }
    }
}
