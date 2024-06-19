using UnityEngine;
using TigerForge;
using FrostweepGames.Plugins.WebGLFileBrowser.Examples;
using UnityEngine.UI;
using UnityEngine.Events;

public class UploadDataToServer : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider uploadSlider;
    public static bool videoUploadDone;
    public UnityEvent OnVideoUpload;
    void Start()
    {
        UniRESTClient.debugMode = true;
    }

    [ContextMenu("Download File")]
    public void DownloadFile()
    {
        var myDownloadInstance = new UniRESTClient.Download();
        myDownloadInstance.FromGame("Tfolder/img2.jpg", "");
        myDownloadInstance.Start();
        myDownloadInstance.onCompleteCallBack = OnDownloadFinished;
        myDownloadInstance.onErrorCallBack = OnDownloadError;
    }

    [ContextMenu("Upload Image File")]
    public void UploadImageFile()
    {
        var myUploadInstance = new UniRESTClient.Upload();
        Debug.Log("Image Path"+ImageFileBrower.imagefilePath);
        
        myUploadInstance.ToGame(ImageFileBrower.imagefilePath, "Tfolder");
        
        myUploadInstance.Start();
        myUploadInstance.onCompleteCallBack = OnImageUploadFinished;
        Debug.Log("Uploaded At: " + myUploadInstance.status.currentFileURL);
    }

    [ContextMenu("Upload Video File")]
    public void UploadVideoFile()
    {
        var myUploadInstance = new UniRESTClient.Upload();
        Debug.Log("Video Path" + VideoFileBrower.videofilePath);
        myUploadInstance.ToGame(VideoFileBrower.videofilePath, "Tfolder");
        myUploadInstance.Start();
        myUploadInstance.onProgessCallBack = OnUploadProgress;
        myUploadInstance.onCompleteCallBack = OnVideoUploadFinished;
    }

    void OnDownloadFinished(UniRESTClient.Download.Status s)
    {
        Debug.Log("Download Completed");
    }

    void OnDownloadError(UniRESTClient.Download.Status s)
    {
        Debug.Log("Download Error");
    }

    void OnImageUploadFinished(UniRESTClient.Upload.Status s)
    {
        Debug.Log("Image URL " + s.currentDestionationFile);
        AppManager.imgURL = s.currentDestionationFile;
        Debug.Log("Image Upload Finished");

    }
    void OnVideoUploadFinished(UniRESTClient.Upload.Status s)
    {
        OnVideoUpload.Invoke();
        videoUploadDone = true;
        Debug.Log("Video URL " + s.currentDestionationFile);
        AppManager.videoURL = s.currentDestionationFile;
        Debug.Log("Video Upload Finished");
    }

    void OnUploadProgress(UniRESTClient.Upload.Status s)
    {
        uploadSlider.value = s.totalProgress;
    }
}
