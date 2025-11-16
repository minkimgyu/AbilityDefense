using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPathFinder
{
    Vector2 GetDirection(Vector3 worldPos);
}
