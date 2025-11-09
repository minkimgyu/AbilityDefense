using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingStrategy : IAttackStrategy
{
    TargetCaptureComponent _targetCaptureComponent;
    List<TargetCaptureComponent.Data> _targetDatas;
    Timer _timer;

    IProjectile.Name _name;
    ProjectileFactory _projectileFactory;

    BuffValue<float> _shootDelay;

    public ShootingStrategy(
        GameObject owner,
        IProjectile.Name name,
        BuffValue<float> shootDelay,
        ProjectileFactory projectileFactory)
    {
        _name = name;
        _projectileFactory = projectileFactory;
        _shootDelay = shootDelay;

        _timer = new Timer();
        _targetDatas = new List<TargetCaptureComponent.Data>();

        _targetCaptureComponent = owner.GetComponentInChildren<TargetCaptureComponent>();
        _targetCaptureComponent.InjectCaptureEvent(OnCaptureTarget, OnRemoveTarget);
    }

    void OnCaptureTarget(TargetCaptureComponent.Data data)
    {
        _targetDatas.Add(data);
    }

    void OnRemoveTarget(TargetCaptureComponent.Data data)
    {
        _targetDatas.Remove(data);
    }

    public void OnUpdate()
    {
        if (_targetDatas.Count == 0) return; // 타겟이 없으면 return

        // 타이머 추가해서 일정 시간마다 발사하게 하기
        if (_timer.CurrentState == Timer.State.Running) return; // 타이머가 동작하지 않으면 return

        for (int i = 0; i < _targetDatas.Count; i++)
        {
            IProjectile projectile = _projectileFactory.Create(_name);
            projectile.Fire(_targetDatas[i].CapturedTarget, _targetDatas[i].CapturedDamageable);
        }

        _timer.Start(_shootDelay.Value);
    }
}
