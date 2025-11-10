using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using System;

public class SpawnableUIPrefabAssetLoader : MultiplePrafabAssetLoader<ISpawnableUI.Name>
{
    public SpawnableUIPrefabAssetLoader(AddressableLoader.Label label, Action<Dictionary<ISpawnableUI.Name, GameObject>, AddressableLoader.Label> OnComplete) : base(label, OnComplete)
    {
    }
}

public class EntityPrefabAssetLoader : MultiplePrafabAssetLoader<Entity.Name>
{
    public EntityPrefabAssetLoader(AddressableLoader.Label label, Action<Dictionary<Entity.Name, GameObject>, AddressableLoader.Label> OnComplete) : base(label, OnComplete)
    {
    }
}

abstract public class MultiplePrafabAssetLoader<Key> : MultipleAssetLoader<Key, GameObject, GameObject>
{
    protected MultiplePrafabAssetLoader(AddressableLoader.Label label, Action<Dictionary<Key, GameObject>, AddressableLoader.Label> OnComplete) : base(label, OnComplete)
    {
    }

    protected override void LoadAsset(IResourceLocation location, Dictionary<Key, GameObject> dictionary, Action OnComplete)
    {
        Addressables.LoadAssetAsync<GameObject>(location).Completed +=
        (handle) =>
        {
            switch (handle.Status)
            {
                case AsyncOperationStatus.Succeeded:
                    Key key = (Key)Enum.Parse(typeof(Key), location.PrimaryKey);

                    dictionary.Add(key, handle.Result);
                    OnComplete?.Invoke();
                    break;

                case AsyncOperationStatus.Failed:
                    break;

                default:
                    break;
            }
        };
    }
}

abstract public class SinglePrafabAssetLoader : SingleAssetLoader<GameObject, GameObject>
{
    protected SinglePrafabAssetLoader(AddressableLoader.Label label, Action<GameObject, AddressableLoader.Label> OnComplete) : base(label, OnComplete)
    {
    }

    protected override void LoadAsset(GameObject value)
    {
        _asset = value;
    }
}