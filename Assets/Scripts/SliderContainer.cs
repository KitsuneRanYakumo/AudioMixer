using System;
using UnityEngine;
using UnityEngine.UI;

public class SliderContainer : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private string _nameMixerAudioGroup;

    public event Action<string, float, float> SliderChanged;

    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(HandleEventSlider);
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(HandleEventSlider);
    }

    private void HandleEventSlider(float value)
    {
        SliderChanged?.Invoke(_nameMixerAudioGroup, value, _slider.maxValue);
    }
}