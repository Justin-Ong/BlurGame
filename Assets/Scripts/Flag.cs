using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    [SerializeField]
    private string team;

    public string Team { get; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6) {
            transform.parent = other.gameObject.GetComponent<Player>().flagHoldPosition;
            transform.localPosition = Vector3.zero;
        }
    }
}
