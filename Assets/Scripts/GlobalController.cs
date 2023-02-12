using System;
using UnityEngine;

public class GlobalController : MonoBehaviour
{
    private IScreenshotable _screenshotable;
    private IRaycastable _raycastableFront;
    private IRaycastable _raycastableSide;
    private IRaycastable _raycastableBottom;
    private IMovable _movable;

    private Vector3 _startPosition;
    private Vector3 _area;

    private void Awake()
    {
        _screenshotable = GameObject.Find("ScreenshotCamera").GetComponent<IScreenshotable>();
        if (_screenshotable == null)
            throw new Exception($"There is no IScreenshotable on the object: {gameObject.name}");

        _raycastableFront = GameObject.Find("Front").GetComponent<IRaycastable>();
        if (_raycastableFront == null)
            throw new Exception($"There is no IRaycastable on the object: {gameObject.name}");

        _raycastableSide = GameObject.Find("Side").GetComponent<IRaycastable>();
        if (_raycastableSide == null)
            throw new Exception($"There is no IRaycastable on the object: {gameObject.name}");

        _raycastableBottom = GameObject.Find("Bottom").GetComponent<IRaycastable>();
        if (_raycastableBottom == null)
            throw new Exception($"There is no IRaycastable on the object: {gameObject.name}");

        _movable = GameObject.Find("Model").GetComponent<IMovable>();
        if (_movable == null)
            throw new Exception($"There is no IMovable on the object: {gameObject.name}");
    }

    private void Update()
    {
        _movable.AutomaticMove();

        _startPosition = _movable.GetCurrentModelPosition();
        _area = _movable.GetModelSize();

        //_screenshotable.AutomaticScreenshot();

        _raycastableBottom.AutomaticRaycast(_startPosition, _area);
        _raycastableFront.AutomaticRaycast(_startPosition, _area);
        _raycastableSide.AutomaticRaycast(_startPosition, _area);
    }
}