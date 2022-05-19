using CMF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform flagHoldPosition;

    private Weapon weapon;

    private Flag heldFlag;

    private Rigidbody rb;

    public bool holdingFlag => heldFlag != null;

    private void Awake()
    {
        weapon = GetComponentInChildren<Weapon>();
        rb = GetComponent<Rigidbody>();
    }

    public void holdFlag(Flag flag)
    {
        if (heldFlag == null)
        {
            heldFlag = flag;
        }
    }

    public void FixedUpdate()
    {
        if (CharacterKeyboardInput.instance.IsFlagThrowKeyPressed() && holdingFlag)
        {
            ThrowFlag();
        }
    }

    private void ThrowFlag()
    {
        heldFlag.Throw(weapon.transform.forward, rb.velocity);
        heldFlag = null;
    }
}
