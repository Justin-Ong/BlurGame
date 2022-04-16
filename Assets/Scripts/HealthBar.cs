using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private MaterialPropertyBlock matBlock;
    private MeshRenderer meshRenderer;
    private Camera mainCamera;
    private HealthSystem healthSystem;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        matBlock = new MaterialPropertyBlock();
        healthSystem = GetComponentInParent<HealthSystem>();
    }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        meshRenderer.enabled = true;
        UpdateParams();
    }

    private void UpdateParams()
    {
        meshRenderer.GetPropertyBlock(matBlock);
        matBlock.SetFloat("_Fill", healthSystem.currHealth / healthSystem.maxHealth);
        meshRenderer.SetPropertyBlock(matBlock);
    }
}
