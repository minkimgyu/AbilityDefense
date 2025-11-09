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
    public virtual void Initialize(float damage, float speed) { }
    public void Fire(ITarget target, IDamageable damageable);
}
