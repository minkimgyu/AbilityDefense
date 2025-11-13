using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackStrategy
{
    void InjectFirePoint(List<Transform> firePoints) { }

    void Attack(List<TargetCaptureComponent.Data> datas) { }
    void Attack(TargetCaptureComponent.Data datas) { }
    void OnUpdate() { }
}
