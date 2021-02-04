using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Animator), typeof(HealthContainer))]
public abstract class BaseStateMachine : MonoBehaviour
{
    public Rigidbody Rigidbody { get; private set; }
    public Animator Animator { get; private set; }
    public HealthContainer Health { get; private set; }

    protected void Init()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Animator = GetComponent<Animator>();
        Health = GetComponent<HealthContainer>();
    }

    private void OnEnable()
    {
        Health.Died += OnDied;
        Enable();
    }

    public abstract void Enable();

    private void OnDisable()
    {
        Health.Died -= OnDied;
        Disable();
    }

    public abstract void Disable();

    protected abstract void OnDied();
}
