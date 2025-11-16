using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEffect
{
    [SerializeField]
    public enum Name
    {
       ExplosionEffect,
    }

    public void Initialize();

    virtual void Play(Vector3 position) { }
    virtual void Play(Vector3 position, Quaternion quaternion) { }
}
