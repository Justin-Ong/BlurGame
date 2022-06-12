using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AITriggerRange : MonoBehaviour
{
    public List<Character> charactersInRange = new List<Character>();

    public UnityEvent OnCharacterEnter;
    public UnityEvent OnCharacterExit;

    public float triggerRange; 

    private void Awake()
    {
        triggerRange = GetComponent<SphereCollider>().radius;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            charactersInRange.Add(other.gameObject.GetComponent<Character>());
            OnCharacterEnter?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            Character exitingCharacter = other.gameObject.GetComponent<Character>();

            if (charactersInRange.Contains(exitingCharacter))
            {
                charactersInRange.Remove(exitingCharacter);
                OnCharacterExit?.Invoke();
            }
        }
    }
}
