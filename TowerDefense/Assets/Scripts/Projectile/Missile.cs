using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour, IProjectile
{
    float _hitDistance = 0.5f;
    float _circleRange;
    float _damage;
    float _speed;
    ITarget.Type _targetType;

    float GetDistanceBetweenTarget(Vector3 targetPos)
    {
        return Vector3.Distance(transform.position, targetPos);
    }

    public void Fire(ITarget target, IDamageable damageable)
    {
        float distance = GetDistanceBetweenTarget(target.GetTransform().position);
        if (distance > _hitDistance)
        {
            // 계속 날라감
            transform.position = Vector3.MoveTowards(transform.position, target.GetTransform().position, _speed);
        }
        else
        {
            // 여기 폭발 데미지 기능 추가
            // 이팩트 추가
            RaycastHit[] hits = Physics.SphereCastAll(transform.position, _circleRange, transform.forward, 1000);
            if (hits.Length == 0) return;

            for (int i = 0; i < hits.Length; i++)
            {
                ITarget hitTarget = hits[i].transform.GetComponent<ITarget>();
                bool isTarget = hitTarget.IsTarget(_targetType);
                if (isTarget == false) continue;

                IDamageable hitDamageable = hits[i].transform.GetComponent<IDamageable>();
                hitDamageable.SetDamage(_damage);
            }
        }
    }
}
