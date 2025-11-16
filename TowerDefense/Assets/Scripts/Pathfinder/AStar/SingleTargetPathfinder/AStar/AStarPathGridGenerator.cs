using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathfinderForTilemap
{
    public class AStarPathGridGenerator : BaseAStarPathGridGenerator
    {
        protected override void SetTerrainPenaltyBias(Grid2D size, AStarPathNode[,] pathNodes)
        {
        }
    }
}