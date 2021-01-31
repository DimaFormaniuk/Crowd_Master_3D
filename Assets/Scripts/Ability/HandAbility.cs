using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HandAbility : Ability
{
    [SerializeField] private float _attackForce;
    [SerializeField] private float _usefulTime;

    private AttackState _state;
    private Coroutine _coroutine;

    public override event UnityAction AbilityEnded;

    public override void UseAbility(AttackState attack)
    {
        if (_coroutine != null)
            Reset();

        _state = attack;

        _state.CollisionDetected += OnPlayerAttack;
    }

    private void OnPlayerAttack()
    {

    }

    private IEnumerator Attack(AttackState state)
    {
        float time = _usefulTime;

        while (time > 0)
        {
            state.Rigidbody.velocity = state.Rigidbody.velocity.normalized * _attackForce;
            time -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        Reset();
        AbilityEnded?.Invoke();
    }

    private void Reset()
    {
        _state.Rigidbody.velocity = Vector3.zero;
        _state.StopCoroutine(_coroutine);
        _coroutine = null;
        _state.CollisionDetected -= OnPlayerAttack;
    }
}
