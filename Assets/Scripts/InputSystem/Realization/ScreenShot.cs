using UnityEngine;

public class ScreenShot : MonoBehaviour, IScreenshotable
{
    public void Screenshot()
    {
        Debug.Log("Screenshot maked");
        ScreenCapture.CaptureScreenshot("Screenshot01.png", 1);
    }
}