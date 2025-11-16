using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedMissileTowerCreater : EntityCreater
{
    ProjectileFactory _projectileFactory;
    EntityData _data;

    public GuidedMissileTowerCreater(GameObject entityPrefab, EntityData data, ProjectileFactory projectileFactory) : base(entityPrefab)
    {
        _projectileFactory = projectileFactory;
        _data = data;
    }

    public override Entity Create(Entity.Name name)
    {
        GameObject entityGO = Object.Instantiate(_entityPrefab);
        Entity entity = entityGO.GetComponent<Entity>();

        GuidedMissileTowerData cloneData = (GuidedMissileTowerData)_data.Clone();

        entity.InjectStrategy(
            new TargetDetectingStrategy(
                cloneData.TargetTypes
            ),
            new FireMissileStrategy(
                cloneData.ProjectileName,
                cloneData.TargetTypes,
                cloneData.ExplosionDamage,
                cloneData.ExplosionRange,
                cloneData.ProjectileSpeed,
                cloneData.AttackRate,
                _projectileFactory
            ),
            new RotateToTargetStrategy
            (
                cloneData.RotationSpeed
            )
        );

        entity.Initialize();

        entity.SetState(Entity.EntityState.Idle);
        entity.SetState(Entity.PlacementState.Planted);
        entity.SetState(Entity.LifeState.Alive);

        

        return entity;
    }
}
