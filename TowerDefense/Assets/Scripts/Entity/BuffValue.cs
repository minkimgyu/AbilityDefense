using Newtonsoft.Json;
using System;

[Serializable]
public class BuffValue<T> where T : struct, IComparable<T>
{
    [JsonProperty("min")]
    private T _min;

    [JsonProperty("max")]
    private T _max;

    [JsonProperty("current")]
    private T _current;

    [JsonIgnore]
    public T Min
    {
        get => _min;
        set
        {
            _min = value;
            Clamp();
        }
    }

    [JsonIgnore]
    public T Max
    {
        get => _max;
        set
        {
            _max = value;
            Clamp();
        }
    }

    [JsonIgnore]
    public T Value
    {
        get => _current;
        set => _current = ClampValue(value);
    }

    public BuffValue() { }

    public BuffValue(T min, T max, T initial)
    {
        _min = min;
        _max = max;
        _current = ClampValue(initial);
    }

    // 🔥 깊은 복사 생성자 추가
    public BuffValue(BuffValue<T> other)
    {
        _min = other._min;
        _max = other._max;
        _current = other._current;
    }

    private T ClampValue(T value)
    {
        if (value.CompareTo(_min) < 0) return _min;
        if (value.CompareTo(_max) > 0) return _max;
        return value;
    }

    private void Clamp() => _current = ClampValue(_current);
}
