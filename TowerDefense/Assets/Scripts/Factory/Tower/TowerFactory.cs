using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory
{
    Dictionary<Entity.Name, TowerCreater> _towerCreater;

    public TowerFactory(
        Dictionary<Entity.Name, GameObject> entityPrefab, 
        Dictionary<Entity.Name, EntityData> towerData,
        ProjectileFactory projectileFactory)
    {
        _towerCreater = new Dictionary<Entity.Name, TowerCreater>()
        {
            { 
                Entity.Name.GuidedMissileTower, new GuidedMissileTowerCreater(
                    entityPrefab[Entity.Name.GuidedMissileTower], 
                    towerData[Entity.Name.GuidedMissileTower],
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
