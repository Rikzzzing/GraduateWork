using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class RaycastGenerator : MonoBehaviour, IRaycastable
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

    private RaycastHit _hit;

    private List<Vector3> _vertexes;

    private string _planeType;

    private void Awake()
    {
        _planeType = gameObject.name;

        switch (_planeType)
        {
            case "Front":
                _directionRay = Vector3.forward;
                break;
            case "Side":
                _directionRay = Vector3.right;
                break;
            case "Bottom":
                _directionRay = Vector3.up;
                break;
        }

        _vertexes = new List<Vector3>();
    }

    public void ManualInputRaycast()
    {

    }

    public List<Vector3> AutomaticRaycast(Vector3 startPosition, Vector3 area)
    {
        _vertexes.Clear();

        CalculateOffset(startPosition, area);

        for (int i = 0; i <= _countRay; i++)
        {
            for (int j = 0; j <= _countRay; j++)
            {
                Physics.Raycast(_currentPosition, _directionRay, out _hit, 20);
                Debug.DrawRay(_currentPosition, _directionRay * 20, _color, 10); // 60 seconds
                //Debug.Log(Physics.Raycast(_ray, 5));
                Debug.Log($"{_planeType}. vertex: " + _hit.point);
                _vertexes.Add(_hit.point);
                _currentPosition += _OffsetAxis1;
            }
            _currentPosition = _startPosition + _OffsetAxis2 * (i + 1);
        }

        Debug.Log("==========================================");
        return _vertexes;
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