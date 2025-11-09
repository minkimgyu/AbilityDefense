using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowerCreater
{
    protected GameObject _entityPrefab;
    protected EntityData _data;

    public TowerCreater(GameObject entityPrefab, EntityData data)
    {
        _entityPrefab = entityPrefab;
        _data = data;
    }

    abstract public Entity Create(Entity.Name name);
}
