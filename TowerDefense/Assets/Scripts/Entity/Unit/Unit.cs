using FlowField;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : Entity, IHealth, ITarget
{
    ITarget.Type _targetType;
    BuffValue<float> _hp;

    public void SetHP(BuffValue<float> hp)
    {
        _hp = hp;
    }

    public void SetDamage(float damage)
    {
        _hp.Value -= damage;
        if (_hp.Value <= 0)
        {
            SetState(LifeState.Dead);
            Destroy(gameObject);
        }
    }

    public void SetHeal(float healAmount)
    {
        _hp.Value += healAmount;
        if(_hp.Value > _hp.Max) _hp.Value = _hp.Max;
    }

    public virtual void SetState(ITarget.Type type)
    {
        _targetType = type;
    }

    public Transform GetTransform()
    {
        if (_lifeState == LifeState.Dead) return null;

        return transform;
    }

    public bool IsTarget(ITarget.Type type)
    {
        return _targetType == type || type == ITarget.Type.All;
    }

    public bool IsTarget(List<ITarget.Type> type)
    {
        return type.Contains(_targetType) || type.Contains(ITarget.Type.All);
    }
}
