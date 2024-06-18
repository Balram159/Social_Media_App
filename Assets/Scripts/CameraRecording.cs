using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System.IO;
using UnityEngine.UI;

public class CameraRecorder : MonoBehaviour
{
    public CameraController cameraController; // Reference to the CameraController
    public Button startButton;
    public Button stopButton;

    private bool isRecording = false;
    private string ffmpegPath = "C://Users//Andy//Downloads//ffmpeg//bin//ffmpeg.exe"; // Path to the FFmpeg executable
    private string outputPath;
    private int frameRate = 30;
    private int frameCount = 0;

    void Start()
    {
        startButton.onClick.AddListener(StartRecording);
        stopButton.onClick.AddListener(StopRecording);
        outputPath = Path.Combine(Application.persistentDataPath, "output.mp4");
    }

    void StartRecording()
    {
        isRecording = true;
        frameCount = 0;
        StartCoroutine(CaptureFrames());
    }

    void StopRecording()
    {
        isRecording = false;
        StopCoroutine(CaptureFrames());
        EncodeVideo();
    }

    IEnumerator CaptureFrames()
    {
        while (isRecording)
        {
            yield return new WaitForEndOfFrame();

            if (cameraController.webCamTexture != null && cameraController.webCamTexture.isPlaying)
            {
                Texture2D texture = new Texture2D(cameraController.webCamTexture.width, cameraController.webCamTexture.height);
                texture.SetPixels(cameraController.webCamTexture.GetPixels());
                texture.Apply();

                byte[] bytes = texture.EncodeToJPG();
                File.WriteAllBytes(Path.Combine(Application.persistentDataPath, $"frame_{frameCount}.jpg"), bytes);
                frameCount++;

                Destroy(texture);
            }
        }
    }

    void EncodeVideo()
    {
        Process ffmpeg = new Process();
        ffmpeg.StartInfo.FileName = ffmpegPath;
        ffmpeg.StartInfo.Arguments = $"-framerate {frameRate} -i {Application.persistentDataPath}/frame_%d.jpg -c:v libx264 -pix_fmt yuv420p {outputPath}";
        ffmpeg.StartInfo.UseShellExecute = false;
        ffmpeg.StartInfo.RedirectStandardOutput = true;
        ffmpeg.StartInfo.RedirectStandardError = true;
        ffmpeg.StartInfo.CreateNoWindow = true;
        ffmpeg.Start();
        ffmpeg.WaitForExit();
        CleanUpFrames();
    }

    void CleanUpFrames()
    {
        for (int i = 0; i < frameCount; i++)
        {
            string filePath = Path.Combine(Application.persistentDataPath, $"frame_{i}.jpg");
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
