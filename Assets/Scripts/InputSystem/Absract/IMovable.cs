using UnityEngine;

public interface IMovable
{
    void ManualInputMove(Vector3 inputDirection);
    void AutomaticMove();
    Vector3 GetCurrentModelPosition();
    Vector3 GetModelSize();
}