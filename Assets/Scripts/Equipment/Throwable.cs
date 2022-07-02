using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    [SerializeField]
    private string throwableName;

    [SerializeField]
    private float throwForce = 10f;

    [SerializeField]
    private float lifetime = 5f;

    private Rigidbody rb;
    private Collider col;
    private Projectile proj;

    public Collider Col => col;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        proj = GetComponent<Projectile>();
    }

    private void OnDisable()
    {
        Destroy(gameObject);
    }

    public void Throw(Vector3 direction, Vector3 playerVelocity)
    {
        transform.parent = null;
        transform.position += direction.normalized;
        rb.velocity = direction * throwForce + playerVelocity;
        proj.Lifetime = lifetime;
        proj.ForceStartTimer();
        EnableRigidbody();
        gameObject.SetActive(true);
    }

    protected virtual void DisableRigidbody()
    {
        rb.isKinematic = true;
    }

    protected virtual void EnableRigidbody()
    {
        rb.isKinematic = false;
    }
}
