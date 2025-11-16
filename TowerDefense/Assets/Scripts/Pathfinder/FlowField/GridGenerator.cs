using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace FlowField
{
    public struct Grid
    {
        private int _rowSize;
        private int _colSize;

        public int RowSize { get => _rowSize; set => _rowSize = value; }
        public int ColSize { get => _colSize; set => _colSize = value; }

        public Grid(int rowSize, int colSize)
        {
            _rowSize = rowSize;
            _colSize = colSize;
        }
    }

    public class GridGenerator : MonoBehaviour
    {
        [SerializeField] GameObject _wallTileParent;
        [SerializeField] GameObject _groundTileParent;

        Node[,] _nodes; // r, c

        Grid _grid;
        Dictionary<Vector2Int, bool> _isWall = new Dictionary<Vector2Int, bool>();

        public Node[,] CreateGrid()
        {
            Transform[] groundTiles = _groundTileParent.GetComponentsInChildren<Transform>();

            Vector2Int minBound = new Vector2Int(int.MaxValue, int.MaxValue);
            Vector2Int maxBound = new Vector2Int(int.MinValue, int.MinValue);

            for (int i = 0; i < groundTiles.Length; i++)
            {
                if (_groundTileParent.transform == groundTiles[i]) continue;

                Vector3Int cellPos = new Vector3Int(
                    Mathf.RoundToInt(groundTiles[i].position.x),
                    Mathf.RoundToInt(groundTiles[i].position.y),
                    Mathf.RoundToInt(groundTiles[i].position.z)
                );

                if (cellPos.x < minBound.x) minBound.x = cellPos.x;
                if (cellPos.z < minBound.y) minBound.y = cellPos.z;
                if (cellPos.x > maxBound.x) maxBound.x = cellPos.x;
                if (cellPos.z > maxBound.y) maxBound.y = cellPos.z;
            }

            int rowSize = maxBound.y - minBound.y + 1; // Z 방향
            int colSize = maxBound.x - minBound.x + 1; // X 방향

            _grid = new Grid(rowSize, colSize);
            _nodes = new Node[rowSize, colSize];

            Transform[] wallTiles = _wallTileParent.GetComponentsInChildren<Transform>();

            for (int i = 0; i < wallTiles.Length; i++)
            {
                if (_wallTileParent.transform == wallTiles[i]) continue;

                Vector2Int pos = new Vector2Int(
                   Mathf.RoundToInt(wallTiles[i].position.x),
                   Mathf.RoundToInt(wallTiles[i].position.z)
                );

                _isWall[pos] = true;
            }

            // 3️ 기준점(Top-Left) 잡기
            // minBound는 (왼쪽 아래)이므로,
            // Top-Left는 (x = minBound.x, z = maxBound.y)
            Vector3 topLeftOrigin = new Vector3(minBound.x, 0f, maxBound.y);

            // 4️ 노드 생성
            for (int row = 0; row < rowSize; row++)
            {
                for (int col = 0; col < colSize; col++)
                {
                    // 좌측 상단에서 (col, row)만큼 이동
                    float worldX = topLeftOrigin.x + col;
                    float worldZ = topLeftOrigin.z - row; // z는 위→아래 방향 반전

                    Vector3 worldPos = new Vector3(worldX, 0f, worldZ);
                    Vector2Int index = new Vector2Int(row, col);

                    Vector2Int wallKey = new Vector2Int(
                        Mathf.RoundToInt(worldX),
                        Mathf.RoundToInt(worldZ)
                    );

                    Node.State state = Node.State.Empty;
                    if(_isWall.ContainsKey(wallKey) && _isWall[wallKey] == true) state = Node.State.Block;

                    _nodes[row, col] = new Node(worldPos, index, state);
                }
            }

            return _nodes;
        }
    }
}