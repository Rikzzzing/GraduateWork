public interface IScreenshotable
{
    void ManualInputScreenshot();
    void AutomaticScreenshot();
    void AutomaticScreenshot(string _name, int number);
}