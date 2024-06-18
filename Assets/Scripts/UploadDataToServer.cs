using UnityEngine;
using TigerForge;

public class UploadDataToServer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UniRESTClient.debugMode = true;
        //_ = UniRESTClient.Async.ApplicationLogin((result) =>
        //{

        //});
        UserLogin();
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

    [ContextMenu("Upload File")]
    public void UploadFile()
    {
        var myUploadInstance = new UniRESTClient.Upload();
        myUploadInstance.ToGame("Users\\aanandkumar\\Downloads\\Social_Media_App-main\\Assets\\img2.jpg", "Tfolder");
        myUploadInstance.Start();
        myUploadInstance.onCompleteCallBack = OnUploadFinished;
        Debug.Log("Uploaded At: " + myUploadInstance.status.currentFileURL);
    }

    void UserLogin()
    {
        _ = UniRESTClient.Async.Login("test7188", "pass1447", (bool ok) =>
        {
            if (ok) Debug.Log(UniRESTClient.userAccount.username + " LOGGED IN!"); else Debug.Log("ERROR: " + UniRESTClient.ServerError);
        });
    }

    void OnDownloadFinished(UniRESTClient.Download.Status s)
    {
        Debug.Log("Download Completed");
    }

    void OnDownloadError(UniRESTClient.Download.Status s)
    {
        Debug.Log("Download Error");
    }

    void OnUploadFinished(UniRESTClient.Upload.Status s)
    {
        Debug.Log("Upload Finished");

    }
}
