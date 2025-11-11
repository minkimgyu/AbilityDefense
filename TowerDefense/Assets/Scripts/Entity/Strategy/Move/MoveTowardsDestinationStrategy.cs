using FlowField;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsDestinationStrategy : IMoveStrategy
{
    FlowField.PathTracker _pathTracker;
    BuffValue<float> _moveSpeed;
    float _rotateSpeed;

    public MoveTowardsDestinationStrategy(
        FlowField.PathTracker pathTracker,
        Transform unitTransform,
        BuffValue<float> moveSpeed,
        float rotateSpeed)
    {
        _pathTracker = pathTracker;
        _unitTransform = unitTransform;
        _moveSpeed = moveSpeed;
        _rotateSpeed = rotateSpeed;
    }

    Transform _unitTransform;

    void Move(Vector3 position)
    {
        // 이동
        _unitTransform.position = Vector3.MoveTowards(
            _unitTransform.position,
            position,
            _moveSpeed.Value * Time.deltaTime
        );
    }

    void Rotate(Vector3 direction)
    {
        // 회전 (부드럽게)
        Quaternion targetRotation = Quaternion.LookRotation(direction.normalized, Vector3.up);

        _unitTransform.rotation = Quaternion.RotateTowards(
            _unitTransform.rotation,
            targetRotation,
            _rotateSpeed * Time.deltaTime
        );
    }

    public void OnUpdate()
    {
        if (_unitTransform == null) return;

        Vector3 nxtPos = _pathTracker.Track(_unitTransform.position);

        // 이동 방향 계산
        Vector3 direction = (nxtPos - _unitTransform.position);
        if (direction.sqrMagnitude < 0.0001f)
            return;

        Move(nxtPos);
        Rotate(direction);
    }
}
