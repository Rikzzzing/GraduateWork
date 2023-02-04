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
    private Vector3 _startScale;
    private Vector3 _currentPosition;
    private Vector3 _currentScale;

    private Quaternion _startRotation;
    private Vector3 _currentRotation;
    private Vector3 _angleRotation;

    private void Awake()
    {
        _startPosition = _parent.transform.position;
        _startScale = _parent.transform.localScale;
        _currentPosition = _startPosition;
        _currentScale = _startScale;

        _startRotation = _object.transform.rotation;
        _currentRotation = _startRotation.eulerAngles;
        _angleRotation.Set(15, 0, 0);
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
        AutomaticInputRotate(_angleRotation);
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
                    AutomaticInputRotate(_angleRotation);
                }
            }
        }

        //_parent.transform.position = _currentPosition;
    }

    private void AutomaticInputRotate(Vector3 angleRotation)
    {
        //_object.transform.rotation = Quaternion.Euler(_currentRotation + angleRotation).normalized;
        //_currentRotation = _object.transform.rotation.eulerAngles;
        _object.transform.Rotate(angleRotation);
        //Debug.Log(_object.transform.rotation.eulerAngles.x);
        Debug.Log(_object.transform.rotation.eulerAngles.normalized.x);
        if (_object.transform.rotation.eulerAngles.x == 360)
            Debug.Log("Yfitkcz");
            //_object.transform.rotation = _startRotation;
    }
}