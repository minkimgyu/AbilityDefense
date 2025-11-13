using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IProjectile
{
    public struct Data
    {
        float damage;
        float speed;

        public Data(
            float damage,
           float speed)
        {
            this.damage = damage;
            this.speed = speed;
        }

        public float Damage => damage;
        public float Speed => speed;
    }

    const float _hitDistance = 0.5f;
    const float _rotateSpeed = 300f;
    Data _data;

    float GetDistance(Vector3 targetPos)
    {
        return Vector3.Distance(transform.position, targetPos);
    }

    public void Fire(Vector3 startPos, Quaternion startQuaternion, ITarget target)
    {
        float distance = GetDistance(target.GetTransform().position);
        if (distance > _hitDistance)
        {
            // 계속 날라감
            transform.position = Vector3.MoveTowards(transform.position, target.GetTransform().position, _data.Speed);

            // 목표 방향 계산
            Vector3 direction = (target.GetTransform().position - transform.position).normalized;

            // 목표를 향해 회전 (부드럽게)
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                _rotateSpeed * Time.deltaTime
            );
        }
        else
        {
            bool canSuccess = target.GetTransform().gameObject.TryGetComponent<IDamageable>(out IDamageable damageable);
            if(canSuccess == false) return;

            // 데미지 주기
            damageable.SetDamage(_data.Damage);
        }
    }
}