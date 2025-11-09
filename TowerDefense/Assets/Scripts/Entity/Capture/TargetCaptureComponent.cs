using UnityEngine;

public class TargetCaptureComponent : MonoBehaviour
{
    public struct Data
    {
        ITarget _capturedTarget;
        IDamageable _capturedDamageable;

        public Data(ITarget capturedTarget, IDamageable capturedDamageable)
        {
            _capturedTarget = capturedTarget;
            _capturedDamageable = capturedDamageable;
        }

        public ITarget CapturedTarget { get => _capturedTarget; }
        public IDamageable CapturedDamageable { get => _capturedDamageable; }
    }

    ITarget.Type _targetType;

    public event System.Action<Data> OnCaptureTarget;
    public event System.Action<Data> OnRemoveTarget;

    public void InjectCaptureEvent(System.Action<Data> OnCaptureTarget, System.Action<Data> OnRemoveTarget)
    {
        this.OnCaptureTarget = OnCaptureTarget;
        this.OnRemoveTarget = OnRemoveTarget;
    }

    public void Initialize(ITarget.Type targetType)
    {
        _targetType = targetType;
    }

    private void OnTriggerEnter(Collider other)
    {
        ITarget target = other.GetComponent<ITarget>();
        bool isTarget = target.IsTarget(_targetType);
        if (isTarget)
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            if(damageable == null)
            {
                Debug.LogError("TargetCaptureComponent: The captured target does not implement IDamageable.");
                return;
            }

            Data data = new Data(target, damageable);
            OnCaptureTarget?.Invoke(data);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ITarget target = other.GetComponent<ITarget>();
        bool isTarget = target.IsTarget(_targetType);
        if (isTarget)
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            if (damageable == null)
            {
                Debug.LogError("TargetCaptureComponent: The captured target does not implement IDamageable.");
                return;
            }

            Data data = new Data(target, damageable);
            OnCaptureTarget?.Invoke(data);
        }
    }
}
