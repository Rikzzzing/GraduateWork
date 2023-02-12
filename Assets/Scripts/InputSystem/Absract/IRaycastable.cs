using UnityEngine;

public interface IRaycastable
{
    void ManualInputRaycast();
    void AutomaticRaycast(Vector3 area, Vector3 startPosition);
}