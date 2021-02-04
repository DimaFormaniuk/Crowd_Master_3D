using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour, IDamageable
{
    [SerializeField] private float _bounceForce;
    [SerializeField] private float _bounceRadius;

    private Vector3 _moveDiraction;

    public bool ApplyDamage(Rigidbody rigidbody, float force)
    {
        Explosion();
        return true;
    }

    private void Explosion()
    {
        Collider[] overlapped = Physics.OverlapSphere(transform.position, _bounceRadius);

        for (int i = 0; i < overlapped.Length; i++)
        {
            Rigidbody rigidbodyTemp = overlapped[i].attachedRigidbody;
            if (rigidbodyTemp)
            {
                rigidbodyTemp.AddExplosionForce(_bounceForce, transform.position, _bounceRadius, 1f);
            }
        }
        Destroy(gameObject);
    }
}
