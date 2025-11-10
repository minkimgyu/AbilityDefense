using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFactory
{
    Dictionary<Entity.Name, EntityCreater> _towerCreater;

    public EntityFactory(
        Dictionary<Entity.Name, GameObject> entityPrefab, 
        Dictionary<Entity.Name, EntityData> towerData,
        ProjectileFactory projectileFactory)
    {
        _towerCreater = new Dictionary<Entity.Name, EntityCreater>()
        {
            { 
                Entity.Name.GuidedMissileTower, new GuidedMissileTowerCreater(
                    entityPrefab[Entity.Name.GuidedMissileTower], 
                    (GuidedMissileTowerData)towerData[Entity.Name.GuidedMissileTower],
                    projectileFactory
                ) 
            },
        };
    }

    public Entity Create(Entity.Name name)
    {
        Entity entity = _towerCreater[name].Create(name);
        entity.Initialize();
        return entity;
    }
}
