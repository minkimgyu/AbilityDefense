using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFactory
{
    Dictionary<Entity.Name, EntityCreater> _entityCreater;

    public EntityFactory(
        Dictionary<Entity.Name, GameObject> entityPrefab, 
        Dictionary<Entity.Name, EntityData> entityData,
        ProjectileFactory projectileFactory,
        FlowField.PathTracker pathTracker)
    {
        _entityCreater = new Dictionary<Entity.Name, EntityCreater>()
        {
            {
                Entity.Name.GuidedMissileTower, new GuidedMissileTowerCreater(
                    entityPrefab[Entity.Name.GuidedMissileTower],
                    entityData[Entity.Name.GuidedMissileTower],
                    projectileFactory
                )
            },
             {
                Entity.Name.BulletTower, new BulletTowerCreater(
                    entityPrefab[Entity.Name.BulletTower],
                    entityData[Entity.Name.BulletTower],
                    projectileFactory
                )
            },
             {
                Entity.Name.Imp, new WalkUnitCreater(
                    entityPrefab[Entity.Name.Imp],
                    pathTracker,
                    entityData[Entity.Name.Imp]
                )
            },
             {
                Entity.Name.Knight, new WalkUnitCreater(
                    entityPrefab[Entity.Name.Knight],
                    pathTracker,
                    entityData[Entity.Name.Knight]
                )
            },
             {
                Entity.Name.Nosedman, new WalkUnitCreater(
                    entityPrefab[Entity.Name.Nosedman],
                    pathTracker,
                    entityData[Entity.Name.Nosedman]
                )
            },
        };
    }

    public Entity Create(Entity.Name name)
    {
        Entity entity = _entityCreater[name].Create(name);
        entity.Initialize();
        return entity;
    }
}
