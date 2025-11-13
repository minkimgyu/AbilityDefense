using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoDetectStrategy : IDetectStrategy
{

    public void InjectCaptureComponent(TargetCaptureComponent captureComponent)
    {
    }

    public void OnTargetEnter(TargetCaptureComponent.Data data)
    {
    }

    public void OnTargetExit(TargetCaptureComponent.Data data)
    {
    }

    public bool TryDetectTarget(out TargetCaptureComponent.Data data)
    {
        data = new TargetCaptureComponent.Data(null);
        return false;
    }

    public bool TryDetectTargets(out List<TargetCaptureComponent.Data> datas)
    {
        datas = new List<TargetCaptureComponent.Data>();
        return false;
    }
}
