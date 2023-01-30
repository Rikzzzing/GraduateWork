using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputObject : MonoBehaviour
{
    private InputDoc _inputDoc;
    private IMovable _movable;

    private void Awake()
    {
        if (_inputDoc == null)
        {
            _inputDoc = new InputDoc();
        }

        _movable = GetComponent<IMovable>();

        if (_movable == null)
        {
            throw new Exception($"There is no IMovable on the object: {gameObject.name}");
        }
    }

    private void OnEnable()
    {
        _inputDoc.Enable();
        _inputDoc.Management.Move.performed += OnMovePerfermed;
    }

    private void OnMovePerfermed(InputAction.CallbackContext obj)
    {
        var inputDirection = _inputDoc.Management.Move.ReadValue<Vector3>();
        _movable.Move(inputDirection);
    }

    private void OnDisable()
    {
        _inputDoc.Disable();
        _inputDoc.Management.Move.performed -= OnMovePerfermed;
    }
}