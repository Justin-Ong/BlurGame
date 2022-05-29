using CMF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform flagHoldPosition;

    public Transform weaponHoldPosition;
    
    [SerializeField]
    private GameObject[] weaponPrefabs;

    private GameObject[] weaponObjects;

    private Weapon[] weapons;

    private Flag heldFlag;

    private Rigidbody rb;

    private string team;

    private int currWeaponIndex = 0;
    private Weapon currWeapon;

    public string Team => team;

    public bool holdingFlag => heldFlag != null;

    public float Speed => rb.velocity.magnitude;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

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
            currWeaponIndex--;
            if (currWeaponIndex < 0)
            {
                currWeaponIndex = weaponObjects.Length - 1;
            }
            UpdateCurrWeapon();
        }
        if (Input.GetAxisRaw("Mouse ScrollWheel") < 0)  // scroll down
        {
            currWeaponIndex++;
            if (currWeaponIndex >= weaponObjects.Length)
            {
                currWeaponIndex = 0;
            }
            UpdateCurrWeapon();
        }
    }

    private void FixedUpdate()
    {
        if (CharacterKeyboardInput.instance.IsFlagThrowKeyPressed() && holdingFlag)
        {
            ThrowFlag();
        }
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
}
