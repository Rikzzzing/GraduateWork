using UnityEngine;

public class MovementObjects : MonoBehaviour, IMovable
{
    [SerializeField] private GameObject _object;
    [SerializeField] private GameObject _parent;
    [SerializeField] private float _leftBorder;
    [SerializeField] private float _rightBorder;
    [SerializeField] private float _upBorder;
    [SerializeField] private float _downBorder;
    [SerializeField] private float _forwardBorder;
    [SerializeField] private float _backwardBorder;

    private Vector3 _startPosition;
    private Quaternion _startRotation;
    private Vector3 _startScale;

    private Vector3 _currentPosition;
    private Vector3 _angleRotation;
    private Vector3 _currentScale;

    private byte _bitCounterRotation;
    private byte _bitCounterScale;
    private byte _xBitMask;
    private byte _yBitMask;
    private byte _zBitMask;

    private void Awake()
    {
        _startPosition = _parent.transform.position;
        _startRotation = _object.transform.rotation;
        _startScale = _parent.transform.localScale;

        _currentPosition = _startPosition;
        _angleRotation.Set(0, 0, 0);
        _currentScale = _startScale;

        _bitCounterRotation = 1;
        _bitCounterScale = 1;
        _xBitMask = 1;
        _yBitMask = 2;
        _zBitMask = 4;
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
        AutomaticInputRotate();
        _currentPosition.x = (float)System.Math.Round(_currentPosition.x + inputDirection.x / 10, 1);

        if (_currentPosition.x < _rightBorder)
        {
            _currentPosition.x = _leftBorder;
            _currentPosition.z = (float)System.Math.Round(_currentPosition.z + inputDirection.z / 10, 1);

            if (_currentPosition.z > _forwardBorder)
            {
                _currentPosition.z = _backwardBorder;
                _currentPosition.y = (float)System.Math.Round(_currentPosition.y + inputDirection.y / 10, 1);

                if (_currentPosition.y > _upBorder)
                {
                    _currentPosition = _startPosition;
                    AutomaticInputRotate();

                    if (_bitCounterRotation >= 8)
                    {
                        AutomaticInputScale();
                        _bitCounterRotation = 1;
                    }
                }
            }
        }

        //_parent.transform.position = _currentPosition;
    }

    private void AutomaticInputRotate()
    {
        var _prevBitCounter = _bitCounterRotation;

        if ((_bitCounterRotation & _xBitMask) == _xBitMask)
        {
            _angleRotation.x += 15;

            if (_angleRotation.x <= 360)
            {
                //Debug.Log("X");
                _object.transform.rotation = _startRotation * Quaternion.Euler(_angleRotation);
            }
            else
            {
                _angleRotation.x = 0;
                _bitCounterRotation++;
            }
        }

        if ((_bitCounterRotation & _yBitMask) == _yBitMask)
        {
            _angleRotation.y += 15;

            if (_angleRotation.y <= 360)
            {
                //Debug.Log("Y");
                _object.transform.rotation = _startRotation * Quaternion.Euler(_angleRotation);
            }
            else if (_bitCounterRotation - _prevBitCounter == 0)
            {
                _bitCounterRotation++;
                _angleRotation.y = 0;
            }
            else
                _angleRotation.y = 0;
        }

        if ((_bitCounterRotation & _zBitMask) == _zBitMask)
        {
            _angleRotation.z += 15;

            if (_angleRotation.z <= 360)
            {
                //Debug.Log("Z");
                _object.transform.rotation = _startRotation * Quaternion.Euler(_angleRotation);
            }
            else if (_bitCounterRotation - _prevBitCounter == 0)
            {
                _bitCounterRotation++;
                _angleRotation.z = 0;
            }
            else
                _angleRotation.z = 0;
        }
    }

    private void AutomaticInputScale()
    {

    }
}