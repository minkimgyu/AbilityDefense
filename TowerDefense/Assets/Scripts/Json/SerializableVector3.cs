using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UnityEngine.Vector3를 JSON 저장용으로 변환하기 위한 구조체.
/// </summary>
[Serializable]
public struct SerializableVector3
{
    [SerializeField, JsonProperty("x")] private float _x;
    [SerializeField, JsonProperty("y")] private float _y;
    [SerializeField, JsonProperty("z")] private float _z;

    public SerializableVector3(float x, float y, float z)
    {
        _x = x;
        _y = y;
        _z = z;
    }

    public SerializableVector3(Vector3 v)
    {
        _x = v.x;
        _y = v.y;
        _z = v.z;
    }

    [JsonIgnore] public float X => _x;
    [JsonIgnore] public float Y => _y;
    [JsonIgnore] public float Z => _z;

    [JsonIgnore]
    public Vector3 ToVector3 => new Vector3(_x, _y, _z);
}