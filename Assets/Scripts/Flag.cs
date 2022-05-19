using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    [SerializeField]
    private float throwForce = 10f;
    
    [SerializeField]
    private string team;

    [SerializeField]
    private Collider flagTrigger;
    [SerializeField]
    private Collider floorCollider;

    [SerializeField]
    private float cooldownTime = 0.25f;

    public string Team { get; }

    private Rigidbody rb;
    private Player holdingPlayer;
    private bool isHeld = false;
    private bool isCoolingDown = false;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6 && !isHeld && !isCoolingDown) {
            holdingPlayer = other.gameObject.GetComponent<Player>();
            holdingPlayer.holdFlag(this);
            transform.parent = holdingPlayer.flagHoldPosition;
            isHeld = true;
            DisableColliders();
            DisableRigidbody();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (holdingPlayer != null && holdingPlayer == other.gameObject.GetComponent<Player>()) {
            holdingPlayer = null;
            transform.parent = null;
            isHeld = false;
            StartCooldown();
            EnableRigidbody();
        }
    }

    public void Throw(Vector3 direction, Vector3 playerVelocity)
    {
        StartCooldown();
        EnableRigidbody();
        holdingPlayer = null;
        transform.parent = null;
        isHeld = false;
        transform.position += direction.normalized;
        rb.velocity = direction * throwForce + playerVelocity;
    }

    private void DisableRigidbody()
    {
        rb.isKinematic = true;
    }

    private void EnableRigidbody()
    {
        rb.isKinematic = false;
    }
    
    private void EnableColliders()
    {
        flagTrigger.enabled = true;
        floorCollider.enabled = true;
    }

    private void DisableColliders()
    {
        flagTrigger.enabled = false;
        floorCollider.enabled = false;
    }

    private void StartCooldown()
    {
        isCoolingDown = true;
        StartCoroutine(CooldownRoutine());
    }

    private IEnumerator CooldownRoutine()
    {
        yield return new WaitForSeconds(cooldownTime);
        isCoolingDown = false;
        EnableColliders();
        yield return null;
    }
}
