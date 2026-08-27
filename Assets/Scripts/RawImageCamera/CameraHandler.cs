using UnityEngine;
using UnityEngine.UI;

public class CameraHandler : MonoBehaviour
{

    [SerializeField] private RawImage cameraPreview;

    private WebCamTexture webCamTexture;

    void Start()
    {
        StartCamera();
    }

    private void StartCamera()
    {
        if (WebCamTexture.devices.Length == 0)
        {
            Debug.LogWarning("No devices found");
            return;
        }
        WebCamDevice cameraDevice = WebCamTexture.devices[0];
        webCamTexture = new WebCamTexture(cameraDevice.name);
        cameraPreview.texture = webCamTexture;

        webCamTexture.Play();
    }

    private void OnDisable()
    {
        if(webCamTexture != null && webCamTexture.isPlaying)
        {
            webCamTexture.Stop();
        }
    }
}

