using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlowField;

public class WalkUnit : Unit
{
    [SerializeField] Animator _animator;

    public override void Initialize()
    {
        _moveStrategy.InjectAnimator(_animator);
    }

    public override void OnUpdate()
    {
    }
}
