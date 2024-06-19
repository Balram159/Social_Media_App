using FrostweepGames.Plugins.WebGLFileBrowser;
using UnityEngine;
using UnityEngine.UI;

public class ImageFileBrower : MonoBehaviour
{
    public Button openFileDialogButton;
    public File[] _loadedFiles;
    public static string imagefilePath;
    public static string videofilePath;
    // Start is called before the first frame update
    void Start()
    {
        openFileDialogButton.onClick.AddListener(OpenFileDialogButtonOnClickHandler);

        WebGLFileBrowser.FilesWereOpenedEvent += FilesWereOpenedEventHandler;
        WebGLFileBrowser.FolderOpenFailedEvent -= FileOpenFailedEventHandler;
    }

    private void OpenFileDialogButtonOnClickHandler()
    {
        WebGLFileBrowser.SetLocalization(LocalizationKey.DESCRIPTION_TEXT, "Select file to load or use drag & drop");

        // you could paste types like: ".png,.jpg,.pdf,.txt,.json"
        // WebGLFileBrowser.OpenFilePanelWithFilters(".png,.jpg,.pdf,.txt,.json");
        WebGLFileBrowser.OpenFilePanelWithFilters(WebGLFileBrowser.GetFilteredFileExtensions(".jpg"));
    }

    private void OnDestroy()
    {
        WebGLFileBrowser.FilesWereOpenedEvent -= FilesWereOpenedEventHandler;
        WebGLFileBrowser.FolderOpenFailedEvent -= FileOpenFailedEventHandler;
    }

    private void FilesWereOpenedEventHandler(File[] files)
    {
        _loadedFiles = files;
        
        if (_loadedFiles != null && _loadedFiles.Length > 0)
        {
            var file = _loadedFiles[0];

            //foreach (var loadedFile in _loadedFiles)
            //{
            //    filePath = loadedFile.fileInfo.path;
            //    Debug.Log(loadedFile.fileInfo.path);
            //}

            if (_loadedFiles.Length == 1)
            {
                if (file.IsImage())
                {
                    imagefilePath = file.fileInfo.path;
                    Debug.Log(file.fileInfo.path);
                }
                else
                {
                    videofilePath = file.fileInfo.path;
                    Debug.Log(file.fileInfo.path);
                }
            }
                
        }
    }

    private void FileOpenFailedEventHandler(string error)
    {
        Debug.Log(error);
    }
}
