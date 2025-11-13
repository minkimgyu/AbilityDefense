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

    public bool TryDetectTarget(out TargetCaptureComponent.Data data)
    {
        // 파괴된 오브젝트(Fake null) 제거
        _targetDatas.RemoveAll(data => data.CapturedTarget == null);

        if (_targetDatas.Count == 0)
        {
            data = new TargetCaptureComponent.Data(null);
            return false;
        }
        else
        {
            data = _targetDatas[0];
            return true;
        }
    }

    public bool TryDetectTargets(out List<TargetCaptureComponent.Data> datas) 
    {
        // 파괴된 오브젝트(Fake null) 제거
        _targetDatas.RemoveAll(data => data.CapturedTarget == null);

        if (_targetDatas.Count == 0)
        {
            datas = null;
            return false;
        }
        else
        {
            datas = _targetDatas;
            return true;
        }
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