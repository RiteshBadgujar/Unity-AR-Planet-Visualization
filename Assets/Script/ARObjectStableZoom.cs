using UnityEngine;

public class ARObjectStableZoom : MonoBehaviour
{
    public Camera arCamera;
    public float distanceFromCamera = 1.2f;

    public float zoomSpeed = 0.01f;
    public float minScale = 0.15f;
    public float maxScale = 1.5f;

    void Start()
    {
        if (arCamera == null)
            arCamera = Camera.main;
    }

    void LateUpdate()
    {
        transform.position = arCamera.transform.position +
                             arCamera.transform.forward * distanceFromCamera;
    }

    void Update()
    {
        if (Input.touchCount == 2)
        {
            Touch t0 = Input.GetTouch(0);
            Touch t1 = Input.GetTouch(1);

            Vector2 p0 = t0.position - t0.deltaPosition;
            Vector2 p1 = t1.position - t1.deltaPosition;

            float prevDist = Vector2.Distance(p0, p1);
            float currDist = Vector2.Distance(t0.position, t1.position);

            float diff = currDist - prevDist;

            float scale = transform.localScale.x + diff * zoomSpeed;
            scale = Mathf.Clamp(scale, minScale, maxScale);

            transform.localScale = Vector3.one * scale;
        }
    }
}
