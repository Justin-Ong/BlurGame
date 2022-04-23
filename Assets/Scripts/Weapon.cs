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
    public int magSize = 1;
    public float reloadTime = 1f;
    public Transform visualTransform;
    public LayerMask mask;
    public bool isAffectedByGravity;

    private Rigidbody playerRb;
    private ObjectPool<Projectile> projectilePool;
    private Camera mainCamera;
    private int currMagSize;
    private bool isReloading;

    private void Awake()
    {
        mainCamera = Camera.main;

        playerRb = GetComponentInParent<Rigidbody>();

        if (projectilePool == null)
        {
            projectilePool = GetComponent<ObjectPool<Projectile>>();
        }

        Assert.IsNotNull(projectilePool);

        currMagSize = magSize;
        isReloading = false;
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = transform.position.z - mainCamera.transform.position.z;
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(mousePos);
        transform.LookAt(worldPos);
        HandleShootButtonInput();
        HandleReloadKeyInput();
    }

    private void HandleShootButtonInput()
    {
        if (currMagSize > 0)
        {
            Shoot();
        }
        else if (!isReloading)
        {
            Reload();
        }
    }

    private void HandleReloadKeyInput()
    {
        if (!isReloading && currMagSize < magSize)
        {
            Reload();
        }
    }

    private void Shoot()
    {
        currMagSize--;
        Projectile instance;
        projectilePool.GetObject(0, out instance);
        instance.transform.position = visualTransform.position;
        instance.transform.rotation = visualTransform.rotation;
        instance.Rb.velocity = transform.forward * projectileSpeed;
        instance.Rb.velocity += playerRb.velocity * inheritanceModifier;
        instance.gameObject.SetActive(true);

        if (currMagSize <= 0)
        {
            Reload();
        }
    }

    private void Reload()
    {
        isReloading = true;
        StartCoroutine(ReloadRoutine());
    }

    private IEnumerator ReloadRoutine()
    {
        yield return new WaitForSeconds(reloadTime);
        currMagSize = magSize;
        isReloading = false;
        yield return null;
    }
}
