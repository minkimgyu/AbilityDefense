using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectFactory
{
    Dictionary<IEffect.Name, GameObject> _effectPrefabs;

    public EffectFactory(Dictionary<IEffect.Name, GameObject> effectPrefabs)
    {
        _effectPrefabs = effectPrefabs;
    }

    public IEffect Create(IEffect.Name name)
    {
        GameObject effectGO = Object.Instantiate(_effectPrefabs[name]);
        IEffect effect = effectGO.GetComponent<IEffect>();
        effect.Initialize();
        return effect;
    }
}
