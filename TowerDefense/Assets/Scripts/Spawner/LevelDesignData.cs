using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum LevelDesignType
{
    Test,
}

[Serializable]
public struct SpawnInfo
{
    [SerializeField, JsonProperty("spawnTime")]
    private float _spawnTime;

    [SerializeField, JsonProperty("spawnPoint")]
    private SerializableVector3 _spawnPoint;

    [SerializeField, JsonProperty("spawnRadius")]
    private float _spawnRadius;

    [SerializeField, JsonProperty("enemyDetails")]
    private List<SpawnDetail> _enemyDetails;

    public SpawnInfo(
        float spawnTime,
        Vector3 spawnPoint,
        float spawnRadius,
        List<SpawnDetail> enemyDetails)
    {
        _spawnTime = spawnTime;
        _spawnPoint = new SerializableVector3(spawnPoint);
        _spawnRadius = spawnRadius;
        _enemyDetails = enemyDetails;
    }

    [JsonIgnore] public float SpawnTime => _spawnTime;
    [JsonIgnore] public float SpawnRadius => _spawnRadius;
    [JsonIgnore] public Vector3 SpawnPoint => _spawnPoint.ToVector3;
    [JsonIgnore] public List<SpawnDetail> EnemyDetails => _enemyDetails;
}

[Serializable]
public struct SpawnDetail
{
    [SerializeField, JsonProperty("name")]
    private Entity.Name _name;

    [SerializeField, JsonProperty("count")]
    private int _count;

    public SpawnDetail(Entity.Name name, int count)
    {
        _name = name;
        _count = count;
    }

    [JsonIgnore] public Entity.Name Name => _name;
    [JsonIgnore] public int Count => _count;
}

[Serializable]
public struct LevelDesignData
{
    [SerializeField, JsonProperty("spawnInfos")]
    private List<SpawnInfo> _spawnInfos;

    public LevelDesignData(List<SpawnInfo> spawnInfos)
    {
        _spawnInfos = spawnInfos;
    }

    [JsonIgnore] public List<SpawnInfo> SpawnInfos => _spawnInfos;
}