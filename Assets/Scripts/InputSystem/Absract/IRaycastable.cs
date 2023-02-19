using System.Collections.Generic;
using UnityEngine;

public interface IRaycastable
{
    void ManualInputRaycast();
    List<Vector3> AutomaticRaycast(Vector3 startPosition, Vector3 area);
}