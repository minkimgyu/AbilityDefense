using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : Entity, IDamageable, ITarget
{
    ITarget.Type _targetType;
    float _hp;

    public void SetDamage(float damage)
    {
        _hp -= damage;
        if (_hp <= 0) Destroy(gameObject);
    }

    public virtual void SetState(ITarget.Type type)
    {
        _targetType = type;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public bool IsTarget(ITarget.Type type)
    {
        return _targetType == type || type == ITarget.Type.All;
    }
}
