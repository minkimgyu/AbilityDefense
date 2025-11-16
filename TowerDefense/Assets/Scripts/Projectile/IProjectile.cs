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

    void InjectEffectFactory(EffectFactory effectFactory) { }

    void SetData(Missile.Data data) { }
    void SetData(Bullet.Data data) { }

    void Fire(Vector3 startPos, Quaternion startQuaternion, ITarget target) { }
}
