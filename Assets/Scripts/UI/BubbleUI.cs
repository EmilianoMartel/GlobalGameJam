using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubbleUI : MonoBehaviour
{
    [SerializeField] private Image _imageBubble;

    public void DesactivateBubble()
    {
        _imageBubble.fillAmount = 0;
    }

    public void ActivateBubble()
    {
        _imageBubble.fillAmount = 1;
    }
}
