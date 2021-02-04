using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyStateMachine : BaseStateMachine, IDamageable
{
    [SerializeField] private EnemyState _firstState;
    [SerializeField] private BrokenState _brokenState;

    private EnemyState _currentState;
    private float _minDamage;

    public PlayerStateMachine Player { get; private set; }

    public event UnityAction<EnemyStateMachine> Died;

    protected override void OnDied()
    {
        enabled = false;
        Rigidbody.constraints = RigidbodyConstraints.None;
        Died?.Invoke(this);
    }

    public override void Enable()
    {
    }

    public override void Disable()
    {
    }

    private void Awake()
    {
        Init();
        Player = FindObjectOfType<PlayerStateMachine>();
    }

    private void Start()
    {
        _currentState = _firstState;
        _currentState.Enter(Rigidbody, Animator, Player);
    }

    private void Update()
    {
        if (_currentState == null)
            return;

        EnemyState nextState = _currentState.GetNextState();

        if (nextState != null)
            Transit(nextState);
    }

    private void Transit(EnemyState nextState)
    {
        if (_currentState != null)
            _currentState.Exit();

        _currentState = nextState;

        if (_currentState != null)
            _currentState.Enter(Rigidbody, Animator, Player);
    }

    public bool ApplyDamage(Rigidbody rigidbody, float force)
    {
        if (force > _minDamage && _currentState != _brokenState)
        {
            Health.TakeDamage((int)force);
            Transit(_brokenState);
            _brokenState.ApplyDamage(rigidbody, force);

            return true;
        }
        return false;
    }
}
