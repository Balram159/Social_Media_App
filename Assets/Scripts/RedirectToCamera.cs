using UnityEngine;
using System.Runtime.InteropServices;

public class RedirectToCamera : MonoBehaviour
{
    private WebCamTexture webCamTexture;

    [DllImport("__Internal")]
    private static extern void _OpenCamera();

    //private void Start()
    //{
    //    OpenWebCam();
    //}
    public void OpenDeviceCamera()
    {
#if UNITY_ANDROID
        OpenAndroidCamera();
#elif UNITY_IOS
        OpenIOSCamera();
#elif UNITY_STANDALONE || UNITY_WEBGL
        OpenWebCam();
#else
        Debug.LogError("Camera can only be opened on an Android, iOS, or desktop platform.");
#endif
    }

    private void OpenAndroidCamera()
    {
        try
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject packageManager = currentActivity.Call<AndroidJavaObject>("getPackageManager");

            string cameraIntent = "android.media.action.IMAGE_CAPTURE";
            AndroidJavaObject intent = new AndroidJavaObject("android.content.Intent", cameraIntent);

            if (intent.Call<AndroidJavaObject>("resolveActivity", packageManager) != null)
            {
                currentActivity.Call("startActivity", intent);
            }
            else
            {
                Debug.LogError("No camera app found on this device.");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to open camera: " + e.Message);
        }
    }

    private void OpenIOSCamera()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            _OpenCamera();
        }
        else
        {
            Debug.LogError("Camera can only be opened on an iOS device.");
        }
    }

    public void OpenWebCam()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        string selectedDeviceName = null;

        // Look for the front camera
        foreach (WebCamDevice device in devices)
        {
            if (device.isFrontFacing)
            {
                selectedDeviceName = device.name;
                break;
            }
        }

        // If no front camera is found, use the first available camera
        if (selectedDeviceName == null && devices.Length > 0)
        {
            selectedDeviceName = devices[0].name;
        }

        if (selectedDeviceName != null)
        {
            if (webCamTexture != null && webCamTexture.isPlaying)
            {
                webCamTexture.Stop();
            }

            webCamTexture = new WebCamTexture(selectedDeviceName);
            Renderer renderer = GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.mainTexture = webCamTexture;
            }
            webCamTexture.Play();
        }
        else
        {
            Debug.LogError("No camera available.");
        }
    }

    private void OnDisable()
    {
        if (webCamTexture != null)
        {
            webCamTexture.Stop();
        }
    }
}
