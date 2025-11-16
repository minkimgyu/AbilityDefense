using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPathTracker
{
    /// <summary>
    /// 현재 위치(worldPos)를 기준으로 다음 이동 위치를 계산하여 반환합니다.
    /// </summary>
    Vector3 Track(Vector3 worldPos);
}