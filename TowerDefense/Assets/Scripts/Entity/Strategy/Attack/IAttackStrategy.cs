using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackStrategy
{
    void InjectFirePoint(Transform firePoint1, Transform firePoint2) { }

    void Attack(List<TargetCaptureComponent.Data> datas) { }
    void Attack(TargetCaptureComponent.Data datas) { }
    void OnUpdate() { }
}
