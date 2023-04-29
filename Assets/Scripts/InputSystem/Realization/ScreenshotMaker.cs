using System;
using UnityEngine;

public class ScreenshotMaker : MonoBehaviour, IScreenshotable
{
    public void ManualInputScreenshot()
    {
        Debug.Log("Screenshot was taken (Manual Input)\n");
        ScreenCapture.CaptureScreenshot($"Dataset/x_train/ManualScreenshot {DateTime.Now.ToString("MM/dd/yyyy HH/mm/ss")}.png", 1);
    }

    public void AutomaticScreenshot()
    {
        Debug.Log("Screenshot was taken (Automatic Input without parametrs)\n");
        ScreenCapture.CaptureScreenshot($"Dataset/x_train/AutoScreenshot {DateTime.Now.ToString("MM/dd/yyyy HH/mm/ss")}.png", 1);
    }
    public void AutomaticScreenshot(string name, int number)
    {
        Debug.Log($"Screenshot was taken (Automatic Input with parametrs: {name} {number})\n");
        ScreenCapture.CaptureScreenshot($"Dataset/x_train/{name}_{number}_X.png");
    }
}