using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class ButtonSound : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private AudioClip _audioClip;

    private AudioSource _audioSource;

    private void OnEnable()
    {
        _button.onClick.AddListener(PlaySound);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(PlaySound);
    }

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void PlaySound()
    {
        if (_audioSource.isPlaying)
            _audioSource.Stop();

        _audioSource.PlayOneShot(_audioClip);
    }
}
