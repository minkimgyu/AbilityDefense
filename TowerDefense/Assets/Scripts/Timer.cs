using UnityEngine;

public class Timer
{
    public enum State
    {
        Ready,
        Running,
        Finish
    }

    State _state;
    float _elapsedTime;   // 경과 시간
    float _duration;      // 총 시간

    public State CurrentState
    {
        get
        {
            if (_state == State.Running && _elapsedTime >= _duration)
                _state = State.Finish;
            return _state;
        }
    }

    public float Ratio
    {
        get
        {
            if (_state == State.Ready) return 0f;
            return Mathf.Clamp01(_elapsedTime / _duration);
        }
    }

    public Timer()
    {
        _state = State.Ready;
        _elapsedTime = 0f;
        _duration = 0f;
    }

    public void Start(float duration)
    {
        if (_state != State.Ready) return;
        _state = State.Running;
        _elapsedTime = 0f;
        _duration = duration;
    }

    public void Reset()
    {
        _state = State.Ready;
        _elapsedTime = 0f;
    }

    // Update에서 매 프레임 호출
    public void Update(float deltaTime)
    {
        if (_state != State.Running) return;

        _elapsedTime += deltaTime;
        if (_elapsedTime >= _duration) _state = State.Finish;
    }
}
