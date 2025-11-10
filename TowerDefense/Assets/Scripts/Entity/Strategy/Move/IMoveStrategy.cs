using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveStrategy
{
    void RotateTo(Vector3 targetPosition) { }
    void InjectTurretTransform(Transform turretTransform) { }
    void OnUpdate() { }
}