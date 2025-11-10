using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityCreater
{
    protected GameObject _entityPrefab;

    public EntityCreater(GameObject entityPrefab)
    {
        _entityPrefab = entityPrefab;
    }

    abstract public Entity Create(Entity.Name name);
}
