using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Hand Ability", menuName = "Player/Abilities/Shuriken", order = 51)]
public class ShurikenAbility : Ability
{
    [SerializeField] private Shuriken _shurikenTemplate;

    [SerializeField] private float _attackForce;
    [SerializeField] private int _countShuriken;
    [SerializeField] private float _angleOffset;

    public override event UnityAction AbilityEnded;

    public override void UseAbility(AttackState attack)
    {
        Vector3 spawnPoint = attack.transform.position;
        spawnPoint.y = 0;
        Vector3 forwardDirection = attack.transform.TransformVector(Vector3.forward);

        Attack(attack, spawnPoint, forwardDirection);

        attack.Rigidbody.velocity = Vector3.zero;
        AbilityEnded?.Invoke();
    }

    private void Attack(AttackState state, Vector3 point, Vector3 direction)
    {
        Instantiate(_shurikenTemplate, point, Quaternion.identity).Attack(direction, _attackForce);

        for (int i = 1; i <= _countShuriken; i++)
        {
            Vector3 left = Quaternion.AngleAxis(_angleOffset * i, Vector3.up) * direction;
            Vector3 right = Quaternion.AngleAxis(-_angleOffset * i, Vector3.up) * direction;

            Instantiate(_shurikenTemplate, point, Quaternion.identity).Attack(left, _attackForce);
            Instantiate(_shurikenTemplate, point, Quaternion.identity).Attack(right, _attackForce);
        }
    }
}