using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    [SerializeField]
    protected float throwForce = 10f;

    [SerializeField]
    protected bool isThrowable = false;

    [SerializeField]
    protected Collider trigger;
    [SerializeField]
    protected Collider floorCollider;

    [SerializeField]
    protected float cooldownTime = 0.25f;

    protected Rigidbody rb;
    protected Player holdingPlayer;
    protected bool isHeld = false;
    protected bool isCoolingDown = false;

    public bool IsHeld => isHeld;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        EnableColliders();
    }

    private void LateUpdate()
    {
        if (holdingPlayer != null)
        {
            transform.localPosition = Vector3.zero;
        }
    }

    public virtual void Throw(Vector3 direction, Vector3 playerVelocity)
    {
        if (isThrowable)
        {
            StartCooldown();
            EnableRigidbody();
            holdingPlayer = null;
            transform.parent = null;
            isHeld = false;
            transform.position += direction.normalized;
            rb.velocity = direction * throwForce + playerVelocity;
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6 && !isHeld && !isCoolingDown)
        {
            holdingPlayer = other.gameObject.GetComponent<Player>();
            transform.parent = holdingPlayer.flagHoldPosition;
            isHeld = true;
            DisableColliders();
            DisableRigidbody();
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (holdingPlayer != null && holdingPlayer == other.gameObject.GetComponent<Player>())
        {
            holdingPlayer = null;
            transform.parent = null;
            isHeld = false;
            StartCooldown();
            EnableRigidbody();
        }
    }

    protected virtual void DisableRigidbody()
    {
        rb.isKinematic = true;
    }

    protected virtual void EnableRigidbody()
    {
        rb.isKinematic = false;
    }

    protected virtual void EnableColliders()
    {
        trigger.enabled = true;
        floorCollider.enabled = true;
    }

    protected virtual void DisableColliders()
    {
        trigger.enabled = false;
        floorCollider.enabled = false;
    }

    protected virtual void StartCooldown()
    {
        isCoolingDown = true;
        StartCoroutine(CooldownRoutine());
    }

    protected virtual IEnumerator CooldownRoutine()
    {
        yield return new WaitForSeconds(cooldownTime);
        isCoolingDown = false;
        EnableColliders();
        yield return null;
    }
}
