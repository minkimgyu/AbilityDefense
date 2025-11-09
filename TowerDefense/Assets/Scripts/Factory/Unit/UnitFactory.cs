using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitFactory
{
    Dictionary<Entity.Name, GameObject> _entityPrefab;

    public UnitFactory(Dictionary<Entity.Name, GameObject> entityPrefab)
    {
        _entityPrefab = entityPrefab;
    }

    public Entity Create(Entity.Name name)
    {
        GameObject entityGO = Object.Instantiate(_entityPrefab[name]);
        Entity entity = entityGO.GetComponent<Entity>();

        return entity;
    }
}
