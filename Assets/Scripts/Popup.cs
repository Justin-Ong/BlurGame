using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Popup : MonoBehaviour
{
    public string popupText;

    [SerializeField]
    private TextMeshProUGUI textBox;

    private void Awake()
    {
        textBox.text = popupText;
        textBox.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        textBox.gameObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        textBox.gameObject.SetActive(false);
    }
}
