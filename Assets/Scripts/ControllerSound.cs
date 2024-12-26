using UnityEngine;
using UnityEngine.Audio;

public class ControllerSound : MonoBehaviour
{
    [SerializeField] private SoundMenu _soundMenu;
    [SerializeField] private AudioMixerGroup _masterAudioMixerGroup;
    [SerializeField] private AudioMixerGroup _soundAudioMixerGroup;
    [SerializeField] private AudioMixerGroup _backgroundAudioMixerGroup;
    [SerializeField] private AudioSource _audioSourceSounds;
    [SerializeField] private AudioSource _audioSourceBackgroundSound;

    private const string MasterVolume = "MasterVolume";
    private const string SoundVolume = "SoundVolume";
    private const string BackgroundSoundVolume = "BackgroundSoundVolume";

    private float _minVolume = -80;
    private float _maxVolume = 0;
    private float _minValueSlider = 0.0001f;

    private void OnEnable()
    {
        SubscribeSoundMenu();
    }

    private void OnDisable()
    {
        UnsubscribeSoundMenu();
    }

    private void ChangeMixerVolume(string exposedParameter, float volume, float maxValue)
    {
        AudioMixer audioMixer = null;

        switch (exposedParameter)
        {
            case MasterVolume:
                audioMixer = _masterAudioMixerGroup.audioMixer;
                break;

            case SoundVolume:
                audioMixer = _soundAudioMixerGroup.audioMixer;
                break;

            case BackgroundSoundVolume:
                audioMixer = _backgroundAudioMixerGroup.audioMixer;
                break;
        }

        SetMixerVolume(audioMixer, exposedParameter, volume, maxValue);
    }

    private void SetMixerVolume(AudioMixer mixer, string exposedParameter, float value, float maxValue)
    {
        value = ConvertValueToVolume(value, maxValue);
        mixer.SetFloat(exposedParameter, Mathf.Log10(value) * 20);
    }

    private float ConvertValueToVolume(float value, float maxValue)
    {
        if (value == 0)
            value = _minValueSlider;
        else
            value /= maxValue;

        return value;
    }

    private void PlayOneShotSound(AudioClip clip)
    {
        _audioSourceSounds.PlayOneShot(clip);
    }

    private void ToggleMusic(bool state)
    {
        if (state)
            _masterAudioMixerGroup.audioMixer.SetFloat(MasterVolume, _maxVolume);
        else
            _masterAudioMixerGroup.audioMixer.SetFloat(MasterVolume, _minVolume);
    }

    private void SubscribeSoundMenu()
    {
        _soundMenu.SliderContianerSentMessage += ChangeMixerVolume;
        _soundMenu.ButtonContainerSentAudioClip += PlayOneShotSound;
        _soundMenu.MuteAllSoundsToggleChanged += ToggleMusic;
    }

    private void UnsubscribeSoundMenu()
    {
        _soundMenu.SliderContianerSentMessage -= ChangeMixerVolume;
        _soundMenu.ButtonContainerSentAudioClip -= PlayOneShotSound;
        _soundMenu.MuteAllSoundsToggleChanged -= ToggleMusic;
    }
}