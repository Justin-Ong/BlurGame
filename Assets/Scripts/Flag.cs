using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : Grabbable
{
    [SerializeField]
    private string team;

    public string Team => team;

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (other.gameObject.layer == 6 && isHeld && !isCoolingDown)
        {
            holdingPlayer.holdFlag(this);
            team = holdingPlayer.Team;
        }
    }
}
