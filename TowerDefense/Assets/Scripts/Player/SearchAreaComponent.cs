using FlowField;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SearchAreaComponent
{
    GridComponent _gridComponent;
    Queue<Node> _queue;
    HashSet<Node> _visited;

    public SearchAreaComponent(GridComponent gridComponent)
    {
        _gridComponent = gridComponent;
        _queue = new Queue<Node>();
        _visited = new HashSet<Node>();
    }

    bool BFS(Node startNode, AreaData areaData, out Vector2Int resultIdx, out Vector3 resultPos)
    {
        bool isEmpty;

        _queue.Enqueue(startNode);
        _visited.Add(startNode);

        while (_queue.Count > 0)
        {
            Node node = _queue.Dequeue();

            for (int i = 0; i < node.NearNodes.Count; i++)
            {
                if (node.NearNodes[i] == null) continue;
                if (_visited.Contains(node.NearNodes[i]) == true) continue;

                isEmpty = _gridComponent.IsEmptyArea(node.NearNodes[i].Index, areaData);
                if (isEmpty == true)
                {
                    resultIdx = node.NearNodes[i].Index;
                    resultPos = node.NearNodes[i].WorldPos;
                    return true;
                }

                _queue.Enqueue(node.NearNodes[i]);
                _visited.Add(node.NearNodes[i]);
            }
        }

        resultIdx = default;
        resultPos = default;
        return false;
    }

    public bool FindEmptyArea(Vector2Int idx, AreaData areaData, out Vector2Int resultIdx, out Vector3 resultPos)
    {
        _queue.Clear();
        _visited.Clear();

        resultIdx = Vector2Int.zero;
        resultPos = Vector3.zero;

        // 시작 노드 설정
        Node startNode = _gridComponent.ReturnNode(idx);
        if (startNode == null) return false; // 실패 시 기본값 반환

        // 현 자리에 빈 공간이 있는지 확인
        bool isEmpty = _gridComponent.IsEmptyArea(idx, areaData);
        if (isEmpty == true) // 있다면 시작 위치 반환
        {
            resultIdx = idx;
            resultPos = startNode.WorldPos;
            return true;
        }

        // BFS 탐색 시작
        bool canFind = BFS(startNode, areaData, out resultIdx, out resultPos);
        if (canFind == true) return true;
        else return false;  // 실패 시 기본값 반환
    }
}
