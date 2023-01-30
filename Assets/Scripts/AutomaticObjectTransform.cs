using System;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class AutomaticObjectTransform : MonoBehaviour
{
    private IMovable _movable;
    private Vector3 _direction;

    private void Awake()
    {
        _movable = GetComponent<IMovable>();
        _direction = new Vector3(-1, 0, 0);

        if (_movable == null)
        {
            throw new Exception($"There is no IMovable on the object: {gameObject.name}");
        }
    }

    private void Start()
    {
        InvokeRepeating("AutoMove", 0.5f, 0.5f);
    }

    private void AutoMove()
    {
        _movable.AutomaticInputMove(_direction);
    }
}