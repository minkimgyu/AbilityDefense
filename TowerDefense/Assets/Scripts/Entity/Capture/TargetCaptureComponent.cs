using System.Collections.Generic;
using UnityEngine;

public class TargetCaptureComponent : MonoBehaviour
{
    // IDamageable는 다른 컴포넌트에서 구현하기

    public struct Data
    {
        ITarget _capturedTarget;

        public Data(ITarget capturedTarget)
        {
            _capturedTarget = capturedTarget;
        }

        public ITarget CapturedTarget { get => _capturedTarget; }
    }

    List<ITarget.Type> _targetTypes;

    public event System.Action<Data> OnCaptureTarget;
    public event System.Action<Data> OnRemoveTarget;

    public void AddCaptureEvent(System.Action<Data> OnCaptureTarget, System.Action<Data> OnRemoveTarget)
    {
        this.OnCaptureTarget = OnCaptureTarget;
        this.OnRemoveTarget = OnRemoveTarget;
    }

    public void Initialize(List<ITarget.Type> targetTypes)
    {
        _targetTypes = targetTypes;
    }

    private void OnTriggerEnter(Collider other)
    {
        ITarget target = other.GetComponent<ITarget>();
        if(target == null) return;

        bool isTarget = target.IsTarget(_targetTypes);
        if (isTarget)
        {
            Data data = new Data(target);
            OnCaptureTarget?.Invoke(data);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ITarget target = other.GetComponent<ITarget>();
        if (target == null) return;

        bool isTarget = target.IsTarget(_targetTypes);
        if (isTarget)
        {
            Data data = new Data(target);
            OnCaptureTarget?.Invoke(data);
        }
    }
}
