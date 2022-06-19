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

    public Collider Col => col;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        GetComponent<Projectile>().Lifetime = lifetime;
    }

    public void Throw(Vector3 direction, Vector3 playerVelocity)
    {
        gameObject.SetActive(true);
        EnableRigidbody();
        transform.parent = null;
        transform.position += direction.normalized;
        rb.velocity = direction * throwForce + playerVelocity;
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
