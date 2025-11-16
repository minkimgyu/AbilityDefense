using System;
using UnityEngine;

namespace FlowField
{
    /// <summary>
    /// GridComponent의 노드 정보를 기반으로
    /// 3D 월드 좌표에서 이동 방향을 추적하는 클래스입니다.
    /// </summary>
    public class PathTracker : IPathTracker
    {
        private GridComponent _gridComponent;
        private float _stepSize;

        /// <summary>
        /// PathTracker 생성자
        /// </summary>
        /// <param name="gridComponent">GridComponent 주입</param>
        /// <param name="stepSize">이동 거리 단위</param>
        public PathTracker(GridComponent gridComponent, float stepSize = 1f)
        {
            _gridComponent = gridComponent;
            _stepSize = stepSize;
        }

        /// <summary>
        /// 현재 3D 위치를 기준으로 DirectionToMove를 따라 이동한 다음 위치를 반환합니다.
        /// </summary>
        /// <param name="worldPos">현재 월드 좌표 (Vector3)</param>
        /// <returns>다음 이동 위치 (Vector3)</returns>
        public Vector3 Track(Vector3 worldPos)
        {
            // 1️⃣ 현재 노드의 이동 방향 가져오기 (2D 기반)
            Vector2 dir2D = _gridComponent.GetNodeDirection(worldPos);

            // 이동할 방향이 없으면 현재 위치 그대로 반환
            if (dir2D == Vector2.zero) return worldPos;

            // 2️⃣ XZ 평면으로 방향 변환
            Vector3 direction3D = new Vector3(dir2D.x, 0f, dir2D.y).normalized;

            // 3️⃣ 이동 적용
            Vector3 nextPos = worldPos + direction3D * _stepSize;

            // 4️⃣ Grid 범위 내로 보정 (XZ 평면 기준)
            Vector2 clamped2D = _gridComponent.GetClampedRange(nextPos);

            // Y값은 그대로 유지 (지형 높이 등 고려)
            Vector3 clamped3D = new Vector3(clamped2D.x, worldPos.y, clamped2D.y);

            return nextPos;
        }
    }
}
