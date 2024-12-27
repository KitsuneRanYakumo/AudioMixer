using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SliderVolume : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private AudioMixerGroup _audioMixerGroup;
    [SerializeField] private SwitcherSound _switcherMuteSound;

    private const float MinValueSlider = 0.0001f;

    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(TryChangeVolume);
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(TryChangeVolume);
    }

    private void TryChangeVolume(float value)
    {
        if (_switcherMuteSound.IsOff)
        {
            float volume = ConvertValueToVolume(value);
            _audioMixerGroup.audioMixer.SetFloat(_audioMixerGroup.name, Mathf.Log10(volume) * 20);
        }
    }

    private float ConvertValueToVolume(float value)
    {
        if (value == 0)
            value = MinValueSlider;
        else
            value /= _slider.maxValue;

        return value;
    }
}