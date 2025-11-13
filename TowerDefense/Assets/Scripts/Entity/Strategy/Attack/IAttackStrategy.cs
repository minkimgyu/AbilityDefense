using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackStrategy
{
    void InjectFirePoint(List<Transform> firePoints) { }

    void Attack(List<TargetCaptureComponent.Data> targetDatas) { }
    void Attack(TargetCaptureComponent.Data targetData) { }
    void OnUpdate() { }
}
