using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectile
{
    public enum Name
    {
        Bullet,
        Missile
    }

    void Initialize(float damage, float speed) { }
    void Fire(Vector3 startPos, Quaternion startQuaternion, ITarget target) { }
    void Fire(Vector3 startPos, Quaternion startQuaternion, ITarget target, IDamageable damageable) { }
    GameObject GetObject();
}
