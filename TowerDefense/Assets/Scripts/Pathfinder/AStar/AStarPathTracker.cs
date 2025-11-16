using PathfinderForTilemap;
using UnityEngine;
using System.Collections.Generic;

public class AStarPathTracker : IPathTracker
{
    private AStarPathfinder _pathfinder;
    private AStarPathGrid _aStarPathGrid;
    private float _stepSize;
    private Transform _target;

    /// <summary>
    /// AStar 기반 PathTracker
    /// </summary>
    /// <param name="pathfinder">A* 길찾기 알고리즘 인스턴스</param>
    /// <param name="stepSize">이동 단위</param>
    public AStarPathTracker(Transform target, AStarPathGrid aStarPathGrid, AStarPathfinder pathfinder, float stepSize = 1f)
    {
        _target = target;
        _pathfinder = pathfinder;
        _stepSize = stepSize;
        _aStarPathGrid = aStarPathGrid;
    }

    /// <summary>
    /// A* 알고리즘이 계산한 방향을 기준으로 다음 이동 위치를 반환합니다.
    /// </summary>
    public Vector3 Track(Vector3 worldPos)
    {
        // 1️⃣ worldPos에서 target까지의 전체 경로 노드 리스트
        List<Vector3> path = _pathfinder.FindPath(
            worldPos,
            _target.position,
            PathSize.Size1x1
        );

        // 경로가 없으면 그대로 반환
        if (path == null || path.Count < 2)
            return worldPos;

        // path[0]은 현재 위치에 가장 가까운 노드
        // path[1]이 우리가 향해야 할 다음 목적지 노드
        Vector3 nextNode = path[1];

        // 2️⃣ worldPos → nextNode 방향 벡터 계산 (XZ 이동)
        Vector3 direction = (nextNode - worldPos);
        direction.y = 0f;

        // 방향이 없다면 이동 X
        if (direction == Vector3.zero)
            return worldPos;

        Vector3 dirNormalized = direction.normalized;

        // 3️⃣ stepSize 만큼 이동
        Vector3 nextPos = worldPos + dirNormalized * _stepSize;

        // 4️⃣ grid 범위 보정
        Vector3 clampedPos = _aStarPathGrid.GetClampedPosition(nextPos);

        return clampedPos;
    }

}
