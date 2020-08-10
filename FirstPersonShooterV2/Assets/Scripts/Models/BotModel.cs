using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotModel : MonoBehaviour
{
    #region Fields

    public Action OnStateChanged = delegate { };

    [SerializeField] private Transform _transform;
    [SerializeField] private NavMeshAgent _agent;
    //[SerializeField] private Rigidbody _botBody;
    //[SerializeField] private Rigidbody _botHead;
    //[SerializeField] private Rigidbody _botWeapon;
    [SerializeField] private Animator _animator;

    [SerializeField] private float _health;
    [SerializeField] private float _damage;
    [SerializeField] private float _lookAroundTime;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;

    [SerializeField] private float _seingDistance;
    [SerializeField] private float _seingAngle;

    private BotState _botSate = BotState.None;

    private Transform _playerTransform;

    private float _dieTime = 2f;

    private float _localTimer = 0f;

    private int _currentPoint = 0;

    private bool _isStateEntered = false;

    private Dictionary<BotState, Action> _state;

    #endregion


    #region Propeties

    public BotState BotState 
    { 
        get => _botSate;
        set
        {
            if (_botSate != value)
            {
                _botSate = value;
                OnStateChanged.Invoke();
            }
        }
    }
    public Dictionary<BotState, Action> State { get => _state; }
    public List<Vector3> Path { get; set; }
    public float DieTime { get => _dieTime; }
    public NavMeshAgent Agent { get => _agent; }

    #endregion


    #region UnityMethods

    private void Start()
    {
        _playerTransform = FindObjectOfType<PlayerModel>().transform;
    }

    #endregion


    #region Methods

    public void Execute()
    {
        if (_state == null)
        {
            StateIni();
        }
        State[_botSate].Invoke();
    }

    private void StateIni()
    {
        _state = new Dictionary<BotState, Action>();
        _state.Add(BotState.None, None);
        _state.Add(BotState.Patrul, Patrul);
        _state.Add(BotState.LookRight, LookRight);
        _state.Add(BotState.LookLeft, LookLeft);
        _state.Add(BotState.TargetFollow, TargetFollow);
        _state.Add(BotState.Die, Die);
    }

    public void SetDamage(float damage)
    {
        _health -= damage;
        if (_health <= 0f)
        {
            SwitchState(BotState.Die);
        }
    }

    public void SwitchState(BotState state)
    {
        _isStateEntered = false;
        _localTimer = 0f;
        BotState = state;
    }

    private void None()
    {
        LookAround();
    }

    private void LookAround()
    {
        _rotationSpeed *= Mathf.Sign(UnityEngine.Random.Range(-1f, 1f));
        if (_rotationSpeed < 0f)
        {
            SwitchState(BotState.LookLeft);
        }
        else
        {
            SwitchState(BotState.LookRight);
        }

    }

    private void LookRight()
    {
        if (_localTimer < _lookAroundTime)
        {
            _transform.Rotate(Vector3.up * _rotationSpeed * Time.deltaTime);
            _localTimer += Time.deltaTime;
        }
        else
        {
            SwitchState(BotState.Patrul);
        }

        if (IsPlayerHere())
        {
            SwitchState(BotState.TargetFollow);
        }
    }

    private void LookLeft()
    {
        if (_localTimer < _lookAroundTime)
        {
            _transform.Rotate(Vector3.up * _rotationSpeed * Time.deltaTime);
            _localTimer += Time.deltaTime;
        }
        else
        {
            SwitchState(BotState.Patrul);
        }

        if (IsPlayerHere())
        {
            SwitchState(BotState.TargetFollow);
        }
    }

    private void Die()
    {
        if (!_isStateEntered)
        {
            _agent.SetDestination(_transform.position);

            //_botBody.isKinematic = false;
            //_botHead.isKinematic = false;
            //_botWeapon.isKinematic = false;

            ServiceLocator.GetService<BotsController>().RemoveBot(this);

            //Destroy(gameObject, _dieTime);

            _isStateEntered = true;
        }
    }

    private void Patrul()
    {
        if (!_isStateEntered)
        {
            if (Path != null && Path.Count > 0)
            {
                _currentPoint++;
                if (_currentPoint >= Path.Count)
                {
                    _currentPoint = 0;
                }
                _agent.SetDestination(Path[_currentPoint]);
            }
            _isStateEntered = true;
            _agent.speed = _walkSpeed;
        }
        if (IsPlayerHere())
        {
            SwitchState(BotState.TargetFollow);
        }
        if (!_agent.hasPath && !_agent.pathPending)
        {
            LookAround();
        }
    }

    private void TargetFollow()
    {
        if (!_isStateEntered)
        {
            _agent.speed = _runSpeed;
            _isStateEntered = true;
        }
        if (IsPlayerHere())
        {
            _agent.SetDestination(_playerTransform.position);
        }
        else
        {
            SwitchState(BotState.Patrul);
        }
    }

    private bool IsPlayerHere()
    {
        bool res = false;

        if ((_playerTransform.position - _transform.position).sqrMagnitude < _seingDistance * _seingDistance)
        {
            if (Vector3.Angle(_transform.forward, _playerTransform.position - _transform.position) < _seingAngle)
            {
                if (!Physics.Linecast(_transform.position, _playerTransform.position))
                {
                    res = true;
                }
            }
        }

        return res;
    }



    #endregion
}
