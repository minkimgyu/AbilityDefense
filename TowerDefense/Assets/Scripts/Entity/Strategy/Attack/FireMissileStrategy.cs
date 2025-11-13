using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMissileStrategy : FireProjectileStrategy
{
    List<ITarget.Type> _targetTypes;
    BuffValue<float> _explosionDamage;
    BuffValue<float> _explosionRange;
    BuffValue<float> _projectileSpeed;

    public FireMissileStrategy(
        IProjectile.Name name,
        List<ITarget.Type> targetTypes,
        BuffValue<float> explosionDamage,
        BuffValue<float> explosionRange,
        BuffValue<float> projectileSpeed,

        BuffValue<float> attackRate,
        ProjectileFactory projectileFactory) : base(name, attackRate, projectileFactory)
    {
        _targetTypes = targetTypes;
        _explosionDamage = explosionDamage;
        _explosionRange = explosionRange;
        _projectileSpeed = projectileSpeed;
    }

    public override void Attack(TargetCaptureComponent.Data targetData)
    {
        bool canFire = CanFire();
        if (canFire == false) return;

        Missile.Data missileData = new Missile.Data(
            _targetTypes,
            _explosionRange.Value,
            _explosionDamage.Value,
            _projectileSpeed.Value
        );

        for (int i = 0; i < _firePoints.Count; i++)
        {
            IProjectile projectile = GetProjectile();
            projectile.SetData(missileData);
            projectile.Fire(_firePoints[i].position, _firePoints[i].rotation, targetData.CapturedTarget);
        }

        ResetTimer();
    }
}
