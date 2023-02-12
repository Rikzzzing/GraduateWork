using System;
using UnityEngine;

public class AutomaticGenerateRaycast : MonoBehaviour
{
    [SerializeField][Range(0.01f, 1.00f)] private float _timeIteration;

    private IRaycastable _raycastable;

    private void Awake()
    {
        _raycastable = GetComponent<IRaycastable>();

        if (_raycastable == null)
        {
            throw new Exception($"There is no IRaycastable on the object: {gameObject.name}");
        }
    }

    private void FixedUpdate()
    {
        AutoRaycast();
    }

    private void AutoRaycast()
    {
        Vector3 area = new Vector3(-1, 1, 1);
        Vector3 startPosition = new Vector3(-1, 1, 1);

        _raycastable.AutomaticRaycast(area, startPosition);
    }
}