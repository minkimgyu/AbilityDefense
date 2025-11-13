using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FireProjectileStrategy : IAttackStrategy
{
    Timer _timer;

    IProjectile.Name _name;
    ProjectileFactory _projectileFactory;

    protected List<Transform> _firePoints;
    BuffValue<float> _attackRate;

    public FireProjectileStrategy(
        IProjectile.Name name,
        BuffValue<float> attackRate,
        ProjectileFactory projectileFactory)
    {
        _name = name;
        _projectileFactory = projectileFactory;
        _attackRate = attackRate;
        _timer = new Timer();
    }

    public void InjectFirePoint(List<Transform> firePoints)
    {
        _firePoints = firePoints;
    }

    public virtual void Attack(TargetCaptureComponent.Data targetData) { }

    protected IProjectile GetProjectile()
    {
        return _projectileFactory.Create(_name);
    }

    protected void ResetTimer()
    {
        _timer.Reset();
        _timer.Start(_attackRate.Value);
    }

    protected bool CanFire()
    {
        if (_timer.CurrentState == Timer.State.Running) return false; // 타이머가 동작하지 않으면 return
        return true;
    }

    public void OnUpdate()
    {
        _timer.Update(Time.deltaTime);
    }
}
