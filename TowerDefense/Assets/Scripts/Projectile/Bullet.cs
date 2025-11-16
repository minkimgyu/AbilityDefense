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

    EffectFactory _effectFactory;

    public void InjectEffectFactory(EffectFactory effectFactory) 
    {
        _effectFactory = effectFactory;
    }

    public void SetData(Bullet.Data data)
    {
        _data = data;
    }

    float GetDistance(Vector3 targetPos)
    {
        return Vector3.Distance(transform.position, targetPos);
    }

    ITarget _target;
    bool _nowFire = false;
    bool _nowHit = false;

    private void Update()
    {
        if (_nowFire == false) return;
        if (_nowHit == true) return;
        if (_target == null) return;

        Transform targetTransform = _target.GetTransform();
        if (targetTransform == null)
        {
            DestroySelf();
            return;
        }

        float distance = GetDistance(targetTransform.position);
        if (distance > _hitDistance)
        {
            FlyToTarget();
        }
        else
        {
            HitTarget();
        }
    }

    public void Fire(Vector3 startPos, Quaternion startQuaternion, ITarget target)
    {
        transform.position = startPos;
        transform.rotation = startQuaternion;
        _target = target;
        _nowFire = true;
    }

    void DestroySelf()
    {
        _nowHit = true;
        Destroy(gameObject);
    }

    void HitTarget()
    {
        bool canSuccess = _target.GetTransform().gameObject.TryGetComponent<IHealth>(out IHealth damageable);
        if (canSuccess == false) return;

        // 데미지 주기
        damageable.SetDamage(_data.Damage);
        DestroySelf();
        return;
    }

    void FlyToTarget()
    {
        // 계속 날라감
        transform.position = Vector3.MoveTowards(transform.position, _target.GetTransform().position, _data.Speed * Time.deltaTime);

        // 목표 방향 계산
        Vector3 direction = (_target.GetTransform().position - transform.position).normalized;

        // 목표를 향해 회전 (부드럽게)
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRotation,
            _rotateSpeed * Time.deltaTime
        );
    }
}