using System;
using UnityEngine;

public class TimeSystem
{
    private const float MINUTE_TIME = 1f;
    private const float FF_TIME = 0.003f;

    public Action OnMinuteChanged;
    public Action OnFastForwardEnd;

    private TimeSpan _startTime = new TimeSpan(1, 08, 00, 00);

    private float _sunriseHour=8;
    private float _sunsetHour=20;

    private TimeSpan _sunriseTime;
    private TimeSpan _sunsetTime;

    private float _timer;

    private float _minuteToRealTime;

    private TimeSpan _currentTime;
    public TimeSpan CurrentTime => _currentTime;


    private TimeSpan _timeToStop;



    // Start is called before the first frame update
    public TimeSystem()
    {
        // _sunInitialIntensity = _sunLight.intensity;
        _timer = _minuteToRealTime;
        _currentTime = TimeSpan.Zero + _startTime;
        _timeToStop = _currentTime;
        _sunriseTime = TimeSpan.FromHours(_sunriseHour);
        _sunsetTime = TimeSpan.FromHours(_sunsetHour);
    }

    // Update is called once per frame
    public void UpdateTime()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            _currentTime = _currentTime.Add(TimeSpan.FromMinutes(1));
            OnMinuteChanged?.Invoke();

            if (_currentTime.TotalMinutes >= _timeToStop.TotalMinutes)
            {
                _minuteToRealTime = MINUTE_TIME;
                _timeToStop = TimeSpan.MaxValue;
                OnFastForwardEnd?.Invoke();
            }
            _timer = _minuteToRealTime;
        }
    }

    public void FastForward(TimeSpan timeToStop)
    {
        GameManager.Instance.Resume();
        _timeToStop = _currentTime.Add(timeToStop);
        _minuteToRealTime = FF_TIME;
    }

    private void RotateSun()
    {
        float intensityMultiplier = 1;
        float timeofDay = (float)(CurrentTime.TotalDays - CurrentTime.Days);
        //_sunLight.transform.localRotation = Quaternion.Euler((timeofDay * 360f) - 90, 170, 0);
        if (timeofDay > _sunriseTime.TotalDays && timeofDay < _sunsetTime.TotalDays)
        {
            if (timeofDay <= _sunsetTime.TotalDays)
                intensityMultiplier = Mathf.Clamp01((timeofDay - ((float)_sunsetTime.TotalDays - 0.02f)) * (1 / 0.02f));
            if (timeofDay >= _sunriseTime.TotalDays)
                intensityMultiplier = Mathf.Clamp01(1 - (timeofDay - ((float)_sunriseTime.TotalDays - 0.02f) * (1 / 0.02f)));
        }
        else
        {
            intensityMultiplier = 0;
        }
    }

    private TimeSpan CalculateTimeDifference(TimeSpan from, TimeSpan to)
    {
        TimeSpan diff = to - from;
        if (diff.TotalSeconds < 0)
        {
            diff += TimeSpan.FromHours(24);
        }

        return diff;
    }
}
