using System.Collections.Generic;
using UnityEngine;

public class TargetDetectingStrategy : IDetectStrategy
{
    List<ITarget.Type> _targetTypes;
    List<TargetCaptureComponent.Data> _targetDatas;

    TargetCaptureComponent _targetCaptureComponent;

    public TargetDetectingStrategy(List<ITarget.Type> targetTypes)
    {
        _targetDatas = new List<TargetCaptureComponent.Data>();
        _targetTypes = targetTypes;
    }

    public TargetCaptureComponent.Data DetectTarget()
    {
        // 파괴된 오브젝트(Fake null) 제거
        _targetDatas.RemoveAll(data => data.CapturedTarget == null || data.CapturedDamageable == null);
        if(_targetDatas.Count == 0) return new TargetCaptureComponent.Data(null, null);

        return _targetDatas[0];
    }

    public List<TargetCaptureComponent.Data> DetectTargets() 
    {
        // 파괴된 오브젝트(Fake null) 제거
        _targetDatas.RemoveAll(data => data.CapturedTarget == null || data.CapturedDamageable == null);
        return _targetDatas; 
    }

    public void InjectCaptureComponent(TargetCaptureComponent captureComponent)
    {
        _targetCaptureComponent = captureComponent;
        _targetCaptureComponent.AddCaptureEvent(OnTargetEnter, OnTargetExit);
    }

    public void OnTargetEnter(TargetCaptureComponent.Data data)
    {
        // 확인은 Attack 클래스에서 진행
        if (data.CapturedTarget.IsTarget(_targetTypes) == false) return;
        _targetDatas.Add(data);
    }

    public void OnTargetExit(TargetCaptureComponent.Data data)
    {
        // 확인은 Attack 클래스에서 진행
        if (data.CapturedTarget.IsTarget(_targetTypes) == false) return;
        _targetDatas.Remove(data);
    }
}