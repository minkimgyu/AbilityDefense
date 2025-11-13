using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityData
{
    [JsonProperty("name")]
    protected Entity.Name _name;

    [JsonProperty("myType")]
    protected ITarget.Type _myType;

    [JsonIgnore] public Entity.Name Name { get => _name; set => _name = value; }
    [JsonIgnore] public ITarget.Type MyType { get => _myType; set => _myType = value; }

    public EntityData(Entity.Name name, ITarget.Type myType)
    {
        Name = name;
        MyType = myType;
    }
}

//public class UnitData : EntityData
//{
//    BuffValue<float> _maxHp;
//    BuffValue<float> _moveSpeed;

//    public UnitData(Entity.Name name, BuffValue<float> maxHp, BuffValue<float> moveSpeed) : base(name)
//    {
//        _maxHp = maxHp;
//        _moveSpeed = moveSpeed;
//    }

//    public BuffValue<float> MaxHp { get => _maxHp; }
//    public BuffValue<float> MoveSpeed { get => _moveSpeed; }
//}

//public class TowerData : EntityData
//{
//    public TowerData(Entity.Name name) : base(name)
//    {
//    }
//}

[Serializable]
public class WalkUnitData : EntityData, IDataModifier
{
    [JsonProperty("maxHp")]
    BuffValue<float> _maxHp;

    [JsonProperty("moveSpeed")]
    BuffValue<float> _moveSpeed;

    [JsonProperty("rotationSpeed")]
    private float _rotationSpeed;

    public WalkUnitData(
        Entity.Name name,
        ITarget.Type myType,
        BuffValue<float> maxHp,
        BuffValue<float> moveSpeed,
        float rotationSpeed) : base(name, myType)
    {
        _maxHp = maxHp;
        _moveSpeed = moveSpeed;
        _rotationSpeed = rotationSpeed;
    }

    public void ModifyMoveSpeed(float speed) { _moveSpeed.Value += speed; }

    [JsonIgnore] public BuffValue<float> MaxHp { get => _maxHp; }
    [JsonIgnore] public BuffValue<float> MoveSpeed { get => _moveSpeed; }
    [JsonIgnore] public float RotationSpeed { get => _rotationSpeed; }
}


[Serializable]
public class GunnerData : EntityData, IDataModifier
{
    [JsonProperty("projectileName")]
    private IProjectile.Name _projectileName;

    [JsonProperty("targetingRange")]
    private BuffValue<float> _targetingRange;

    [JsonProperty("attackDamage")]
    private BuffValue<float> _attackDamage;

    [JsonProperty("attackRate")]
    private BuffValue<float> _attackRate;

    [JsonProperty("targetTypes")]
    private List<ITarget.Type> _targetTypes;

    [JsonProperty("rotationSpeed")]
    private float _rotationSpeed;

    [JsonProperty("projectileSpeed")]
    private BuffValue<float> _projectileSpeed;

    public GunnerData(
        Entity.Name name,
        ITarget.Type myType,
        List<ITarget.Type> targetTypes,
        IProjectile.Name projectileName,
        BuffValue<float> attackDamage,
        BuffValue<float> targetingRange,
        BuffValue<float> attackRate,
        BuffValue<float> projectileSpeed,
        float rotationSpeed) : base(name, myType)
    {
        _targetTypes = targetTypes;
        _attackDamage = attackDamage;
        _projectileName = projectileName;
        _targetingRange = targetingRange;
        _attackRate = attackRate;
        _projectileSpeed = projectileSpeed;
        _rotationSpeed = rotationSpeed;
    }

    public void ModifyAttackDamage(float attack) => _attackDamage.Value += attack;
    public void ModifyAttackRate(float rate) => _attackRate.Value += rate;
    public void ModifyTargetingRange(float range) => _targetingRange.Value += range;

    [JsonIgnore] public BuffValue<float> TargetingRange => _targetingRange;
    [JsonIgnore] public BuffValue<float> AttackDamage => _attackDamage;
    [JsonIgnore] public BuffValue<float> AttackRate => _attackRate;
    [JsonIgnore] public IProjectile.Name ProjectileName => _projectileName;
    [JsonIgnore] public List<ITarget.Type> TargetTypes => _targetTypes;
    [JsonIgnore] public float RotationSpeed => _rotationSpeed;
    [JsonIgnore] public BuffValue<float> ProjectileSpeed => _projectileSpeed;
}

[Serializable]
public class BulletTowerData : EntityData, IDataModifier
{
    [JsonProperty("projectileName")]
    private IProjectile.Name _projectileName;

    [JsonProperty("targetingRange")]
    private BuffValue<float> _targetingRange;

    [JsonProperty("attackDamage")]
    private BuffValue<float> _attackDamage;

    [JsonProperty("attackRate")]
    private BuffValue<float> _attackRate;

    [JsonProperty("projectileSpeed")]
    private BuffValue<float> _projectileSpeed;

    [JsonProperty("targetTypes")]
    private List<ITarget.Type> _targetTypes;

    [JsonProperty("rotationSpeed")]
    private float _rotationSpeed;

    public BulletTowerData(
        Entity.Name name,
        ITarget.Type myType,
        List<ITarget.Type> targetTypes,
        IProjectile.Name projectileName,
        BuffValue<float> attackDamage,
        BuffValue<float> targetingRange,
        BuffValue<float> attackRate,
        BuffValue<float> projectileSpeed,
        float rotationSpeed) : base(name, myType)
    {
        _attackDamage = attackDamage;
        _projectileName = projectileName;
        _targetingRange = targetingRange;
        _attackRate = attackRate;
        _targetTypes = targetTypes;
        _rotationSpeed = rotationSpeed;
        _projectileSpeed = projectileSpeed;
    }

    public void ModifyAttackDamage(float attack) => _attackDamage.Value += attack;
    public void ModifyAttackRate(float rate) => _attackRate.Value += rate;
    public void ModifyTargetingRange(float range) => _targetingRange.Value += range;

    [JsonIgnore] public BuffValue<float> TargetingRange => _targetingRange;
    [JsonIgnore] public BuffValue<float> AttackDamage => _attackDamage;
    [JsonIgnore] public BuffValue<float> AttackRate => _attackRate;
    [JsonIgnore] public IProjectile.Name ProjectileName => _projectileName;
    [JsonIgnore] public List<ITarget.Type> TargetTypes => _targetTypes;
    [JsonIgnore] public BuffValue<float> FireSpeed => _projectileSpeed;
    [JsonIgnore] public float RotationSpeed => _rotationSpeed;
}

[Serializable]
public class GuidedMissileTowerData : EntityData, IDataModifier
{
    [JsonProperty("projectileName")]
    private IProjectile.Name _projectileName;

    [JsonProperty("targetTypes")]
    private List<ITarget.Type> _targetTypes;

    [JsonProperty("targetingRange")]
    private BuffValue<float> _targetingRange;

    [JsonProperty("explosionDamage")]
    private BuffValue<float> _explosionDamage;

    [JsonProperty("explosionRange")]
    private BuffValue<float> _explosionRange;

    [JsonProperty("attackRate")]
    private BuffValue<float> _attackRate;

    [JsonProperty("projectileSpeed")]
    private BuffValue<float> _projectileSpeed;

    [JsonProperty("rotationSpeed")]
    private float _rotationSpeed;

    public GuidedMissileTowerData(
        Entity.Name name,
        ITarget.Type myType,
        List<ITarget.Type> targetTypes,
        IProjectile.Name projectileName,
        BuffValue<float> explosionDamage,
        BuffValue<float> explosionRange,
        BuffValue<float> targetingRange,
        BuffValue<float> attackRate,
        BuffValue<float> projectileSpeed,
        float rotationSpeed) : base(name, myType)
    {
        _targetTypes = targetTypes;
        _explosionDamage = explosionDamage;
        _projectileName = projectileName;
        _targetingRange = targetingRange;
        _explosionRange = explosionRange;
        _attackRate = attackRate;
        _projectileSpeed = projectileSpeed;
        _rotationSpeed = rotationSpeed;
    }

    public void ModifyAttackDamage(float attack) => _explosionDamage.Value += attack;
    public void ModifyAttackRate(float rate) => _attackRate.Value += rate;
    public void ModifyTargetingRange(float range) => _targetingRange.Value += range;

    [JsonIgnore] public BuffValue<float> TargetingRange => _targetingRange;
    [JsonIgnore] public BuffValue<float> ExplosionDamage => _explosionDamage;
    [JsonIgnore] public BuffValue<float> ExplosionRange => _explosionRange;
    [JsonIgnore] public BuffValue<float> AttackRate => _attackRate;
    [JsonIgnore] public IProjectile.Name ProjectileName => _projectileName;
    [JsonIgnore] public List<ITarget.Type> TargetTypes => _targetTypes;
    [JsonIgnore] public float RotationSpeed => _rotationSpeed;
    [JsonIgnore] public BuffValue<float> ProjectileSpeed => _projectileSpeed;
}