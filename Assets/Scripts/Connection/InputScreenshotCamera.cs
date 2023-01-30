using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputScreenshotCamera : MonoBehaviour
{
    private InputDoc _inputDoc;
    private IDoScreenshot _screenShot;

    private void Awake()
    {
        if (_inputDoc == null)
        {
            _inputDoc = new InputDoc();
        }

        _screenShot = GetComponent<IDoScreenshot>();

        if (_screenShot == null)
        {
            throw new Exception($"There is no IJumpable on the object: {gameObject.name}");
        }
    }

    private void OnEnable()
    {
        _inputDoc.Enable();
        _inputDoc.Management.Screenshot.performed += OnScreenshotPerfermed;
    }

    private void OnScreenshotPerfermed(InputAction.CallbackContext obj)
    {
        _screenShot.MakeScreenshot();
    }

    private void OnDisable()
    {
        _inputDoc.Management.Screenshot.performed -= OnScreenshotPerfermed;
        _inputDoc.Disable();
    }
}