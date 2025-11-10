using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDetectStrategy
{
    void InjectCaptureComponent(TargetCaptureComponent captureComponent);
    List<TargetCaptureComponent.Data> DetectTargets();
    TargetCaptureComponent.Data DetectTarget();

    void OnTargetEnter(TargetCaptureComponent.Data data);
    void OnTargetExit(TargetCaptureComponent.Data data);
}
