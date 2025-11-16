using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBulletStrategy : FireProjectileStrategy
{
    List<ITarget.Type> _targetTypes;
    BuffValue<float> _damage;
    BuffValue<float> _projectileSpeed;

    public FireBulletStrategy(
        IProjectile.Name name,
        List<ITarget.Type> targetTypes,
        BuffValue<float> damage,
        BuffValue<float> projectileSpeed,

        BuffValue<float> attackRate,
        ProjectileFactory projectileFactory) : base(name, attackRate, projectileFactory)
    {
        _targetTypes = targetTypes;
        _damage = damage;
        _projectileSpeed = projectileSpeed;
    }

    public override void Attack(TargetCaptureComponent.Data targetData)
    {
        bool canFire = CanFire();
        if (canFire == false) return;

        Bullet.Data bulletData = new Bullet.Data(
            _damage.Value,
            _projectileSpeed.Value
        );

        for (int i = 0; i < _firePoints.Count; i++)
        {
            IProjectile projectile = GetProjectile();
            projectile.SetData(bulletData);
            projectile.Fire(_firePoints[i].position, _firePoints[i].rotation, targetData.CapturedTarget);
        }

        ResetTimer();
    }
}
