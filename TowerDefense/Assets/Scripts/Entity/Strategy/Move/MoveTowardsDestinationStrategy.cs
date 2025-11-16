using FlowField;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsDestinationStrategy : IMoveStrategy
{
    IPathTracker _pathTracker;
    BuffValue<float> _moveSpeed;
    float _rotateSpeed;
    Animator _animator;

    public MoveTowardsDestinationStrategy(
        IPathTracker pathTracker,
        Transform unitTransform,
        BuffValue<float> moveSpeed,
        float rotateSpeed)
    {
        _pathTracker = pathTracker;
        _unitTransform = unitTransform;
        _moveSpeed = moveSpeed;
        _rotateSpeed = rotateSpeed;
    }

    public void InjectAnimator(Animator animator) 
    {
        _animator = animator;
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
        Vector3 direction = (nxtPos - _unitTransform.position); // 이동 방향 계산

        bool canMove = direction.sqrMagnitude > 0.0001f;
        if (canMove == false)
        {
            _animator.SetBool("Walk", false);
            return;
        }
        else
        {
            _animator.SetBool("Walk", true);
        }

        Move(nxtPos);
        Rotate(direction);
    }
}
