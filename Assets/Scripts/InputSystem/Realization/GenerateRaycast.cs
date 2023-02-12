using System;
using UnityEngine;

public class GenerateRaycast : MonoBehaviour, IRaycastable
{
    #region Parametrs in Inspector
    [SerializeField][Range(1, 100)] private int _countRay;
    [SerializeField] private LayerMask _maskRay;
    [SerializeField][ColorUsage(false)] private Color _color;
    #endregion

    private Vector3 _startPosition;
    private Vector3 _currentPosition;
    private Vector3 _directionRay;
    private Vector3 _OffsetAxis1;
    private Vector3 _OffsetAxis2;

    private string _planeType;

    private void Awake()
    {
        var size = GetComponent<Collider>().bounds.size;
        _planeType = gameObject.name;

        switch (_planeType)
        {
            case "Front":
                _directionRay = Vector3.forward;
                break;
            case "Side":
                _directionRay = Vector3.left;
                break;
            case "Bottom":
                _directionRay = Vector3.up;
                break;
        }
    }

    public void ManualInputRaycast()
    {

    }

    public void AutomaticRaycast(Vector3 startPosition, Vector3 area)
    {
        CalculateOffset(startPosition, area);

        for (int i = 0; i <= _countRay; i++)
        {
            for (int j = 0; j <= _countRay; j++)
            {
                Debug.DrawRay(_currentPosition, _directionRay*5, _color);
                _currentPosition = _currentPosition + _OffsetAxis1;
            }
            _currentPosition = _startPosition + _OffsetAxis2 * (i + 1);
        }
    }

    private void CalculateOffset(Vector3 startPosition, Vector3 area)
    {
        switch (_planeType)
        {
            case "Front":
                _startPosition.Set(startPosition.x, startPosition.y, 0);
                _OffsetAxis1.Set(area.x / _countRay, 0, 0);
                _OffsetAxis2.Set(0, area.y / _countRay, 0);
                break;
            case "Side":
                _startPosition.Set(0, startPosition.y, startPosition.z);
                _OffsetAxis1.Set(0, area.y / _countRay, 0);
                _OffsetAxis2.Set(0, 0, area.z / _countRay);
                break;
            case "Bottom":
                _startPosition.Set(startPosition.x, 0, startPosition.z);
                _OffsetAxis1.Set(area.x / _countRay, 0, 0);
                _OffsetAxis2.Set(0, 0, area.z / _countRay);
                break;
        }

        _currentPosition = _startPosition;
    }
}