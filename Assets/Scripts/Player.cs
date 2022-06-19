using CMF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public Transform flagHoldPosition;

    public Transform weaponHoldPosition;
    
    [SerializeField]
    private GameObject[] weaponPrefabs;

    private GameObject[] weaponObjects;

    private Weapon[] weapons;

    private Flag heldFlag;

    private Rigidbody rb;
    private Collider col;

    private string team;

    private Inventory inventory;

    private int currWeaponIndex = 0;
    private Weapon currWeapon;

    public string Team => team;

    public bool holdingFlag => heldFlag != null;

    public float Speed => rb.velocity.magnitude;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        inventory = GetComponent<Inventory>();

        weaponObjects = new GameObject[weaponPrefabs.Length];
        weapons = new Weapon[weaponPrefabs.Length];
        for (int i = 0; i < weaponPrefabs.Length; i++)
        {
            GameObject weapon = Instantiate(weaponPrefabs[i]);
            weapon.transform.parent = weaponHoldPosition;
            weapon.transform.localPosition = Vector3.zero;
            weapon.GetComponent<ProjectilePool>().Init();
            weaponObjects[i] = weapon;
            weapons[i] = weapon.GetComponent<Weapon>();
            weapons[i].Hide();
        }

        UpdateCurrWeapon();
    }

    public void holdFlag(Flag flag)
    {
        if (heldFlag == null)
        {
            heldFlag = flag;
        }
    }

    private void Update()
    {
        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0)  // scroll up
        {
            if (holdingFlag)
            {
                ThrowFlag();
            }

            currWeaponIndex--;
            if (currWeaponIndex < 0)
            {
                currWeaponIndex = weaponObjects.Length - 1;
            }
            UpdateCurrWeapon();
        }
        if (Input.GetAxisRaw("Mouse ScrollWheel") < 0)  // scroll down
        {
            if (holdingFlag)
            {
                ThrowFlag();
            }

            currWeaponIndex++;
            if (currWeaponIndex >= weaponObjects.Length)
            {
                currWeaponIndex = 0;
            }
            UpdateCurrWeapon();
        }
        if (CharacterKeyboardInput.instance.IsFlagThrowKeyPressed() && holdingFlag)
        {
            ThrowFlag();
        }
        if (CharacterKeyboardInput.instance.IsThrowableKeyPressed())
        {
            UseThrowable();
        }
    }

    private void FixedUpdate()
    {

    }

    private void ThrowFlag()
    {
        heldFlag.Throw(currWeapon.transform.forward, rb.velocity);
        heldFlag = null;
    }

    private void UpdateCurrWeapon()
    {
        if (currWeapon != null)
        {
            currWeapon.Hide();
        }
        currWeapon = weapons[currWeaponIndex];
        currWeapon.Show();
    }

    private void UseThrowable()
    {
        Throwable throwable = inventory.GetThrowable();
        if (throwable != null)
        {
            throwable.transform.position = weaponHoldPosition.position;
            throwable.Throw(currWeapon.transform.forward, rb.velocity);
        }
    }
}
