using UnityEngine;

public class ScreenShot : MonoBehaviour, IDoScreenshot
{
    public void MakeScreenshot()
    {
        Debug.Log("Screenshot maked");
        ScreenCapture.CaptureScreenshot("Screenshot01.png", 1);
    }
}