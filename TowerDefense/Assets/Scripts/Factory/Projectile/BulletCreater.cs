using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCreater : ProjectileCreater
{
    EffectFactory _effectFactory;

    public BulletCreater(GameObject projectilePrefab, EffectFactory effectFactory) : base(projectilePrefab)
    {
        _effectFactory = effectFactory;
    }

    public override IProjectile Create(IProjectile.Name name)
    {
        GameObject projectileGO = Object.Instantiate(_projectilePrefab);
        IProjectile projectile = projectileGO.GetComponent<IProjectile>();
        projectile.InjectEffectFactory(_effectFactory);
        return projectile;
    }
}
