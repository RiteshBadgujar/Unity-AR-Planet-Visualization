using UnityEngine;

public class RotationController : MonoBehaviour
{
    public GameObject PlanetObject;
    public float rotationSpeed = 50f;

    void Update()
    {
        PlanetObject.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
