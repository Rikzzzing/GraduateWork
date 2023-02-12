using System;
using UnityEngine;

public class AutomaticScreenShot : MonoBehaviour
{
    [SerializeField][Range(0.01f, 1.00f)] private float _timeIteration;

    private IScreenshotable _screenshotable;

    private void Awake()
    {
        _screenshotable = GetComponent<IScreenshotable>();

        if (_screenshotable == null)
        {
            throw new Exception($"There is no IScreenshotable on the object: {gameObject.name}");
        }
    }

    private void Start()
    {
        InvokeRepeating("AutoScreenhot", _timeIteration, _timeIteration);
    }

    public void AutoScreenhot()
    {
        _screenshotable.AutomaticScreenshot();
    }
}