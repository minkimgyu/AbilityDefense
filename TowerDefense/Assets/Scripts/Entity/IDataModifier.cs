using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataModifier
{
    virtual void ApplyAttackDamage(float attack) { }
    virtual void ApplyAttackRate(float rate) { }
    virtual void ApplyTargetingRange(float range) { }
}