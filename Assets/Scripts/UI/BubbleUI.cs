using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubbleUI : MonoBehaviour
{
    [SerializeField] private Image _imageBubble;

    public void DesactivateBubble()
    {
        _imageBubble.fillOrigin = 0;
    }

    public void ActivateBubble()
    {
        _imageBubble.fillOrigin = 1;
    }
}
