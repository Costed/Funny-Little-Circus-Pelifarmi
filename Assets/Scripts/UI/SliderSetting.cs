using HietakissaUtils;
using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;

public class SliderSetting : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI valueText;

    public event Action <float> OnValueChanged;


    void Start()
    {
        ValueChanged(slider.value);
    }

    void OnEnable()
    {
        slider.onValueChanged.AddListener(ValueChanged);
    }
    void OnDisable()
    {
        slider.onValueChanged.RemoveListener(ValueChanged);
    }

    public void ValueChanged(float value)
    {
        float roundedValue = value.RoundToDecimalPlaces(2);
        valueText.text = roundedValue.ToString();
        OnValueChanged?.Invoke(value);

        Debug.Log($"Slider value changed to: {roundedValue}");
    }
}
