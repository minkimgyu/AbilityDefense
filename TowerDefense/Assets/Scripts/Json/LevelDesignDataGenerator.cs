#if UNITY_EDITOR
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct SpawnPointField
{
    [SerializeField] private Transform _spawnPoint;                // 스폰 위치
    [SerializeField] private float _spawnRadius;               // 스폰 반경
    [SerializeField] private Color _spawnRadiusColor;               // 스폰 반경

    public float SpawnRadius => _spawnRadius;
    public Transform SpawnPoint => _spawnPoint;
    public Color SpawnRadiusColor => _spawnRadiusColor;
}

[Serializable]
public struct LevelDesignField
{
    [SerializeField] private float _spawnTime;                     // 생성 시간 (초 단위)
    [SerializeField] private int _spawnPointFieldIndex;                // 스폰 위치
    [SerializeField] private List<SpawnDetail> _enemyDetails; // 해당 위치에서 생성될 적 목록

    public float SpawnTime => _spawnTime;
    public int SpawnPointIndex => _spawnPointFieldIndex;
    public List<SpawnDetail> EnemyDetails => _enemyDetails;
}

public class LevelDesignDataGenerator : BaseDataGenerator<LevelDesignType>
{
    [Header("스폰 포인트 정보")]
    [SerializeField] private List<SpawnPointField> _spawnPointField = new List<SpawnPointField>();

    [Header("적 스폰 정보")]
    [SerializeField] private List<LevelDesignField> _spawnField = new List<LevelDesignField>();

    [ContextMenu("🔧 Generate Data")]
    public override void GenerateData()
    {
        List<SpawnInfo> spawnInfos = new List<SpawnInfo>();

        for (int i = 0; i < _spawnField.Count; i++)
        {
            spawnInfos.Add(new SpawnInfo(
                _spawnField[i].SpawnTime,
                _spawnPointField[_spawnField[i].SpawnPointIndex].SpawnPoint.position,
                _spawnPointField[_spawnField[i].SpawnPointIndex].SpawnRadius,
                _spawnField[i].EnemyDetails
            ));
        }

        LevelDesignData data = new LevelDesignData(spawnInfos);

        SaveToJson(data);
    }

    private void OnDrawGizmos()
    {
        if(_spawnPointField == null) return;
        if(_spawnPointField.Count == 0) return;

        for (int i = 0; i < _spawnPointField.Count; i++)
        {
            if(_spawnPointField[i].SpawnPoint == null) continue;
            Gizmos.color = _spawnPointField[i].SpawnRadiusColor;
            Gizmos.DrawWireSphere(_spawnPointField[i].SpawnPoint.position, _spawnPointField[i].SpawnRadius);
        }
    }
}
#endif
