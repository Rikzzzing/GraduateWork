using UnityEngine;

public class MovementObjects : MonoBehaviour, IMovable
{
    [SerializeField] private GameObject _object;
    [SerializeField] private float _leftBorder;
    [SerializeField] private float _rightBorder;
    [SerializeField] private float _upBorder;
    [SerializeField] private float _downBorder;
    [SerializeField] private float _forwardBorder;
    [SerializeField] private float _backwardBorder;

    private Vector3 _startPosition;
    private Vector3 _currentPosition;


    private void Awake()
    {
        _startPosition = _object.transform.position;
        _currentPosition = _startPosition;
    }

    public void ManualInputMove(Vector3 inputDirection)
    {
        _currentPosition.x = (float)System.Math.Round(_currentPosition.x + inputDirection.x / 10, 1);
        if (_currentPosition.x > _leftBorder)
            _currentPosition.x = _rightBorder;
        else if (_currentPosition.x < _rightBorder)
            _currentPosition.x = _leftBorder;


        _currentPosition.y = (float)System.Math.Round(_currentPosition.y + inputDirection.y / 10, 1);
        if (_currentPosition.y < _downBorder)
            _currentPosition.y = _upBorder;
        else if (_currentPosition.y > _upBorder)
            _currentPosition.y = _downBorder;


        _currentPosition.z = (float)System.Math.Round(_currentPosition.z + inputDirection.z / 10, 1);
        if (_currentPosition.z < _backwardBorder)
            _currentPosition.z = _forwardBorder;
        else if (_currentPosition.z > _forwardBorder)
            _currentPosition.z = _backwardBorder;

        _object.transform.position = _currentPosition;
    }

    public void AutomaticInputMove(Vector3 inputDirection)
    {
        _currentPosition.x = (float)System.Math.Round(_currentPosition.x + inputDirection.x / 10, 1);
        if (_currentPosition.x < -0.9f)
            _currentPosition.x = -0.1f;
        else if (_currentPosition.x > -0.1f)
            _currentPosition.x = -0.9f;

        _object.transform.position = _currentPosition;
    }
}