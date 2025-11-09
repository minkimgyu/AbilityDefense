using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedMissileTowerCreater : TowerCreater
{
    ProjectileFactory _projectileFactory;

    public GuidedMissileTowerCreater(GameObject entityPrefab, EntityData data, ProjectileFactory projectileFactory) : base(entityPrefab, data)
    {
        _projectileFactory = projectileFactory;
    }

    public override Entity Create(Entity.Name name)
    {
        GuidedMissileTowerData data = (GuidedMissileTowerData)_data;

        Entity entity = Object.Instantiate(_entityPrefab).GetComponent<Entity>();
        entity.InjectStrategy(
            new StopStrategy(),
            new ShootingStrategy(
                entity.gameObject, 
                data.ProjectileName,
                data.AttackRate,
                _projectileFactory
            )
        );

        entity.Initialize();
        return entity;
    }
}
