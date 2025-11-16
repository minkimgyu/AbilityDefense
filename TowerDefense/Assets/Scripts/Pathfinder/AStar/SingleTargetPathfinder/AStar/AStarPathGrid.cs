using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Debug = UnityEngine.Debug;

namespace PathfinderForTilemap
{
    public class AStarPathGrid : MonoBehaviour, IPathGrid<AStarPathNode>
    {
        AStarPathNode[,] _pathNodes; // r, c

        Grid2D _gridSize;

        Vector3 _topLeftPos;
        Vector3 _bottomRightPos;

        [SerializeField] BaseAStarPathGridGenerator _pathGridGenerator;
        [SerializeField] PathNodeSO _pathNodeSO;

        public Grid2D GetGridSize() { return _gridSize; }

        AStarPathGridDrawer _drawer;

        // 초기화 함수
        public void Initialize()
        {
            if (_pathNodeSO == null) return;

            _pathNodes = _pathNodeSO.GetPathNodes();
            _gridSize = new Grid2D(_pathNodes.GetLength(0), _pathNodes.GetLength(1));

            _topLeftPos = GetPathNode(new Grid2D(0, 0)).WorldPos;
            _bottomRightPos = GetPathNode(new Grid2D(_pathNodes.GetLength(0) - 1, _pathNodes.GetLength(1) - 1)).WorldPos;

            _drawer = GetComponent<AStarPathGridDrawer>();
            if (_drawer != null) _drawer.Initialize(this);
        }

        [ContextMenu("RebuildGrid")]
        public void RG()
        {
            RebuildGrid();
        }

        public void RebuildGrid(Grid2D startIndex = default, Grid2D endIndex = default)
        {
            if (_pathGridGenerator == null)
            {
                Debug.LogError("PathGridGenerator is null!");
                return;
            }

            // 전체 그리드 갱신
            if (endIndex.Row == 0 && endIndex.Column == 0)
                endIndex = _gridSize;

            AStarPathNode[,] newPathNodes =
                _pathGridGenerator.RebuildGrid(startIndex, endIndex);

            for (int i = startIndex.Row; i < endIndex.Row; i++)
            {
                for (int j = startIndex.Column; j < endIndex.Column; j++)
                {
                    int r = i - startIndex.Row;
                    int c = j - startIndex.Column;

                    _pathNodes[i, j].Block = newPathNodes[r, c].Block;
                    _pathNodes[i, j].NearNodeIndexes = newPathNodes[r, c].NearNodeIndexes;
                }
            }
        }

        public AStarPathNode GetPathNode(Grid2D grid) { return _pathNodes[grid.Row, grid.Column]; }

        // -----------------------------
        // 좌표 Clamp
        // GridComponent GetClampedRange와 동일
        // -----------------------------
        public Vector3 GetClampedPosition(Vector3 worldPos)
        {
            Vector3 topLeft = _topLeftPos;
            Vector3 bottomRight = _bottomRightPos;

            float x = Mathf.Clamp(worldPos.x, topLeft.x, bottomRight.x);
            float z = Mathf.Clamp(worldPos.z, bottomRight.z, topLeft.z);

            return new Vector3(x, 0, z);
        }

        // -----------------------------
        // 월드 좌표 → Grid 인덱스 변환
        // GridComponent GetNodeIndex와 동일
        // -----------------------------
        public Grid2D GetPathNodeIndex(Vector3 worldPos)
        {
            Vector3 clamped = GetClampedPosition(worldPos);
            Vector3 topLeft = _topLeftPos;

            float nodeSize = 1f; // GridComponent의 규칙과 동일

            int r = Mathf.RoundToInt(Mathf.Abs(topLeft.z - clamped.z) / nodeSize);
            int c = Mathf.RoundToInt(Mathf.Abs(topLeft.x - clamped.x) / nodeSize);

            r = Mathf.Clamp(r, 0, _gridSize.Row - 1);
            c = Mathf.Clamp(c, 0, _gridSize.Column - 1);

            return new Grid2D(r, c);
        }
    }
}