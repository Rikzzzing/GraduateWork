using UnityEngine;

public interface IMovable
{
    void ManualInputMove(Vector3 inputDirection);
    void AutomaticInputMove(Vector3 inputDirection);
}