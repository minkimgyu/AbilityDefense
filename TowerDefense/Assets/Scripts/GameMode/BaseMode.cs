using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMode : MonoBehaviour
{
    public abstract void Initialize();

    private void Start()
    {
        Initialize();
    }
}
