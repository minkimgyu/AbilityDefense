using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardUIFactory
{
    GameObject _cardUIPrefab;
    Dictionary<CardData.Name, Sprite> _cardIconSprites;

    public CardUIFactory(GameObject cardUI, Dictionary<CardData.Name, Sprite> cardIconSprites)
    {
        _cardUIPrefab = cardUI;
        _cardIconSprites = cardIconSprites;
    }

    public ISpawnableUI Create(CardData.Name cardName, CardData cardData)
    {
        GameObject cardGO = Object.Instantiate(_cardUIPrefab);
        ISpawnableUI ui = cardGO.GetComponent<ISpawnableUI>();
        ui.Initialize(_cardIconSprites[cardName], cardData);

        return ui;
    }
}