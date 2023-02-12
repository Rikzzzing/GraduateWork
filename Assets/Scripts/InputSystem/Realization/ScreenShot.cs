using System;
using UnityEngine;

public class ScreenShot : MonoBehaviour, IScreenshotable
{
    public void ManualInputScreenshot()
    {
        Debug.Log("Screenshot maked");
        ScreenCapture.CaptureScreenshot("DatasetScreenshots/Screenshot01.png", 1);
    }

    public void AutomaticScreenshot()
    {
        Debug.Log("Screenshot maked");
        ScreenCapture.CaptureScreenshot($"DatasetScreenshots/Screenshot{DateTime.Now.Second}.png", 1);
    }
}