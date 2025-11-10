using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMissileStrategy : IAttackStrategy
{
    Timer _timer;

    IProjectile.Name _name;
    ProjectileFactory _projectileFactory;

    BuffValue<float> _shootDelay;

    public FireMissileStrategy(
        IProjectile.Name name,
        BuffValue<float> shootDelay,
        ProjectileFactory projectileFactory)
    {
        _name = name;
        _projectileFactory = projectileFactory;
        _shootDelay = shootDelay;
        _timer = new Timer();
    }

    Transform _firePoint1;
    Transform _firePoint2;

    public void InjectFirePoint(Transform firePoint1, Transform firePoint2)
    {
        _firePoint1 = firePoint1;
        _firePoint2 = firePoint2;
    }

    public void Attack(TargetCaptureComponent.Data targetData)
    {
        if (_timer.CurrentState == Timer.State.Running) return; // 타이머가 동작하지 않으면 return
        if( targetData.CapturedTarget == null || targetData.CapturedDamageable == null ) return;
        // 이미 파괴된 타겟이면 발사하지 않음

        IProjectile projectile1 = _projectileFactory.Create(_name);
        IProjectile projectile2 = _projectileFactory.Create(_name);

        projectile1.Fire(_firePoint1.position, _firePoint1.rotation, targetData.CapturedTarget);
        projectile2.Fire(_firePoint2.position, _firePoint2.rotation, targetData.CapturedTarget);

        _timer.Reset();
        _timer.Start(_shootDelay.Value);
    }

    public void OnUpdate()
    {
        _timer.Update(Time.deltaTime);
    }
}
