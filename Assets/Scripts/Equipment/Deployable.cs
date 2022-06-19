using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deployable : MonoBehaviour
{
    [SerializeField]
    private string deployableName;

    private Rigidbody rb;
    private Collider col;

    public Collider Col => col;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void Deploy()
    {
        gameObject.SetActive(true);
    }
}
