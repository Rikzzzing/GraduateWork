using System;
using UnityEngine;

public class GenerateRaycast : MonoBehaviour
{
    [SerializeField][Range(1, 100)] private int _countRay;
    [SerializeField] private Vector3 _directionRay;
    [SerializeField] private LayerMask _maskRay;
    [SerializeField] private float _distanceRay;
    [SerializeField] private string _type;

    private Vector3 _startPosition;
    private Vector3 _currentPosition;
    private Vector3 _xOffset;
    private Vector3 _yOffset;
    private Vector3 _zOffset;
    private Vector3 _size;
    private Ray _ray;

    private void Awake()
    {
        _startPosition = GetComponent<Transform>().position;
        _currentPosition = _startPosition;
        _size = GetComponent<Collider>().bounds.size;

        if (_size.x != 0)
            _xOffset.Set(-_size.x / _countRay, 0, 0);

        if (_size.y != 0)
            _yOffset.Set(0, _size.y / _countRay, 0);

        if (_size.z != 0)
            _zOffset.Set(0, 0, _size.z / _countRay);

        _ray = new Ray(_startPosition, _directionRay);
    }

    private void FixedUpdate()
    {
        CheckType(_type);
    }

    private void CheckType(string type)
    {
        switch (type)
        {
            case "Front":
                GenerateFrontRaycast(_xOffset, _yOffset);
                break;
            case "Side":
                GenerateSideRaycast(_yOffset, _zOffset);
                break;
            case "Bottom":
                GenerateBottomRaycast(_xOffset, _zOffset);
                break;
            default:
                break;
        }
        _currentPosition = _startPosition;
    }

    private void GenerateFrontRaycast(Vector3 xOffset, Vector3 yOffset)
    {
        for (int i = 0; i <= _countRay; i++)
        {
            for (int j = 0; j <= _countRay; j++)
            {
                Debug.DrawRay(_currentPosition, _directionRay * 50, Color.blue);
                _currentPosition = _currentPosition + xOffset;
            }
            _currentPosition = _startPosition + yOffset * (i + 1);
        }
    }

    private void GenerateSideRaycast(Vector3 yOffset, Vector3 zOffset)
    {
        for (int i = 0; i <= _countRay; i++)
        {
            for (int j = 0; j <= _countRay; j++)
            {
                Debug.DrawRay(_currentPosition, _directionRay * 50, Color.red);
                _currentPosition = _currentPosition + zOffset;
            }
            _currentPosition = _startPosition + yOffset * (i + 1);
        }
    }

    private void GenerateBottomRaycast(Vector3 xOffset, Vector3 zOffset)
    {
        for (int i = 0; i <= _countRay; i++)
        {
            for (int j = 0; j <= _countRay; j++)
            {
                Debug.DrawRay(_currentPosition, _directionRay * 50, Color.green);
                _currentPosition = _currentPosition + xOffset;
            }
            _currentPosition = _startPosition + zOffset * (i + 1);
        }
    }
}