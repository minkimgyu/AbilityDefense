using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedMissileTowerCreater : EntityCreater
{
    ProjectileFactory _projectileFactory;
    GuidedMissileTowerData _data;

    public GuidedMissileTowerCreater(GameObject entityPrefab, GuidedMissileTowerData data, ProjectileFactory projectileFactory) : base(entityPrefab)
    {
        _projectileFactory = projectileFactory;
        _data = data;
    }

    public override Entity Create(Entity.Name name)
    {
        GameObject entityGO = Object.Instantiate(_entityPrefab);
        Entity entity = entityGO.GetComponent<Entity>();

        entity.InjectStrategy(
            new TargetDetectingStrategy(
                _data.TargetTypes
            ),
            new FireMissileStrategy(
                _data.ProjectileName,
                _data.TargetTypes,
                _data.ExplosionDamage,
                _data.ExplosionRange,
                _data.ProjectileSpeed,
                _data.AttackRate,
                _projectileFactory
            ),
            new RotateToTargetStrategy
            (
                _data.RotationSpeed
            )
        );

        entity.Initialize();

        entity.SetState(Entity.EntityState.Idle);
        entity.SetState(Entity.PlacementState.Planted);
        entity.SetState(Entity.LifeState.Alive);

        

        return entity;
    }
}
