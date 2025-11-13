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

    void Initialize(Missile.Data data) { }
    void Initialize(Bullet.Data data) { }

    void Fire(Vector3 startPos, Quaternion startQuaternion, ITarget target) { }
}
