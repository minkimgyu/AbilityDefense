using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMode : BaseMode
{
    [SerializeField] FlowField.GridComponent _gridComponent;
    [SerializeField] PlayerController _playerController;
    [SerializeField] CardController _cardController;

    FlowField.FlowField _flowField;

    public override void Initialize()
    {
        AddressableLoader addressableLoader = FindObjectOfType<AddressableLoader>();
        if (addressableLoader == null) return;

        CardUIFactory cardUIFactory = new CardUIFactory(
            addressableLoader.SpawnableUIPrefabAssets[ISpawnableUI.Name.CardUI],
            addressableLoader.CardIconSprites
        );

        ProjectileFactory projectileFactory = new ProjectileFactory(
            addressableLoader.ProjectilePrefabAssets
        );

        EntityFactory entityFactory = new EntityFactory(
            addressableLoader.EntityPrefabAssets,
            addressableLoader.EntityDataAssets,
            projectileFactory
        );

        _gridComponent.Initialize();
        _flowField = new FlowField.FlowField(_gridComponent);
        _playerController.Initialize(_gridComponent, entityFactory);

        _cardController.Initialize(addressableLoader.CardDataAssets, cardUIFactory);
        _cardController.InjectDragEvents(_playerController.OnCardDragStart, _playerController.OnCardDragEnd);
    }
}
