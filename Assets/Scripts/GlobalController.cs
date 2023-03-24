using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class GlobalController : MonoBehaviour
{
    [SerializeField][Range(2, 10)] private byte _iterationTime;
    private IScreenshotable _screenshotable;
    private IRaycastable _raycastableFront;
    private IRaycastable _raycastableSide;
    private IRaycastable _raycastableBottom;
    private ITransformable _transformable;

    private Vector3 _startPosition;
    private Vector3 _area;
    private string _name;

    private List<Vector3> _vertexes;

    private StreamWriter _file;

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

        _transformable = GameObject.Find("Model").GetComponent<ITransformable>();
        if (_transformable == null)
            throw new Exception($"There is no IMovable on the object: {gameObject.name}");
    }

    private void Start()
    {
        //StartCoroutine(AllOperations());
        //AllOperations();
        //InvokeRepeating("AllOperations", _iterationTime, _iterationTime);
        InvokeRepeating("StartAllOperation", _iterationTime, _iterationTime);
    }

    private void StartAllOperation()
    {
        StartCoroutine(AllOperations());
    }

    IEnumerator AllOperations()
    {
        Debug.Log("Started Coroutine at timestamp : " + Time.time);
        Debug.Log("==========================================");

        _transformable.AutomaticMove();
        _startPosition = _transformable.GetCurrentModelPosition();
        _area = _transformable.GetModelSize();
        _name = _transformable.GetModelName();

        Debug.Log("_startPosition: " + _startPosition.ToString("F5"));
        Debug.Log("_area: " + _area.ToString("F5"));
        Debug.Log("_name: " + _name);
        Debug.Log("==========================================");

        yield return new WaitForSeconds(_iterationTime / 2);

        _screenshotable.AutomaticScreenshot(_name, _transformable.GetTransformIteration());

        _vertexes = _raycastableBottom.AutomaticRaycast(_startPosition, _area);
        _vertexes.AddRange(_raycastableFront.AutomaticRaycast(_startPosition, _area));
        _vertexes.AddRange(_raycastableSide.AutomaticRaycast(_startPosition, _area));
        //_vertexes = _vertexes.Union(_vertexes).ToList();

        Debug.Log("==========================================");
        Debug.Log("We have a list of vertexes:");
        foreach (Vector3 vertex in _vertexes)
        {
            Debug.Log("vert = " + vertex);
        }
        Debug.Log("==========================================");
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);

        WriteToFile();
        _vertexes.Clear();
    }

    private void WriteToFile()
    {
        _file = new StreamWriter($"Dataset\\DatasetRes\\{_name}_{_transformable.GetTransformIteration()}.txt");
        char[] ch = { '(', ')' };
        foreach (Vector3 vertex in _vertexes)
        {
            _file.WriteLine(vertex.ToString().Trim(ch));
        }

        _file.Close();
    }
}