using UnityEngine;

public class MovementObjects : MonoBehaviour, IMovable
{
    [SerializeField] GameObject _object;

    private Vector3 _startPosition;
    private Vector3 _currentPosition;


    private void Awake()
    {
        _startPosition = _object.transform.position;
        _currentPosition = _startPosition;
    }

    public void Move(Vector3 inputDirection)
    {
        Debug.Log("I moved");

        if (_currentPosition.x > -1 & _currentPosition.x < 0)
        {
            _currentPosition.x = (float)System.Math.Round(_currentPosition.x + inputDirection.x / 10, 1);
        }
        else if (_currentPosition.x <= -1)
        {
            _currentPosition.x = -0.1f;
        }
        else if (_currentPosition.x >= 0)
        {
            _currentPosition.x = -0.9f;
        }

        if (_currentPosition.y < 1 & _currentPosition.y > 0)
        {
            _currentPosition.y = (float)System.Math.Round(_currentPosition.y + inputDirection.y / 10, 1);
        }
        else if (_currentPosition.y <= 0)
        {
            _currentPosition.y = 0.9f;
        }
        else if (_currentPosition.y >= 1)
        {
            _currentPosition.y = 0.1f;
        }

        if (_currentPosition.z < 1 & _currentPosition.z > 0)
        {
            _currentPosition.z = (float)System.Math.Round(_currentPosition.z + inputDirection.z / 10, 1);
        }
        else if (_currentPosition.z <= 0)
        {
            _currentPosition.z = 0.9f;
        }
        else if (_currentPosition.z >= 1)
        {
            _currentPosition.z = 0.1f;
        }

        _object.transform.position = _currentPosition;
    }
}