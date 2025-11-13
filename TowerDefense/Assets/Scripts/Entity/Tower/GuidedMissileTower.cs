using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedMissileTower : Tower
{
    [SerializeField] Transform _firePoint1;
    [SerializeField] Transform _firePoint2;
    [SerializeField] Transform _turretTransform;
    [SerializeField] TargetCaptureComponent _targetCaptureComponent;

    public override void Initialize()
    {
        List<Transform> firePoints = new List<Transform>() { _firePoint1, _firePoint2 };
        _attackStrategy.InjectFirePoint(firePoints);
        _moveStrategy.InjectTurretTransform(_turretTransform);
        _detectStrategy.InjectCaptureComponent(_targetCaptureComponent);
    }

    public override void OnUpdate()
    {
        bool canDetact = _detectStrategy.TryDetectTarget(out TargetCaptureComponent.Data targetData);
        if(canDetact == false) return;

        _attackStrategy.Attack(targetData);

       Transform targetTransform = targetData.CapturedTarget.GetTransform();
        if(targetTransform == null) return;

        _moveStrategy.RotateTo(targetTransform.position);
    }
}