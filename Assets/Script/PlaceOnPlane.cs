using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceOnPlane : MonoBehaviour
{
    public GameObject objectToPlace;
    private ARRaycastManager raycastManager;
    private GameObject spawnedObject;

    private void Start()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                List<ARRaycastHit> hits = new List<ARRaycastHit>();

                if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = hits[0].pose;

                    if (spawnedObject == null)
                    {
                        spawnedObject = Instantiate(objectToPlace, hitPose.position + new Vector3(0, 0.05f, 0), hitPose.rotation);

                        spawnedObject.transform.parent = null;
                    }
                    else
                    {
                        spawnedObject.transform.position = hitPose.position + new Vector3(0, 0.05f, 0);
                    }
                }
            }
        }
    }
}
