using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCreater : ProjectileCreater
{
    public BulletCreater(GameObject projectilePrefab) : base(projectilePrefab)
    {
    }

    public override IProjectile Create(IProjectile.Name name)
    {
        GameObject projectileGO = Object.Instantiate(_projectilePrefab);
        IProjectile projectile = projectileGO.GetComponent<IProjectile>();
        return projectile;
    }
}
