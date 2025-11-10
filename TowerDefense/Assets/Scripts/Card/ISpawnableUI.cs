using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawnableUI
{
    public enum Name
    {
        CardUI,
    }

    virtual void Initialize(Sprite cardIcon, CardData cardData) { }

    GameObject GetObject();
}
