using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITarget
{
    public enum Type
    {
        Enemy,
        Ally,
        All
    }

    bool IsTarget(ITarget.Type type);

    Transform GetTransform();
}
