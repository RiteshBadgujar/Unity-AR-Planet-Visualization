using UnityEngine;
using UnityEngine.UI;

public class CameraFeedController : MonoBehaviour
{
   
    public RawImage rawImage;
    private WebCamTexture webcamTexture;
    private string backCamName; 

    void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length == 0)
        {
            Debug.LogError("No cameras detected on device!");
            return;
        }

        
        for (int i = 0; i < devices.Length; i++)
        {
            if (devices[i].isFrontFacing == false)
            {
                backCamName = devices[i].name;
                break;
            }
        }

        if (string.IsNullOrEmpty(backCamName))
        {
            Debug.LogWarning("Back camera not found, using default camera.");
            backCamName = devices[0].name;
        }

        
        webcamTexture = new WebCamTexture(backCamName, 1280, 720, 30);

       
        rawImage.texture = webcamTexture;

        webcamTexture.Play();

        
        SetCameraRotationAndAspect();
    }

  
    void Update()
    {
        if (webcamTexture != null && webcamTexture.isPlaying)
        {
            SetCameraRotationAndAspect();
        }
    }

    void SetCameraRotationAndAspect()
    {
       
        float ratio = (float)webcamTexture.width / webcamTexture.height;
        
        rawImage.GetComponent<AspectRatioFitter>().aspectRatio = ratio;

     
        int orientation = -webcamTexture.videoRotationAngle;
        rawImage.rectTransform.localEulerAngles = new Vector3(0, 0, orientation);

        
        rawImage.uvRect = new Rect(0, 0, 1, 1);
        if (webcamTexture.videoVerticallyMirrored)
        {
            rawImage.uvRect = new Rect(0, 1, 1, -1);
        }
    }

    void OnDisable()
    {
      
        if (webcamTexture != null && webcamTexture.isPlaying)
        {
            webcamTexture.Stop();
        }
    }
}
