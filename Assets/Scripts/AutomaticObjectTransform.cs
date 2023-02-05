using System;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class AutomaticObjectTransform : MonoBehaviour
{
    [SerializeField][Range(0.01f, 1.00f)] private float _timeIteration;

    private IMovable _movable;

    private void Awake()
    {
        _movable = GetComponent<IMovable>();

        if (_movable == null)
        {
            throw new Exception($"There is no IMovable on the object: {gameObject.name}");
        }
    }

    private void Start()
    {
        InvokeRepeating("AutoMove", _timeIteration, _timeIteration);
    }

    private void AutoMove()
    {
        _movable.AutomaticInputMove();
    }
}