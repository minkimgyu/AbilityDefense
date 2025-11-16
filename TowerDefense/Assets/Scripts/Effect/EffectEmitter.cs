using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectEmitter : MonoBehaviour, IEffect
{
    ParticleSystem[] _particleSystems;

    bool _initialized = false;

    // ---------------------------------------------------------
    // 1. 파티클 초기화
    // ---------------------------------------------------------
    public void Initialize()
    {
        _particleSystems = GetComponentsInChildren<ParticleSystem>();
        _initialized = true;
    }

    // ---------------------------------------------------------
    // 2. 모든 파티클 종료시 파괴
    // ---------------------------------------------------------
    void Update()
    {
        if (!_initialized) return;

        for (int i = 0; i < _particleSystems.Length; i++)
        {
            if (_particleSystems[i] != null && _particleSystems[i].IsAlive(true))
                return; // 하나라도 살아있으면 계속 유지
        }

        Destroy(gameObject); // 모든 파티클 종료 → 오브젝트 파괴
    }

    // ---------------------------------------------------------
    // IEffect 구현부
    // ---------------------------------------------------------
    public void Play(Vector3 position)
    {
        transform.position = position;
        PlayAll();
    }

    public void Play(Vector3 position, Quaternion rotation)
    {
        transform.SetPositionAndRotation(position, rotation);
        PlayAll();
    }

    // 모든 파티클 재생
    void PlayAll()
    {
        if (!_initialized) return;

        foreach (var ps in _particleSystems)
        {
            if (ps != null)
                ps.Play(true);
        }
    }
}
