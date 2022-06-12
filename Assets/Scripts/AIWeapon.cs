using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Impact.Utility.ObjectPool;
using CMF;

public class AIWeapon : Weapon
{
    public bool IsBusy => isCoolingDown || isReloading;

    protected override void Start()
    {
        if (projectilePool == null)
        {
            projectilePool = GetComponent<ProjectilePool>();
        }
        Assert.IsNotNull(projectilePool);

        currMagSize = magSize;
    }

    protected override void Update()
    {
        // do nothing
    }

    protected override void Shoot()
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

    public void AIShoot()
    {
        Shoot();
    }
}
