using FlowField;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileFactory
{
    Dictionary<IProjectile.Name, ProjectileCreater> _projectileCreater;

    public ProjectileFactory(Dictionary<IProjectile.Name, GameObject> projectilePrefab, EffectFactory effectFactory)
    {
        _projectileCreater = new Dictionary<IProjectile.Name, ProjectileCreater>()
        {
            {
                IProjectile.Name.Bullet, new BulletCreater(projectilePrefab[IProjectile.Name.Bullet], effectFactory)
            },
            {
                IProjectile.Name.Missile, new MissileCreater(projectilePrefab[IProjectile.Name.Missile], effectFactory)
            },
        };
    }

    public IProjectile Create(IProjectile.Name name)
    {
        IProjectile projectile = _projectileCreater[name].Create(name);
        return projectile;
    }
}
