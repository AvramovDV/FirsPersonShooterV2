using System;
using UnityEngine;


public class TimeController : BaseController
{
    #region Fields

    private Action _action;
    private float _time;
    private float _currentTime;
    private bool _repeating;

    #endregion


    #region Propeties

    public bool IsDone { get; set; } = false;

    #endregion


    #region ClassLifeCycles

    public TimeController(Action action, float time, bool repeating)
    {
        _action = action;
        _time = time;
        _repeating = repeating;
        _currentTime = _time;
        On();
    }

    #endregion


    #region Methods

    public override void On()
    {
        base.On();
        ServiceLocatorMonoBehaviour.GetService<GameController>().OnUpdate += Timer;
    }

    public override void Off()
    {
        base.Off();
        ServiceLocatorMonoBehaviour.GetService<GameController>().OnUpdate -= Timer;
    }

    private void Timer()
    {
        _currentTime -= Time.deltaTime;
        if (_currentTime <= 0f)
        {
            _action?.Invoke();
            if (_repeating)
            {
                _currentTime = _time;
            }
            else
            {
                Off();
            }
            IsDone = true;
        }
    }

    #endregion
}
