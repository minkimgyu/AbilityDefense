using PathfinderForTilemap;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace PathfinderForTilemap
{
    abstract public class BaseAStarPathGridGenerator : MonoBehaviour, IPathGridGenerator
    {
        [Header("3D Grid Parents")]
        [SerializeField] GameObject _wallTileParent;
        [SerializeField] GameObject _groundTileParent;

        [SerializeField] PathNodeSO _pathNodeSO;

        // 생성된 그리드
        Dictionary<Vector2Int, bool> _isWall = new Dictionary<Vector2Int, bool>();

        NodeBounds _bounds;

        struct NodeBounds
        {
            public Vector2Int min;
            public Vector2Int max;
        }

        /// <summary>
        /// 그리드 맵 생성
        /// </summary>
        [ContextMenu("GenerateGrid")]
        public void GenerateGrid()
        {
#if UNITY_EDITOR
            BuildBounds();
            AStarPathNode[,] nodes = CreateGrid();

            if (_pathNodeSO != null)
            {
                _pathNodeSO.SetPathDatas(nodes);
                EditorUtility.SetDirty(_pathNodeSO);
                AssetDatabase.SaveAssets();
            }
#endif
        }

        public AStarPathNode[,] RebuildGrid(Grid2D startIndex, Grid2D endIndex)
        {
            BuildBounds();
            return CreateGrid();
        }

        void BuildBounds()
        {
            _isWall.Clear();

            Transform[] ground = _groundTileParent.GetComponentsInChildren<Transform>();

            Vector2Int min = new Vector2Int(int.MaxValue, int.MaxValue);
            Vector2Int max = new Vector2Int(int.MinValue, int.MinValue);

            // 1. Ground 기준 Grid 크기 계산 (X,Z 기준)
            foreach (Transform t in ground)
            {
                if (t == _groundTileParent.transform) continue;

                Vector2Int p = new Vector2Int(
                    Mathf.RoundToInt(t.position.x),
                    Mathf.RoundToInt(t.position.z)
                );

                if (p.x < min.x) min.x = p.x;
                if (p.y < min.y) min.y = p.y;
                if (p.x > max.x) max.x = p.x;
                if (p.y > max.y) max.y = p.y;
            }

            _bounds.min = min;
            _bounds.max = max;

            // 2. Wall 위치 저장
            Transform[] wall = _wallTileParent.GetComponentsInChildren<Transform>();
            foreach (Transform t in wall)
            {
                if (t == _wallTileParent.transform) continue;

                Vector2Int p = new Vector2Int(
                    Mathf.RoundToInt(t.position.x),
                    Mathf.RoundToInt(t.position.z)
                );

                _isWall[p] = true;
            }
        }

        AStarPathNode[,] CreateGrid()
        {
            int rowSize = _bounds.max.y - _bounds.min.y + 1; // Z 방향
            int colSize = _bounds.max.x - _bounds.min.x + 1; // X 방향

            AStarPathNode[,] nodes = new AStarPathNode[rowSize, colSize];

            // Top-Left 기준점
            Vector3 topLeft = new Vector3(
                _bounds.min.x,
                0f,
                _bounds.max.y
            );

            // 1. 노드 생성
            for (int row = 0; row < rowSize; row++)
            {
                for (int col = 0; col < colSize; col++)
                {
                    float worldX = topLeft.x + col;
                    float worldZ = topLeft.z - row;

                    Vector3 worldPos = new Vector3(worldX, 0f, worldZ);

                    bool isBlock = false;
                    Vector2Int key = new Vector2Int(Mathf.RoundToInt(worldX), Mathf.RoundToInt(worldZ));

                    if (_isWall.ContainsKey(key)) isBlock = true;

                    Grid2D gridIndex = new Grid2D(row, col);
                    nodes[row, col] = new AStarPathNode(worldPos, gridIndex, isBlock);
                }
            }

            // 2. 인접 노드 설정
            SetNearNodes(nodes, rowSize, colSize);

            return nodes;
        }

        void SetNearNodes(AStarPathNode[,] nodes, int rowSize, int colSize)
        {
            Grid2D gridSize = new Grid2D(rowSize, colSize);

            for (int r = 0; r < rowSize; r++)
            {
                for (int c = 0; c < colSize; c++)
                {
                    AStarPathNode n = nodes[r, c];
                    Grid2D index = new Grid2D(r, c);

                    n.NearNodeIndexes[PathSize.Size1x1] =
                        GetNearNodeIndexes(nodes, gridSize, index, PathSize.Size1x1);

                    n.NearNodeIndexes[PathSize.Size3x3] =
                        GetNearNodeIndexes(nodes, gridSize, index, PathSize.Size3x3);
                }
            }
        }

        List<int> GetNearNodeIndexes(AStarPathNode[,] nodes, Grid2D size, Grid2D index, PathSize pathSize)
        {
            // 기존 BaseAStarPathGridGenerator(2D Tilemap) 로직 그대로 재활용
            // 단, tilemap 계산 로직만 제거됨.
            switch (pathSize)
            {
                case PathSize.Size1x1:
                    return GetNearMovable1x1(nodes, size, index);

                case PathSize.Size3x3:
                    return GetNearMovable3x3(nodes, size, index);
            }
            return null;
        }


        bool OutOfRange(Grid2D size, Grid2D index)
        {
            return index.Row < 0 || index.Column < 0 ||
                   index.Row >= size.Row || index.Column >= size.Column;
        }

        List<int> GetNearMovable1x1(AStarPathNode[,] nodes, Grid2D size, Grid2D index)
        {
            List<int> near = new List<int>();

            // 직선 방향
            for (int i = 0; i < GridUtility.NearStraightIndexes.Length; i++)
            {
                Grid2D dir = GridUtility.NearIndexes[GridUtility.NearStraightIndexes[i]];
                Grid2D n = new Grid2D(index.Row + dir.Row, index.Column + dir.Column);

                if (OutOfRange(size, n)) continue;
                if (nodes[n.Row, n.Column].Block) continue;

                near.Add(GridUtility.NearStraightIndexes[i]);
            }

            // 대각선 방향
            for (int i = 0; i < GridUtility.NearDiagonalIndexes.Length; i++)
            {
                Grid2D dir = GridUtility.NearIndexes[GridUtility.NearDiagonalIndexes[i]];
                Grid2D n = new Grid2D(index.Row + dir.Row, index.Column + dir.Column);

                if (OutOfRange(size, n)) continue;
                if (nodes[n.Row, n.Column].Block) continue;

                // 코너 체크 (기존 로직 그대로)
                if (!CheckDiagonal(nodes, size, index, i)) continue;

                near.Add(GridUtility.NearDiagonalIndexes[i]);
            }

            return near;
        }

        bool CheckDiagonal(AStarPathNode[,] nodes, Grid2D size, Grid2D index, int diagonalCase)
        {
            Grid2D g1, g2;
            AStarPathNode n1, n2;

            switch (diagonalCase)
            {
                case 0:
                    g1 = Add(index, GridUtility.NearIndexes[GridUtility.NearStraightIndexes[0]]);
                    g2 = Add(index, GridUtility.NearIndexes[GridUtility.NearStraightIndexes[1]]);
                    break;
                case 1:
                    g1 = Add(index, GridUtility.NearIndexes[GridUtility.NearStraightIndexes[0]]);
                    g2 = Add(index, GridUtility.NearIndexes[GridUtility.NearStraightIndexes[2]]);
                    break;
                case 2:
                    g1 = Add(index, GridUtility.NearIndexes[GridUtility.NearStraightIndexes[1]]);
                    g2 = Add(index, GridUtility.NearIndexes[GridUtility.NearStraightIndexes[3]]);
                    break;
                case 3:
                    g1 = Add(index, GridUtility.NearIndexes[GridUtility.NearStraightIndexes[2]]);
                    g2 = Add(index, GridUtility.NearIndexes[GridUtility.NearStraightIndexes[3]]);
                    break;
                default:
                    return false;
            }

            if (OutOfRange(size, g1) || OutOfRange(size, g2)) return false;

            n1 = nodes[g1.Row, g1.Column];
            n2 = nodes[g2.Row, g2.Column];

            return (!n1.Block && !n2.Block);
        }

        Grid2D Add(Grid2D a, Grid2D b)
        {
            return new Grid2D(a.Row + b.Row, a.Column + b.Column);
        }

        List<int> GetNearMovable3x3(AStarPathNode[,] nodes, Grid2D size, Grid2D index)
        {
            List<int> near = new List<int>();

            for (int i = 0; i < GridUtility.NearDiagonalIndexes.Length; i++)
            {
                Grid2D n = Add(index, GridUtility.NearIndexes[GridUtility.NearDiagonalIndexes[i]]);
                if (OutOfRange(size, n)) continue;
                if (nodes[n.Row, n.Column].Block) continue;

                if (HaveBlockInNear(nodes, size, n)) continue;
                near.Add(GridUtility.NearDiagonalIndexes[i]);
            }

            for (int i = 0; i < GridUtility.NearStraightIndexes.Length; i++)
            {
                Grid2D n = Add(index, GridUtility.NearIndexes[GridUtility.NearStraightIndexes[i]]);
                if (OutOfRange(size, n)) continue;
                if (nodes[n.Row, n.Column].Block) continue;

                if (HaveBlockInNear(nodes, size, n)) continue;
                near.Add(GridUtility.NearStraightIndexes[i]);
            }

            return near;
        }

        bool HaveBlockInNear(AStarPathNode[,] nodes, Grid2D size, Grid2D index)
        {
            for (int i = 0; i < GridUtility.NearIndexes.Length; i++)
            {
                Grid2D n = Add(index, GridUtility.NearIndexes[i]);
                if (!OutOfRange(size, n) && nodes[n.Row, n.Column].Block)
                    return true;
            }
            return false;
        }


        // 가중치 설정
        abstract protected void SetTerrainPenaltyBias(Grid2D size, AStarPathNode[,] pathNodes);
    }
}