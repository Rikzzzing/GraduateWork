using UnityEngine;

public interface ITransformable
{
    void ManualInputMove(Vector3 inputDirection);
    void AutomaticMove();
    Vector3 GetCurrentModelPosition();
    Vector3 GetModelSize();
    string GetModelName();
    int GetTransformIteration();
}