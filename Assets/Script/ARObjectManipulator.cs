using UnityEngine;

public class ARObjectManipulator : MonoBehaviour
{
    public float rotateSpeed = 0.2f;
    public float zoomSpeed = 0.005f;
    public float minScale = 0.3f;
    public float maxScale = 2.5f;

    private float currentXRotation = 0f;
    private float verticalLimit = 60f;

    void Update()
    {
        // ------------------ ROTATION (One finger) ------------------
        if (Input.touchCount == 1)
        {
            Touch t = Input.GetTouch(0);

            if (t.phase == TouchPhase.Moved)
            {
                Vector2 delta = t.deltaPosition;

                // Yaw
                transform.Rotate(Vector3.up, -delta.x * rotateSpeed, Space.World);

                // Pitch (Clamped)
                float xRot = delta.y * rotateSpeed;
                currentXRotation += xRot;
                currentXRotation = Mathf.Clamp(currentXRotation, -verticalLimit, verticalLimit);

                Vector3 yaw = transform.rotation.eulerAngles;
                transform.rotation = Quaternion.Euler(-currentXRotation, yaw.y, 0f);
            }
        }

        // ------------------ PINCH ZOOM (Two fingers) ------------------
        if (Input.touchCount == 2)
        {
            Touch t0 = Input.GetTouch(0);
            Touch t1 = Input.GetTouch(1);

            float prevDist = (t0.position - t0.deltaPosition - (t1.position - t1.deltaPosition)).magnitude;
            float currDist = (t0.position - t1.position).magnitude;
            float diff = currDist - prevDist;

            float scaleFactor = 1 + diff * zoomSpeed;

            Vector3 newScale = transform.localScale * scaleFactor;

            float s = Mathf.Clamp(newScale.x, minScale, maxScale);
            transform.localScale = new Vector3(s, s, s);
        }
    }
}
