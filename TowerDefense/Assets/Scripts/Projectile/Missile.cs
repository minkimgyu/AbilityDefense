using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour, IProjectile
{
    public struct Data
    {
        List<ITarget.Type> targetTypes;
        float circleRange;
        float damage;
        float speed;

        public Data(
            List<ITarget.Type> targetTypes,
            float circleRange,
            float damage,
            float speed)
        {
            this.targetTypes = targetTypes;
            this.circleRange = circleRange;
            this.damage = damage;
            this.speed = speed;
        }

        public List<ITarget.Type> TargetTypes => targetTypes;
        public float CircleRange => circleRange;
        public float Damage => damage;
        public float Speed => speed;
    }

    const float _hitDistance = 0.5f;
    const float _rotateSpeed = 300f;
    Data _data;

    public void Initialize(Missile.Data data) 
    {
        _data = data;
    }

    float GetDistance(Vector3 targetPos)
    {
        return Vector3.Distance(transform.position, targetPos);
    }

    public void Fire(Vector3 startPos, Quaternion startQuaternion, ITarget target)
    {
        transform.position = startPos;
        transform.rotation = startQuaternion;

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
            // 여기 폭발 데미지 기능 추가
            // 이팩트 추가
            RaycastHit[] hits = Physics.SphereCastAll(transform.position, _data.CircleRange, transform.forward, 1000);
            if (hits.Length == 0) return;

            for (int i = 0; i < hits.Length; i++)
            {
                ITarget hitTarget = hits[i].transform.GetComponent<ITarget>();
                bool isTarget = hitTarget.IsTarget(_data.TargetTypes);
                if (isTarget == false) continue;

                IDamageable hitDamageable = hits[i].transform.GetComponent<IDamageable>();
                hitDamageable.SetDamage(_data.Damage);
            }
        }
    }
}
