using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField]
    private AITriggerRange range;
    [SerializeField]
    private GameObject weaponPrefab;
    [SerializeField]
    private Transform weaponPos;

    private AIWeapon weapon;
    private Transform target;
    private float maxRange;

    private void OnEnable()
    {
        if (weapon == null)
        {
            GameObject temp = Instantiate(weaponPrefab);
            temp.transform.parent = weaponPos;
            temp.transform.localPosition = Vector3.zero;
            temp.GetComponent<ProjectilePool>().Init();
            weapon = temp.GetComponent<AIWeapon>();
        }
    }

    void Start()
    {
        maxRange = range.triggerRange + 1;
        range.OnCharacterEnter.AddListener(OnCharacterEnterRange);
        range.OnCharacterExit.AddListener(OnCharacterExitRange);
    }

    private void OnCharacterEnterRange()
    {
        if (target == null)
        {
            target = GetNewTarget();
        }
    }

    private void OnCharacterExitRange()
    {
        target = GetNewTarget();
    }

    private Transform GetNewTarget()
    {
        if (range.charactersInRange.Count > 0)
        {
            Transform closest = null;
            float closestDist = maxRange;
            for (int i = 0; i < range.charactersInRange.Count; i++)
            {
                Transform currTransform = range.charactersInRange[i].transform;
                float currDist = Vector3.Distance(transform.position, currTransform.position);
                if (currDist < closestDist)
                {
                    closestDist = currDist;
                    closest = currTransform;
                }
            }
            return closest;
        }
        else
        {
            return null;
        }
    }

    private void Update()
    {
        if (target != null)
        {
            weapon.transform.LookAt(target);
            if (!weapon.IsBusy) {
                weapon.Shoot();
            }
        }
    }
}
