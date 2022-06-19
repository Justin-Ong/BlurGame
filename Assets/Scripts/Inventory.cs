using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> deployablePrefabs = new List<GameObject>();

    [SerializeField]
    private List<GameObject> throwablePrefabs = new List<GameObject>();

    [SerializeField]
    private List<GameObject> activatablePrefabs = new List<GameObject>();

    [SerializeField]
    private List<GameObject> passivePrefabs = new List<GameObject>();

    private List<GameObject> deployableObjects = new List<GameObject>();
    private List<GameObject> throwableObjects = new List<GameObject>();
    private List<GameObject> activatableObjects = new List<GameObject>();
    private List<GameObject> passiveObjects = new List<GameObject>();

    // true = in inventory, false = used
    private Dictionary<Deployable, bool> deployables = new Dictionary<Deployable, bool>();
    private Dictionary<Throwable, bool> throwables = new Dictionary<Throwable, bool>();
    private Dictionary<Activatable, bool> activatables = new Dictionary<Activatable, bool>();
    private Dictionary<Passive, bool> passives = new Dictionary<Passive, bool>();

    private void Awake()
    {
        for (int i = 0; i < deployablePrefabs.Count; i++)
        {
            GameObject newDeployableObject = Instantiate(deployablePrefabs[i], transform);
            newDeployableObject.transform.SetParent(null);

            deployableObjects.Add(newDeployableObject);
            Deployable newDeployable = newDeployableObject.GetComponent<Deployable>();
            deployables.Add(newDeployable, true);

            Physics.IgnoreCollision(GetComponent<Collider>(), newDeployable.Col);

            newDeployableObject.SetActive(false);
        }
        for (int i = 0; i < throwablePrefabs.Count; i++)
        {
            GameObject newThrowableObject = Instantiate(throwablePrefabs[i], transform);
            newThrowableObject.transform.SetParent(null);

            throwableObjects.Add(newThrowableObject);
            Throwable newThrowable = newThrowableObject.GetComponent<Throwable>();
            throwables.Add(newThrowable, true);

            Physics.IgnoreCollision(GetComponent<Collider>(), newThrowable.Col);

            newThrowableObject.SetActive(false);
        }
        for (int i = 0; i < activatablePrefabs.Count; i++)
        {
            GameObject newActivatableObject = Instantiate(activatablePrefabs[i], transform);
            newActivatableObject.SetActive(false);
            activatableObjects.Add(newActivatableObject);
            activatables.Add(newActivatableObject.GetComponent<Activatable>(), true);
        }
        for (int i = 0; i < passivePrefabs.Count; i++)
        {
            GameObject newPassiveObject = Instantiate(passivePrefabs[i], transform);
            newPassiveObject.SetActive(false);
            passiveObjects.Add(newPassiveObject);
            passives.Add(newPassiveObject.GetComponent<Passive>(), true);
        }
    }

    public Throwable GetThrowable()
    {
        Throwable result = null;

        if (throwables.Count > 0) {
            foreach (KeyValuePair<Throwable, bool> pair in throwables)
            {
                if (pair.Value == true)
                {
                    result = pair.Key;
                    throwables[result] = false;
                    break;
                }
            }
        }
        return result;
    }
}
