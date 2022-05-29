using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Impact.Utility.ObjectPool;
using CMF;

public class Weapon : MonoBehaviour
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

    [SerializeField]
    private Renderer[] renderers;

    private Rigidbody playerRb;
    private ProjectilePool projectilePool;
    private Camera mainCamera;
    private int currMagSize;
    private bool isCoolingDown;
    private bool isReloading = false;
    private bool isHidden = false;

    private void Start()
    {
        mainCamera = Camera.main;

        playerRb = GetComponentInParent<Rigidbody>();

        if (projectilePool == null)
        {
            projectilePool = GetComponent<ProjectilePool>();
        }
        Assert.IsNotNull(projectilePool);

        currMagSize = magSize;
    }

    private void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = transform.position.z - mainCamera.transform.position.z;
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(mousePos);
        transform.LookAt(worldPos);
        if (!isHidden)
        {
            if (CharacterKeyboardInput.instance.IsShootButtonPressed())
            {
                HandleShootButtonInput();
            }
            if (CharacterKeyboardInput.instance.IsReloadKeyPressed())
            {
                HandleReloadKeyInput();
            }
        }
    }


    private void HandleShootButtonInput()
    {
        if (currMagSize > 0 && !isReloading)
        {
            Shoot();
        }
        else if (currMagSize <= 0 && !isReloading)
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
        if (!isCoolingDown) {
            currMagSize--;
            Projectile instance;
            projectilePool.GetObject(0, out instance);
            instance.transform.position = visualTransform.position;
            instance.transform.rotation = visualTransform.rotation;
            instance.Rb.velocity = transform.forward * projectileSpeed;
            instance.Rb.velocity += playerRb.velocity * inheritanceModifier;
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

    public void Hide()
    {
        foreach (Renderer rend in renderers) {
            rend.enabled = false;
        }
        isHidden = true;
    }

    public void Show()
    {
        foreach (Renderer rend in renderers)
        {
            rend.enabled = true;
        }
        isHidden = false;
    }
}
