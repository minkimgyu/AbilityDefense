using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoDetectStrategy : IDetectStrategy
{
    public TargetCaptureComponent.Data DetectTarget()
    {
        return default;
    }

    public List<TargetCaptureComponent.Data> DetectTargets()
    {
        return null;
    }

    public void InjectCaptureComponent(TargetCaptureComponent captureComponent)
    {
    }

    public void OnTargetEnter(TargetCaptureComponent.Data data)
    {
    }

    public void OnTargetExit(TargetCaptureComponent.Data data)
    {
    }
}
