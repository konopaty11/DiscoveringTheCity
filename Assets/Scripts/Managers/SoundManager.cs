using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource _musicAudioSource;
    [SerializeField] AudioClip _clip;
    [SerializeField] Image _musicImage;
    [SerializeField] Image _soundImage;
    [SerializeField] Sprite _onMusicSprite;
    [SerializeField] Sprite _offMusicSprite;
    [SerializeField] Sprite _onSoundSprite;
    [SerializeField] Sprite _offSoundSprite;

    bool _isMusicOn = false;
    bool _isSoundOn = true;
    List<Button> _buttons;
    List<AudioSource> _audioSources = new();

    void Start()
    {
        _buttons = FindObjectsByType<Button>(FindObjectsInactive.Include, FindObjectsSortMode.None).ToList();
        foreach (Button _button in _buttons)
        {
            AudioSource _audioSource = _button.gameObject.AddComponent<AudioSource>();
            _audioSources.Add(_audioSource);
            _audioSource.clip = _clip;
            _audioSource.playOnAwake = false;

            _button.onClick.AddListener(_audioSource.Play);
        }
    }

    public void MusicClick()
    {
        if (_isMusicOn)
        {
            _isMusicOn = false;
            _musicImage.sprite = _offMusicSprite;
        }
        else
        {
            _isMusicOn = true;
            _musicImage.sprite = _onMusicSprite;
        }

        _musicAudioSource.volume = _isMusicOn ? 1 : 0;
    }

    public void SoundClick()
    {
        if (_isSoundOn)
        {
            _isSoundOn = false;
            _soundImage.sprite = _offSoundSprite;
        }
        else
        {
            _isSoundOn = true;
            _soundImage.sprite = _onSoundSprite;
        }

        foreach (AudioSource _audioSource in _audioSources)
        {
            _audioSource.volume = _isSoundOn ? 1 : 0;
        }
    }
}
