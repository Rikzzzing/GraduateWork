using UnityEngine;

public interface IRaycastable
{
    void ManualInputRaycast();
    void AutomaticRaycast(Vector3 startPosition, Vector3 area);
}