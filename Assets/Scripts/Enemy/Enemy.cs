using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public bool ApplyDamage(Rigidbody rigidbody, float force)
    {
        Debug.Log("box");
        return true;
    }
}
