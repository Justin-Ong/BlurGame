using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Impact.Utility.ObjectPool;

public class Weapon : MonoBehaviour
{
    public string weaponName;
    public float projectileSpeed;
    public float projectileDamage;
    public float projectileLifetime = 1f;
    public float inheritanceModifier = 1f;
    public Transform visualTransform;
    public LayerMask mask;
    public bool isAffectedByGravity;

    private Rigidbody playerRb;
    private ObjectPool<Projectile> projectilePool;
    private Camera mainCamera;


    private void Awake()
    {
        mainCamera = Camera.main;

        playerRb = GetComponentInParent<Rigidbody>();

        if (projectilePool == null)
        {
            projectilePool = GetComponent<ObjectPool<Projectile>>();
        }

        Assert.IsNotNull(projectilePool);
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = transform.position.z - mainCamera.transform.position.z;
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(mousePos);
        transform.LookAt(worldPos);

        if (Input.GetMouseButtonDown(0)) {
            Shoot();
        }
    }

    private void Shoot()
    {
        Projectile instance;
        if (projectilePool.GetObject(0, out instance)) {
            instance.transform.position = visualTransform.position;
            instance.transform.rotation = visualTransform.rotation;
            instance.Rb.velocity = transform.forward * projectileSpeed;
            instance.Rb.velocity += playerRb.velocity * inheritanceModifier;
            instance.gameObject.SetActive(true);
        }
    }
}
