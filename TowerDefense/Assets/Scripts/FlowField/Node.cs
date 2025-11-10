using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace FlowField
{
    public class Node : INode<Node>
    {
        public enum State
        {
            Empty,
            Block,
        }

        public Node(Vector2 pos, Vector2Int index, State state)
        {
            _localPos = pos;
            _index = index;
            _state = state;
            _pathWeight = int.MaxValue;
            HaveBlockNodeInNeighbor = false;
        }

        // 그리드 상 위치
        Vector2 _localPos;
        public Vector2 LocalPos { get { return _localPos; } }

        // 실제 위치
        public Vector3 WorldPos { get { return new Vector3(_localPos.x, 0, _localPos.y); } }

        Vector2Int _index; // 그리드 상 인덱스
        public Vector2Int Index { get { return _index; } }

        public byte Weight
        {
            get
            {
                switch (_state)
                {
                    case State.Empty:
                        return 1;
                    case State.Block:
                        return 255;
                    default:
                        return 0;
                }
            }
        }

        Vector2 directionToMove;
        public Vector2 DirectionToMove { get { return directionToMove; } set { directionToMove = value; } } // 노드의 방향성


        float _pathWeight; // 다익스트라로 생성되는 가중치
        public float PathWeight { get { return _pathWeight; } set { _pathWeight = value; } }


        State _state;
        public State CurrentState { get { return _state; } }

        // 주변 모든 노드 포함
        public List<Node> NearNodes { get; set; }

        // 올바른 방향을 가리키는 노드
        public List<Node> MovableNodes { get; set; }

        public bool HaveBlockNodeInNeighbor { get; set; }

        public int CompareTo(Node other)
        {
            int compareValue = _pathWeight.CompareTo(other._pathWeight);
            return compareValue;
        }
    }
}