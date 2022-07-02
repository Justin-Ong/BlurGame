using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Serializable]
    public struct Pair<K, V>
    {
        [field: SerializeField] public K Key { get; set; }
        [field: SerializeField] public V Value { get; set; }
    }

    [SerializeField]
    private Pair<GameObject, int> deployablePrefab = new Pair<GameObject, int>();

    [SerializeField]
    private Pair<GameObject, int> throwablePrefab = new Pair<GameObject, int>();

    [SerializeField]
    private Pair<GameObject, int> activatablePrefab = new Pair<GameObject, int>();

    [SerializeField]
    private Pair<GameObject, int> passivePrefab = new Pair<GameObject, int>();

    private int numDeployablesUsed;
    private int numThrowablesUsed;

    private void Awake()
    {
        
    }

    public Throwable GetThrowable()
    {
        if (numThrowablesUsed < throwablePrefab.Value) {
            numThrowablesUsed++;
            return Instantiate(throwablePrefab.Key, transform).GetComponent<Throwable>();
        }
        return null;
    }

    public Deployable GetDeployable()
    {
        if (numDeployablesUsed < deployablePrefab.Value)
        {
            numDeployablesUsed++;
            return Instantiate(deployablePrefab.Key, transform).GetComponent<Deployable>();
        }
        return null;
    }

    public void RefillConsumables()
    {
        numDeployablesUsed = 0;
        numThrowablesUsed = 0;
    }
}
