using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityData
{
    protected Entity.Name _name;

    public EntityData(Entity.Name name)
    {
        _name = name;
    }
}

public class UnitData : EntityData
{
    BuffValue<float> _maxHp;
    BuffValue<float> _moveSpeed;

    public UnitData(Entity.Name name, BuffValue<float> maxHp, BuffValue<float> moveSpeed) : base(name)
    {
        _maxHp = maxHp;
        _moveSpeed = moveSpeed;
    }

    public BuffValue<float> MaxHp { get => _maxHp; }
    public BuffValue<float> MoveSpeed { get => _moveSpeed; }
}

public class TowerData : EntityData
{
    public TowerData(Entity.Name name) : base(name)
    {
    }
}

public class GuidedMissileTowerData : EntityData, IDataModifier
{
    IProjectile.Name _projectileName;
    BuffValue<float> _targetingRange;
    BuffValue<float> _explosionDamage;
    BuffValue<float> _explosionRange;
    BuffValue<float> _attackRate;

    public GuidedMissileTowerData(
        Entity.Name name,
        IProjectile.Name projectileName,
        BuffValue<float> explosionDamage,
        BuffValue<float> targetingRange,
        BuffValue<float> explosionRange,
        BuffValue<float> attackRate) : base(name)
    {
        _explosionDamage = explosionDamage;
        _projectileName = projectileName;
        _targetingRange = targetingRange;
        _explosionRange = explosionRange;
        _attackRate = attackRate;
    }

    public void ApplyAttackDamage(float attack) { _explosionDamage.Value += attack; }

    public BuffValue<float> TargetingRange { get => _targetingRange; }
    public BuffValue<float> ExplosionDamage { get => _explosionDamage; }
    public BuffValue<float> ExplosionRange { get => _explosionRange; }
    public BuffValue<float> AttackRate { get => _attackRate; }
    public IProjectile.Name ProjectileName { get => _projectileName; }
}