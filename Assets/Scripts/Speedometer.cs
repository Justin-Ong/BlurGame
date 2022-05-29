using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Speedometer : MonoBehaviour
{
    [SerializeField]
    private int maxSpeedToDisplay;

    [SerializeField]
    private Image[] sections;

    [SerializeField]
    private TextMeshProUGUI speedText;

    [SerializeField]
    private Player player;

    private void LateUpdate()
    {
        UpdateSectionDisplay();
        UpdateTextDisplay();
    }

    private void UpdateSectionDisplay()
    {
        int numSectionsToDisplay = Mathf.RoundToInt((player.Speed / maxSpeedToDisplay) * sections.Length);
        for (int i = 0; i < sections.Length; i++) {
            if (i < numSectionsToDisplay) {
                sections[i].enabled = true;
            }
            else {
                sections[i].enabled = false;
            }
        }
    }

    private void UpdateTextDisplay()
    {
        int speed = Mathf.RoundToInt(player.Speed);
        if (speed > maxSpeedToDisplay)
        {
            speedText.text = maxSpeedToDisplay.ToString() + "+";
        }
        else
        {
            speedText.text = speed.ToString();
        }
    }
}
