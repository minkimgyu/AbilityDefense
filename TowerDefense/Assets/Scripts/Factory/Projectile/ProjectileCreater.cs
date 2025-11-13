using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileCreater
{
    protected GameObject _projectilePrefab;

    public ProjectileCreater(GameObject projectilePrefab)
    {
        _projectilePrefab = projectilePrefab;
    }

    abstract public IProjectile Create(IProjectile.Name name);
}
