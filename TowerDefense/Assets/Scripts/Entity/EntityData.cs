using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityData
{
    [JsonProperty("name")]
    protected Entity.Name _name;

    [JsonProperty("myType")]
    protected ITarget.Type _myType;

    [JsonIgnore] public Entity.Name Name { get => _name; set => _name = value; }
    [JsonIgnore] public ITarget.Type MyType { get => _myType; set => _myType = value; }

    // ✔ Newtonsoft.Json이 필요한 기본 생성자 (반드시 있어야 함)
    [JsonConstructor]
    public EntityData(Entity.Name name, ITarget.Type myType)
    {
        Name = name;
        MyType = myType;
    }

    // 각 Data에서 반드시 override하여 Deep Copy를 구현해야 함
    public abstract EntityData Clone();
}

[Serializable]
public class WalkUnitData : EntityData, IDataModifier
{
    [JsonProperty("maxHp")]
    BuffValue<float> _maxHp;

    [JsonProperty("moveSpeed")]
    BuffValue<float> _moveSpeed;

    [JsonProperty("rotationSpeed")]
    private float _rotationSpeed;

    // ✔ Newtonsoft.Json이 필요한 기본 생성자 (반드시 있어야 함)
    [JsonConstructor]
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

    public WalkUnitData(WalkUnitData other) : base(other.Name, other.MyType)
    {
        _maxHp = new BuffValue<float>(other._maxHp);
        _moveSpeed = new BuffValue<float>(other._moveSpeed);
        _rotationSpeed = other._rotationSpeed;
    }

    public override EntityData Clone()
    {
        return new WalkUnitData(this);
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

    // ✔ Newtonsoft.Json이 필요한 기본 생성자 (반드시 있어야 함)
    [JsonConstructor]
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

    public GunnerData(GunnerData other) : base(other.Name, other.MyType)
    {
        _projectileName = other._projectileName;
        _targetTypes = new List<ITarget.Type>(other._targetTypes);

        _attackDamage = new BuffValue<float>(other._attackDamage);
        _targetingRange = new BuffValue<float>(other._targetingRange);
        _attackRate = new BuffValue<float>(other._attackRate);
        _projectileSpeed = new BuffValue<float>(other._projectileSpeed);

        _rotationSpeed = other._rotationSpeed;
    }

    public override EntityData Clone()
    {
        return new GunnerData(this);
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

    // ✔ Newtonsoft.Json이 필요한 기본 생성자 (반드시 있어야 함)
    [JsonConstructor]
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

    public BulletTowerData(BulletTowerData other) : base(other.Name, other.MyType)
    {
        _projectileName = other._projectileName;
        _targetTypes = new List<ITarget.Type>(other._targetTypes);

        _attackDamage = new BuffValue<float>(other._attackDamage);
        _targetingRange = new BuffValue<float>(other._targetingRange);
        _attackRate = new BuffValue<float>(other._attackRate);
        _projectileSpeed = new BuffValue<float>(other._projectileSpeed);

        _rotationSpeed = other._rotationSpeed;
    }

    public override EntityData Clone()
    {
        return new BulletTowerData(this);
    }

    public void ModifyAttackDamage(float attack) => _attackDamage.Value += attack;
    public void ModifyAttackRate(float rate) => _attackRate.Value += rate;
    public void ModifyTargetingRange(float range) => _targetingRange.Value += range;

    [JsonIgnore] public BuffValue<float> TargetingRange => _targetingRange;
    [JsonIgnore] public BuffValue<float> AttackDamage => _attackDamage;
    [JsonIgnore] public BuffValue<float> AttackRate => _attackRate;
    [JsonIgnore] public IProjectile.Name ProjectileName => _projectileName;
    [JsonIgnore] public List<ITarget.Type> TargetTypes => _targetTypes;
    [JsonIgnore] public BuffValue<float> ProjectileSpeed => _projectileSpeed;
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

    // ✔ Newtonsoft.Json이 필요한 기본 생성자 (반드시 있어야 함)
    [JsonConstructor]
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

    public GuidedMissileTowerData(GuidedMissileTowerData other) : base(other.Name, other.MyType)
    {
        _projectileName = other._projectileName;
        _targetTypes = new List<ITarget.Type>(other._targetTypes);

        _explosionDamage = new BuffValue<float>(other._explosionDamage);
        _explosionRange = new BuffValue<float>(other._explosionRange);
        _targetingRange = new BuffValue<float>(other._targetingRange);
        _attackRate = new BuffValue<float>(other._attackRate);
        _projectileSpeed = new BuffValue<float>(other._projectileSpeed);

        _rotationSpeed = other._rotationSpeed;
    }

    public override EntityData Clone()
    {
        return new GuidedMissileTowerData(this);
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