using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Impact.Utility.ObjectPool;
using CMF;

public class AIWeapon : MonoBehaviour
{
    public string weaponName;
    public float projectileSpeed;
    public float projectileDamage;
    public float projectileLifetime = 1f;
    public float inheritanceModifier = 1f;
    public int magSize = 1;
    public float cooldownTime = 1f;
    public float reloadTime = 1f;
    public Transform visualTransform;
    public LayerMask mask;
    public bool isAffectedByGravity;

    private ProjectilePool projectilePool;
    private int currMagSize;
    private bool isCoolingDown = false;
    private bool isReloading = false;

    public bool IsBusy => isCoolingDown || isReloading;

    private void Start()
    {
        if (projectilePool == null)
        {
            projectilePool = GetComponent<ProjectilePool>();
        }
        Assert.IsNotNull(projectilePool);

        currMagSize = magSize;
    }

    public void Shoot()
    {
        if (!isCoolingDown)
        {
            currMagSize--;
            Projectile instance;
            projectilePool.GetObject(0, out instance);
            instance.transform.position = visualTransform.position;
            instance.transform.rotation = visualTransform.rotation;
            instance.Rb.velocity = transform.forward * projectileSpeed;
            instance.gameObject.SetActive(true);
            StartCooldown();
        }

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

    private void StartCooldown()
    {
        isCoolingDown = true;
        StartCoroutine(CooldownRoutine());
    }

    private IEnumerator CooldownRoutine()
    {
        yield return new WaitForSeconds(cooldownTime);
        isCoolingDown = false;
        yield return null;
    }
}
