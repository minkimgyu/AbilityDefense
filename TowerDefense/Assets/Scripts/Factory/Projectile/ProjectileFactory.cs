using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileFactory
{
    Dictionary<IProjectile.Name, GameObject> _projectilePrefab;

    public ProjectileFactory(Dictionary<IProjectile.Name, GameObject> projectilePrefab)
    {
        _projectilePrefab = projectilePrefab;
    }

    public IProjectile Create(IProjectile.Name name)
    {
        GameObject projectileGO = Object.Instantiate(_projectilePrefab[name]);
        IProjectile projectile = projectileGO.GetComponent<IProjectile>();

        return projectile;
    }
}
