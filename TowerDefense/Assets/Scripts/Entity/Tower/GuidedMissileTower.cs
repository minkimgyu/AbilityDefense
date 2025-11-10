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
        _attackStrategy.InjectFirePoint(_firePoint1, _firePoint2);
        _moveStrategy.InjectTurretTransform(_turretTransform);
        _detectStrategy.InjectCaptureComponent(_targetCaptureComponent);
    }

    public override void OnUpdate()
    {
        TargetCaptureComponent.Data targetData = _detectStrategy.DetectTarget();
        _attackStrategy.Attack(targetData);
        _moveStrategy.RotateTo(targetData.CapturedTarget.GetTransform().position);
    }
}