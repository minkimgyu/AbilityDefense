using System;

public class BuffValue<T> where T : struct, IComparable<T>
{
    private T _min;
    private T _max;
    private T _current;

    public T Min
    {
        get => _min;
        set
        {
            _min = value;
            Clamp();
        }
    }

    public T Max
    {
        get => _max;
        set
        {
            _max = value;
            Clamp();
        }
    }

    public T Value
    {
        get => _current;
        set
        {
            _current = ClampValue(value);
        }
    }

    public BuffValue(T min, T max, T initial)
    {
        _min = min;
        _max = max;
        _current = ClampValue(initial);
    }

    private T ClampValue(T value)
    {
        if (value.CompareTo(_min) < 0) return _min;
        if (value.CompareTo(_max) > 0) return _max;
        return value;
    }

    private void Clamp() => _current = ClampValue(_current);
}