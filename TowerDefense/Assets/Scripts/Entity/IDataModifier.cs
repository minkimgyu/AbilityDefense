using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataModifier
{
    virtual void ModifyAttackDamage(float attack) { }
    virtual void ModifyAttackRate(float rate) { }
    virtual void ModifyTargetingRange(float range) { }
    virtual void ModifyMoveSpeed(float speed) { }
}