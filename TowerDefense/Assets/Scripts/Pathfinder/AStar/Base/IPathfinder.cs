using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathfinderForTilemap
{
    public interface IPathfinder
    {
        List<Vector3> FindPath(Vector3 startPos, Vector3 targetPos, PathSize pathSize);
    }
}
