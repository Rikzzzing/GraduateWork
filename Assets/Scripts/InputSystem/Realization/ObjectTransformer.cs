using UnityEngine;

public class ObjectTransformer : MonoBehaviour, ITransformable
{
    #region Parametrs in Inspector
    [Header("Object and Parent")]
    [SerializeField] private GameObject _object;
    [SerializeField] private GameObject _parent;
    [SerializeField] private Mesh[] _mesh;
    [SerializeField] private string[] _name;
    [Space(2)]

    [Header("Borders of movement")]
    [SerializeField] private float _leftBorder;
    [SerializeField] private float _rightBorder;
    [SerializeField] private float _upBorder;
    [SerializeField] private float _downBorder;
    [SerializeField] private float _forwardBorder;
    [SerializeField] private float _backwardBorder;
    [Space(2)]

    [Header("Parametrs of movement")]
    [SerializeField][Range(0.1f, 1.0f)] private float _stepScale;
    [SerializeField][Range(1, 60)] private byte _stepRotate;
    [SerializeField][Range(0.1f, 2.0f)] private float _dividerMove;
    #endregion

    private Vector3 _inputDirection;

    private Vector3 _startPosition;
    private Quaternion _startRotation;
    private Vector3 _startScale;

    private Vector3 _stepPosition;
    private Vector3 _angleRotation;
    private Vector3 _factorScale;

    private byte _bitCounterRotation;
    private byte _bitCounterScale;
    private byte _maxBitCounter;
    private byte _xBitMask;
    private byte _yBitMask;
    private byte _zBitMask;

    private byte _numberOfMesh;
    private int _transformIteration;

    private void Awake()
    {
        _inputDirection = Vector3.one;

        _startPosition = _parent.transform.position;
        _startRotation = _object.transform.rotation;
        _startScale = _parent.transform.localScale;

        _stepPosition = _startPosition;
        _angleRotation = Vector3.zero;
        _factorScale = Vector3.zero;

        _bitCounterRotation = 1;
        _bitCounterScale = 1;
        _maxBitCounter = 8;
        _xBitMask = 1;
        _yBitMask = 2;
        _zBitMask = 4;

        _numberOfMesh = 1;
        _transformIteration = 0;
    }

    public void ManualInputMove(Vector3 inputDirection)
    {
        _stepPosition.x = (float)System.Math.Round(_stepPosition.x + inputDirection.x / _dividerMove, 1);
        if (_stepPosition.x > _leftBorder)
            _stepPosition.x = _rightBorder;
        else if (_stepPosition.x < _rightBorder)
            _stepPosition.x = _leftBorder;


        _stepPosition.y = (float)System.Math.Round(_stepPosition.y + inputDirection.y / _dividerMove, 1);
        if (_stepPosition.y < _downBorder)
            _stepPosition.y = _upBorder;
        else if (_stepPosition.y > _upBorder)
            _stepPosition.y = _downBorder;


        _stepPosition.z = (float)System.Math.Round(_stepPosition.z + inputDirection.z / _dividerMove, 1);
        if (_stepPosition.z < _backwardBorder)
            _stepPosition.z = _forwardBorder;
        else if (_stepPosition.z > _forwardBorder)
            _stepPosition.z = _backwardBorder;

        _object.transform.position = _stepPosition;
        Debug.Log("Object moved (Manual Input)");
    }

    public void AutomaticMove()
    {
        _transformIteration++;
        _stepPosition.x = (float)System.Math.Round(_stepPosition.x + _inputDirection.x / _dividerMove, 1);

        if (_stepPosition.x > _leftBorder)
        {
            _stepPosition.x = _rightBorder;
            _stepPosition.z = (float)System.Math.Round(_stepPosition.z + _inputDirection.z / _dividerMove, 1);

            if (_stepPosition.z > _forwardBorder)
            {
                _stepPosition.z = _backwardBorder;
                _stepPosition.y = (float)System.Math.Round(_stepPosition.y + _inputDirection.y / _dividerMove, 1);

                if (_stepPosition.y > _upBorder)
                {
                    _stepPosition = _startPosition;
                    AutomaticRotate();

                    if (_bitCounterRotation >= _maxBitCounter)
                    {
                        AutomaticScale();
                        _bitCounterRotation = 1;

                        if (_bitCounterScale >= _maxBitCounter)
                        {
                            _bitCounterScale = 1;
                            _transformIteration = 0;
                            _object.GetComponentInChildren<MeshFilter>().mesh = _mesh[_numberOfMesh % _mesh.Length];
                            _object.GetComponentInChildren<MeshCollider>().sharedMesh = _mesh[_numberOfMesh % _mesh.Length];
                            _object.transform.GetChild(0).name = _name[_numberOfMesh++ % _name.Length];
                        }
                    }
                }
            }
        }

        _parent.transform.position = _stepPosition;
    }

    private void AutomaticRotate()
    {
        if ((_bitCounterRotation & _xBitMask) == _xBitMask)
            _angleRotation.x += _stepRotate;

        if ((_bitCounterRotation & _yBitMask) == _yBitMask)
            _angleRotation.y += _stepRotate;

        if ((_bitCounterRotation & _zBitMask) == _zBitMask)
            _angleRotation.z += _stepRotate;

        if (_angleRotation.x <= 360 && _angleRotation.y <= 360 && _angleRotation.z <= 360)
            _object.transform.rotation = _startRotation * Quaternion.Euler(_angleRotation);
        else
        {
            _angleRotation = Vector3.zero;
            _bitCounterRotation++;
        }
    }

    private void AutomaticScale()
    {
        if ((_bitCounterScale & _xBitMask) == _xBitMask)
            _factorScale.x = (float)System.Math.Round(_factorScale.x + _stepScale, 1);

        if ((_bitCounterScale & _yBitMask) == _yBitMask)
            _factorScale.y = (float)System.Math.Round(_factorScale.y + _stepScale, 1);

        if ((_bitCounterScale & _zBitMask) == _zBitMask)
            _factorScale.z = (float)System.Math.Round(_factorScale.z + _stepScale, 1);

        if (_factorScale.x <= 5 && _factorScale.y <= 5 && _factorScale.z <= 5)
            _parent.transform.localScale = _startScale + _factorScale;
        else
        {
            _parent.transform.localScale = _startScale;
            _factorScale = Vector3.zero;
            _bitCounterScale++;
        }
    }

    public Vector3 GetCurrentModelPosition()
    {
        return gameObject.GetComponentInChildren<Renderer>().bounds.min;
    }

    public Vector3 GetModelSize()
    {
        return gameObject.GetComponentInChildren<Renderer>().bounds.size;
    }

    public string GetModelName()
    {
        return gameObject.transform.GetChild(0).name;
    }

    public int GetTransformIteration()
    {
        return _transformIteration;
    }
}