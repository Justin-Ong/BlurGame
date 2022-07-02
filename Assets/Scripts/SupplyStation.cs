using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplyStation : MonoBehaviour
{
    [SerializeField]
    private LayerMask mask;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");
        if ((mask.value & 1 << other.gameObject.layer) == 1 << other.gameObject.layer)
        {
            Inventory inv = other.GetComponent<Inventory>();
            inv.RefillConsumables();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
