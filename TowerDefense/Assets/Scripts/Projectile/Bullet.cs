using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IProjectile
{
    float _hitDistance = 0.5f;
    float _damage;
    float _speed;

    float GetDistanceBetweenTarget(Vector3 targetPos)
    {
        return Vector3.Distance(transform.position, targetPos);
    }

    public void Fire(Vector3 startPos, Quaternion startQuaternion, ITarget target, IDamageable damageable)
    {
        float distance = GetDistanceBetweenTarget(target.GetTransform().position);
        if (distance > _hitDistance)
        {
            // 계속 날라감
            transform.position = Vector3.MoveTowards(transform.position, target.GetTransform().position, _speed);
        }
        else
        {
            // 데미지 주기
            damageable.SetDamage(_damage);
        }
    }

    public GameObject GetObject()
    {
        return gameObject;
    }
}