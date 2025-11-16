using FlowField;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkUnitCreater : EntityCreater
{
    FlowField.PathTracker _pathTracker;
    EntityData _data;

    public WalkUnitCreater(
        GameObject entityPrefab,
        PathTracker pathTracker,
        EntityData data) : base(entityPrefab)
    {
        _pathTracker = pathTracker;
        _data = data;
    }

    public override Entity Create(Entity.Name name)
    {
        GameObject entityGO = Object.Instantiate(_entityPrefab);
        Entity entity = entityGO.GetComponent<Entity>();

        WalkUnitData cloneData = (WalkUnitData)_data.Clone();

        entity.InjectStrategy(
            new NoDetectStrategy(),
            new NoAttackStrategy(),
            new MoveTowardsDestinationStrategy(
                _pathTracker,
                entityGO.transform,
                cloneData.MoveSpeed,
                cloneData.RotationSpeed
            )
        );

        entity.Initialize();

        entity.SetState(Entity.EntityState.Idle);
        entity.SetState(Entity.PlacementState.Planted);
        entity.SetState(Entity.LifeState.Alive);

        IHealth health = entity.GetComponent<IHealth>();
        health.SetHP(cloneData.MaxHp);

        return entity;
    }
}
