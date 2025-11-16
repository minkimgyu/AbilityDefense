using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarTestMode : BaseMode
{
    [SerializeField] Transform _portal;
    [SerializeField] EnemySpawner _enemySpawner;
    [SerializeField] LevelDesignType _levelDesignType = LevelDesignType.Test;

    [SerializeField] PathfinderForTilemap.AStarPathGrid _gridComponent;
    [SerializeField] PathfinderForTilemap.AStarPathfinder _pathfinder;

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
        _pathfinder.Initialize(_gridComponent);
        IPathTracker pathTracker = new AStarPathTracker(_portal, _gridComponent, _pathfinder);
        EntityFactory entityFactory = new EntityFactory(
            addressableLoader.EntityPrefabAssets,
            addressableLoader.EntityDataAssets,
            projectileFactory,
            pathTracker
        );

        _enemySpawner.Initialize(addressableLoader.EnemySpawnDataAsset[_levelDesignType], entityFactory);
        //_playerController.Initialize(_gridComponent, entityFactory);
        //_cardController.Initialize(addressableLoader.CardDataAssets, cardUIFactory);
        //_cardController.InjectDragEvents(_playerController.OnCardDragStart, _playerController.OnCardDragEnd);
    }
}
