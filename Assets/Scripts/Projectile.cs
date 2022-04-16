using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Impact.Utility.ObjectPool;

public class Projectile : PooledObject
{
    public GameObject explosionPrefab;

    private float lifetime;
    private LayerMask mask;
    private Rigidbody rb;
    private float damage;

    public float Lifetime { set { lifetime = value; } }
    public LayerMask Mask { set { mask = value; } }
    public Rigidbody Rb => rb;
    public float Damage { set { damage = value; } get { return damage; } }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        StartCoroutine(lifeTimer(lifetime));
    }

    private IEnumerator lifeTimer(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((mask.value & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
        {
            HealthSystem healthSystem = collision.gameObject.GetComponent<HealthSystem>();
            if (healthSystem != null)
            {
                healthSystem.TakeDamage(damage);
            }
            Explode();
        }
    }

    private void Explode()
    {
        if (explosionPrefab != null)
        {
            StartCoroutine(ExplosionRoutine());
        }
        gameObject.SetActive(false);
    }

    private IEnumerator ExplosionRoutine()
    {
        explosionPrefab.SetActive(true);
        yield return new WaitForEndOfFrame();
        explosionPrefab.SetActive(false);
    }
}
