using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RotateToTargetStrategy : IMoveStrategy
{
    float _rotateSpeed;

    public RotateToTargetStrategy(
        float rotateSpeed)
    {
        _rotateSpeed = rotateSpeed;
    }

    Transform _turretTransform;

    public void InjectTurretTransform(Transform turretTransform) 
    {
        _turretTransform = turretTransform;
    }

    public void RotateTo(Vector3 targetPosition)
    {
        if (_turretTransform == null) return;

        // 터렛과 타겟 방향 벡터 계산
        Vector3 direction = targetPosition - _turretTransform.position;
        if (direction == Vector3.zero) return; // 타겟과 같은 위치일 경우 회전할 필요 없음

        // 터렛이 바라봐야 하는 목표 회전
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // 현재 회전에서 목표 회전까지 회전 (프레임에 따라 부드럽게)
        _turretTransform.rotation = Quaternion.RotateTowards(
            _turretTransform.rotation,
            targetRotation,
            _rotateSpeed * Time.deltaTime
        );
    }
}
