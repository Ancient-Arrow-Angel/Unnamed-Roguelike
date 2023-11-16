using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void SetMaxValue(int Value)
    {
        slider.maxValue = Value;
        slider.value = Value;
    }

    public void SetValue(int Value)
    {
        slider.value = Value;
    }
}