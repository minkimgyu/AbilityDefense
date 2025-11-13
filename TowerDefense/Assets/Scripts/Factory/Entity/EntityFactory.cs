using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFactory
{
    Dictionary<Entity.Name, EntityCreater> _entityCreater;

    public EntityFactory(
        Dictionary<Entity.Name, GameObject> entityPrefab, 
        Dictionary<Entity.Name, EntityData> towerData,
        ProjectileFactory projectileFactory,
        FlowField.PathTracker pathTracker)
    {
        _entityCreater = new Dictionary<Entity.Name, EntityCreater>()
        {
            {
                Entity.Name.GuidedMissileTower, new GuidedMissileTowerCreater(
                    entityPrefab[Entity.Name.GuidedMissileTower],
                    (GuidedMissileTowerData)towerData[Entity.Name.GuidedMissileTower],
                    projectileFactory
                )
            },
             {
                Entity.Name.Imp, new WalkUnitCreater(
                    entityPrefab[Entity.Name.Imp],
                    pathTracker,
                    (WalkUnitData)towerData[Entity.Name.Imp]
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
