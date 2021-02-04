using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStateMachine : BaseStateMachine
{
    [SerializeField] private State _firstState;

    private State _currentState;

    public event UnityAction Damage;

    override protected void OnDied()
    {
        enabled = false;
        Animator.SetTrigger("broken");
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
    }

    private void Start()
    {
        _currentState = _firstState;
        _currentState.Enter(Rigidbody, Animator);
    }

    private void Update()
    {
        if (_currentState == null)
            return;

        State nextState = _currentState.GetNextState();

        if (nextState != null)
            Transit(nextState);
    }

    private void Transit(State nextState)
    {
        if (_currentState != null)
            _currentState.Exit();

        _currentState = nextState;

        if (_currentState != null)
            _currentState.Enter(Rigidbody, Animator);
    }

    public void ApplyDamage(float damage)
    {
        Damage?.Invoke();
        Health.TakeDamage((int)damage);
    }
}
