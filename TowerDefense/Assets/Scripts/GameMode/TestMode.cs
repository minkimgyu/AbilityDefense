using FlowField;
using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMode : BaseMode
{
    [SerializeField] FlowField.GridComponent _gridComponent;
    [SerializeField] PlayerController _playerController;
    [SerializeField] CardController _cardController;
    [SerializeField] Transform _portal;
    [SerializeField] EnemySpawner _enemySpawner;
    [SerializeField] LevelDesignType _levelDesignType = LevelDesignType.Test;

    FlowField.FlowField _flowField;

    public override void Initialize()
    {
        AddressableLoader addressableLoader = FindObjectOfType<AddressableLoader>();
        if (addressableLoader == null) return;

        CardUIFactory cardUIFactory = new CardUIFactory(
            addressableLoader.SpawnableUIPrefabAssets[ISpawnableUI.Name.CardUI],
            addressableLoader.CardIconSprites
        );

        EffectFactory effectFactory = new EffectFactory(
            addressableLoader.EffectAssets
        );

        ProjectileFactory projectileFactory = new ProjectileFactory(
            addressableLoader.ProjectilePrefabAssets,
            effectFactory
        );

        _gridComponent.Initialize();
        _flowField = new FlowField.FlowField(_gridComponent);

        Debug.Log(_portal.position);
        _flowField.FindPath(_portal.position);

        FlowField.PathTracker pathTracker = new FlowField.PathTracker(_gridComponent);
        EntityFactory entityFactory = new EntityFactory(
            addressableLoader.EntityPrefabAssets,
            addressableLoader.EntityDataAssets,
            projectileFactory,
            pathTracker
        );

        _enemySpawner.Initialize(addressableLoader.EnemySpawnDataAsset[_levelDesignType], entityFactory);
        _playerController.Initialize(_gridComponent, entityFactory);

        _cardController.Initialize(addressableLoader.CardDataAssets, cardUIFactory);
        _cardController.InjectDragEvents(_playerController.OnCardDragStart, _playerController.OnCardDragEnd);
    }
}
