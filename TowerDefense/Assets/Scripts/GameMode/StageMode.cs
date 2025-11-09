using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMode : PlayMode
{
    [SerializeField] FlowField.GridComponent _gridComponent;
    [SerializeField] PlayerController _playerController;
    [SerializeField] CardController _cardController;

    FlowField.FlowField _flowField;

    public override void Initialize()
    {
        AddressableLoader addressableLoader = FindObjectOfType<AddressableLoader>();
        if (addressableLoader == null) return;

        _gridComponent.Initialize();
        _flowField = new FlowField.FlowField(_gridComponent);
        _playerController.Initialize(_gridComponent);

        CardUIFactory cardUIFactory = new CardUIFactory(
            addressableLoader.SpawnableUIAssets[ISpawnableUI.Name.CardUI],
            addressableLoader.CardIconSprites);




        _cardController.Initialize(addressableLoader.CardDataAssets, cardUIFactory);
        _cardController.InjectDragEvents(_playerController.OnCardDragStart, _playerController.OnCardDragEnd);
    }
}
