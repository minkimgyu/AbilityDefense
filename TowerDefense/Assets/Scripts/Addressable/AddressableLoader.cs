using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.SceneManagement;
using System;

public class AddressableLoader : MonoBehaviour
{
    public enum Label
    {
        CardJsonData,
        EntityJsonData,
        LevelDesignJsonData,

        CardIconSprite,
        Sound,

        SpawnableUIPrefab,
        EntityPrefab,
        ProjectilePrefab,
    }

    HashSet<BaseLoader> _assetLoaders;

    int _successCount;
    int _totalCount;
    Action OnCompleted;
    Action<float> OnProgress;

    public void Initialize(bool dontDestroyOnLoad = true)
    {
        if(dontDestroyOnLoad) DontDestroyOnLoad(gameObject);

        _successCount = 0;
        _totalCount = 0;
        _assetLoaders = new HashSet<BaseLoader>();
    }

    public void InjectProgressEvent(Action<float> OnProgress)
    {
        this.OnProgress = OnProgress;
    }

    public Dictionary<IEffect.Name, GameObject> EffectAssets { get; private set; }
    public Dictionary<ISpawnableUI.Name, GameObject> SpawnableUIPrefabAssets { get; private set; }
    public Dictionary<IProjectile.Name, GameObject> ProjectilePrefabAssets { get; private set; }
    public Dictionary<Entity.Name, GameObject> EntityPrefabAssets { get; private set; }
    public Dictionary<LevelDesignType, LevelDesignData> EnemySpawnDataAsset { get; private set; }


    public Dictionary<CardData.Name, CardData> CardDataAssets { get; private set; }
    public Dictionary<Entity.Name, EntityData> EntityDataAssets { get; private set; }



    public Dictionary<ISoundPlayable.SoundName, AudioClip> SoundAssets { get; private set; }
    public Dictionary<CardData.Name, Sprite> CardIconSprites { get; private set; }


    public void Load(Action OnCompleted)
    {
        _assetLoaders.Add(new CardDataAssetLoader(Label.CardJsonData, (value, label) => { CardDataAssets = value; OnSuccess(label); }));
        _assetLoaders.Add(new EntityDataAssetLoader(Label.EntityJsonData, (value, label) => { EntityDataAssets = value; OnSuccess(label); }));
        _assetLoaders.Add(new LevelDesignDataAssetLoader(Label.LevelDesignJsonData, (value, label) => { EnemySpawnDataAsset = value; OnSuccess(label); }));

        _assetLoaders.Add(new ProjectilePrefabAssetLoader(Label.ProjectilePrefab, (value, label) => { ProjectilePrefabAssets = value; OnSuccess(label); }));
        _assetLoaders.Add(new SpawnableUIPrefabAssetLoader(Label.SpawnableUIPrefab, (value, label) => { SpawnableUIPrefabAssets = value; OnSuccess(label); }));
        _assetLoaders.Add(new EntityPrefabAssetLoader(Label.EntityPrefab, (value, label) => { EntityPrefabAssets = value; OnSuccess(label); }));

        _assetLoaders.Add(new CardIconAssetLoader(Label.CardIconSprite, (value, label) => { CardIconSprites = value; OnSuccess(label); }));

        this.OnCompleted = OnCompleted;
        _totalCount = _assetLoaders.Count;
        foreach (var loader in _assetLoaders)
        {
            loader.Load();
        }
    }

    void OnSuccess(Label label)
    {
        _successCount++;
        Debug.Log(_successCount);
        Debug.Log(label.ToString() + "Success");

        OnProgress?.Invoke((float)_successCount / _totalCount);
        if (_successCount == _totalCount)
        {
            Debug.Log("Complete!");
            OnCompleted?.Invoke();
        }
    }

    public void Release()
    {
        foreach (var loader in _assetLoaders)
        {
            loader.Release();
        }
    }
}
