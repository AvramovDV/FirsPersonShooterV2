using System;
using System.Collections.Generic;
using UnityEngine;


public class BotAnimationModel : MonoBehaviour
{
    #region Fields

    [SerializeField] private Animator _animator;
    [SerializeField] private BotModel _botModel;

    private string _animJustWalk = "JustWalk";
    private string _animWalkToTarget = "WalkToTarget";
    private string _animAttack = "Attack";
    private string _animTurnLeft = "TurnLeft";
    private string _animTurnRight = "TurnRight";
    private string _animFallingBack = "FallingBack";

    private Dictionary<BotState, Action> _animationsMachine;

    #endregion


    #region UnityMethods

    private void Start()
    {
        Init();
    }

    private void OnEnable()
    {
        _botModel.OnStateChanged += SetAnimation;
    }

    private void OnDisable()
    {
        _botModel.OnStateChanged -= SetAnimation;
    }

    #endregion


    #region Methods

    private void Init()
    {
        _animationsMachine = new Dictionary<BotState, Action>();
        _animationsMachine.Add(BotState.Patrul, () => _animator.SetTrigger(_animJustWalk));
        _animationsMachine.Add(BotState.TargetFollow, () => _animator.SetTrigger(_animWalkToTarget));
        _animationsMachine.Add(BotState.LookLeft, () => _animator.SetTrigger(_animTurnLeft));
        _animationsMachine.Add(BotState.LookRight, () => _animator.SetTrigger(_animTurnRight));
        _animationsMachine.Add(BotState.Die, () => _animator.SetTrigger(_animFallingBack));
    }

    private void SetAnimation()
    {
        if (_animationsMachine == null)
        {
            Init();
        }
        if (_animationsMachine.ContainsKey(_botModel.BotState))
        {
            _animationsMachine[_botModel.BotState].Invoke();
        }
    }



    #endregion
}
