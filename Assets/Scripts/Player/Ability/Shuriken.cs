using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Shuriken : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _speedRotate;
    [SerializeField] private float _lifeTime;

    private float _damage;
    private Rigidbody _rigidbody;
    private Vector3 _diraction;

    public void Attack(Vector3 diraction, float damage)
    {
        _diraction = diraction;
        _damage = damage;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();

        _diraction = Vector3.zero;
    }

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.up * _speedRotate * Time.fixedDeltaTime);
        transform.position += (_speed * Time.fixedDeltaTime) * _diraction;
    }

    private void Update()
    {
        if (_lifeTime <= 0)
            Destroy(gameObject);
        _lifeTime -= Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
        {
            damageable.ApplyDamage(_rigidbody, _damage);
            Destroy(gameObject);
        }
    }
}
