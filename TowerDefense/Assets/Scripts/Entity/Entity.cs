using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public enum Name
    {
        Gunner,
        GridMissileTower,
        BulletTower,
        GuidedMissileTower,
        ThrowTower,
        ProvocationWarrior,
        ProvocationGolem,
        SparkTower,
        LaserTower
    }

    public enum PlacementState
    {
        Ready, // 배치 준비 상태
        Planted, // 배치 완료 상태
    }

    public enum LifeState
    {
        Alive, // 생존 상태
        Dead // 사망 상태
    }

    public enum EntityState
    {
        Idle, // 일반 상태
        Groggy, // 기절 상태
    }

    IMoveStrategy _moveStrategy;
    IAttackStrategy _attackStrategy;

    protected LifeState _lifeState; // 생존 상태
    protected PlacementState _placementState; // 배치 상태
    protected EntityState _entityState; // 엔티티 상태 (일반, 기절)

    public void InjectStrategy(IMoveStrategy moveStrategy, IAttackStrategy attackStrategy)
    {
        _moveStrategy = moveStrategy;
        _attackStrategy = attackStrategy;
    }

    public virtual void SetState(LifeState state)
    {
        _lifeState = state;
    }

    public virtual void SetState(PlacementState state)
    {
        _placementState = state;
    }

    public virtual void SetState(EntityState state)
    {
        _entityState = state;
    }

    public abstract void Initialize();

    private void Update()
    {
        if(_lifeState == LifeState.Dead) return;
        if(_placementState == PlacementState.Ready) return;
        if(_entityState == EntityState.Groggy) return;

        _moveStrategy.OnUpdate();
        _attackStrategy.OnUpdate();
    }

    // Unit, Tower 구분하기
    // 전략으로 기능 구분
    // 이동(회전), 공격(딕셔너리로 업그레이드 구분)
}
