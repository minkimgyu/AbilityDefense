using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectileStrategy : IAttackStrategy
{
    Timer _timer;

    IProjectile.Name _name;
    ProjectileFactory _projectileFactory;

    BuffValue<float> _shootDelay;
    List<Transform> _firePoints;

    public FireProjectileStrategy(
        IProjectile.Name name,
        BuffValue<float> shootDelay,
        ProjectileFactory projectileFactory)
    {
        _name = name;
        _projectileFactory = projectileFactory;
        _shootDelay = shootDelay;
        _timer = new Timer();
        _firePoints = new List<Transform>();
    }


    public void InjectFirePoint(List<Transform> firePoints)
    {
        _firePoints = firePoints;
    }

    void FireToTarget(int idx, TargetCaptureComponent.Data targetData)
    {
        IProjectile projectile1 = _projectileFactory.Create(_name);
        projectile1.Fire(_firePoints[idx].position, _firePoints[idx].rotation, targetData.CapturedTarget);
    }

    public void Attack(TargetCaptureComponent.Data targetData)
    {
        if (_timer.CurrentState == Timer.State.Running) return; // 타이머가 동작하지 않으면 return
        if(targetData.CapturedTarget == null) return;
        // 이미 파괴된 타겟이면 발사하지 않음

        for (int i = 0; i < _firePoints.Count; i++)
        {
            FireToTarget(i, targetData);
        }

        _timer.Reset();
        _timer.Start(_shootDelay.Value);
    }

    public void OnUpdate()
    {
        _timer.Update(Time.deltaTime);
    }
}
