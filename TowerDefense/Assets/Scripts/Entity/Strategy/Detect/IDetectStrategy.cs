using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDetectStrategy
{
    void InjectCaptureComponent(TargetCaptureComponent captureComponent);

    bool TryDetectTarget(out TargetCaptureComponent.Data data);
    bool TryDetectTargets(out List<TargetCaptureComponent.Data> datas);

    void OnTargetEnter(TargetCaptureComponent.Data data);
    void OnTargetExit(TargetCaptureComponent.Data data);
}
