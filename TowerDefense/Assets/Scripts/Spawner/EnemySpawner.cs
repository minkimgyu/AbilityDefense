using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private LevelDesignData _spawnData;
    [SerializeField] private float _enemyMinDistance = 1.5f;

    private float _timeSinceStart;
    private int _currentSpawnIndex;
    private List<GameObject> _spawnedEnemies;

    bool _isInitialized = false;
    EntityFactory _entityFactory;

    public void Initialize(LevelDesignData spawnData, EntityFactory entityFactory)
    {
        _spawnData = spawnData;
        _entityFactory = entityFactory;
        _spawnedEnemies = new List<GameObject>();
        _currentSpawnIndex = 0;
        _timeSinceStart = 0f;

        _isInitialized = true;
    }

    private void Update()
    {
        if (_isInitialized == false) return;

        _timeSinceStart += Time.deltaTime;

        List<SpawnInfo> infos = _spawnData.SpawnInfos;
        int infoCount = infos.Count;

        while (_currentSpawnIndex < infoCount &&
               _timeSinceStart >= infos[_currentSpawnIndex].SpawnTime)
        {
            SpawnEnemies(infos[_currentSpawnIndex]);
            _currentSpawnIndex++;
        }
    }

    private void SpawnEnemies(SpawnInfo info)
    {
        List<SpawnDetail> details = info.EnemyDetails;
        int detailCount = details.Count;

        for (int d = 0; d < detailCount; d++)
        {
            SpawnDetail detail = details[d];
            for (int i = 0; i < detail.Count; i++)
            {
                if (TryGetValidSpawnPosition(info.SpawnPoint, info.SpawnRadius, out Vector3 spawnPos))
                {
                    Entity entity = _entityFactory.Create(detail.Name);
                    entity.SetPosition(spawnPos);
                    _spawnedEnemies.Add(entity.gameObject);
                }
                else
                {
                    Debug.LogWarning(
                        $"⚠️ [{detail.Name}] 스폰 실패: 유효한 위치를 찾지 못했습니다. (SpawnTime: {info.SpawnTime:F2}s)");
                }
            }
        }
    }

    /// <summary>
    /// 랜덤 스폰 위치를 시도하고, 성공 시 위치를 반환
    /// </summary>
    private bool TryGetValidSpawnPosition(Vector3 center, float radius, out Vector3 validPos)
    {
        const int maxTryCount = 20;

        for (int i = 0; i < maxTryCount; i++)
        {
            Vector2 randCircle = UnityEngine.Random.insideUnitCircle * radius;
            Vector3 candidate = center + new Vector3(randCircle.x, 0, randCircle.y);

            if (!IsOverlapping(candidate))
            {
                validPos = candidate;
                return true;
            }
        }

        validPos = Vector3.zero;
        return false;
    }

    /// <summary>
    /// 다른 적과의 거리 검사 (겹치는 경우 true)
    /// </summary>
    private bool IsOverlapping(Vector3 position)
    {
        int count = _spawnedEnemies.Count;
        for (int i = 0; i < count; i++)
        {
            GameObject enemy = _spawnedEnemies[i];
            if (enemy == null) continue;

            float distance = Vector3.Distance(position, enemy.transform.position);
            if (distance < _enemyMinDistance)
                return true;
        }
        return false;
    }
}
