using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DamageData
{
    ITarget.Type _targetType;
    float _damageAmount;

    public DamageData(ITarget.Type targetType, float damageAmount)
    {
        _targetType = targetType;
        _damageAmount = damageAmount;
    }

    public ITarget.Type TargetType { get => _targetType; }
    public float DamageAmount { get => _damageAmount; }
}
