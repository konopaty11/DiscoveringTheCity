using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// менеджер звуковых эффектов
/// </summary>
public class SoundManager : MonoBehaviour
{
    [SerializeField] Saves _saves;
    [SerializeField] AudioSource _musicAudioSource;
    [SerializeField] AudioSource _soundAudioSource;
    [SerializeField] Image _musicImage;
    [SerializeField] Image _soundImage;
    [SerializeField] Sprite _onMusicSprite;
    [SerializeField] Sprite _offMusicSprite;
    [SerializeField] Sprite _onSoundSprite;
    [SerializeField] Sprite _offSoundSprite;

    bool _isMusicOn = false;
    bool _isSoundOn = true;
    List<Button> _buttons;

    void OnEnable()
    {
        Saves.DataLoad += RestoreSettings;
    }

    void OnDisable()
    {
        Saves.DataLoad -= RestoreSettings;
    }

    void Start()
    {
        _buttons = FindObjectsByType<Button>(FindObjectsInactive.Include, FindObjectsSortMode.None).ToList();
        foreach (Button _button in _buttons)
        {
            _button.onClick.AddListener(_soundAudioSource.Play);
        }
    }

    /// <summary>
    /// восстановление данных
    /// </summary>
    void RestoreSettings()
    {
        _isMusicOn = _saves.SavesData.musicOn;
        _isSoundOn = _saves.SavesData.soundOn;

        _musicAudioSource.volume = _isMusicOn ? 1 : 0;
        _soundAudioSource.volume = _isSoundOn ? 1 : 0;
        _musicImage.sprite = _isMusicOn ? _onMusicSprite : _offMusicSprite;
        _soundImage.sprite = _isSoundOn ? _onSoundSprite : _offSoundSprite;
    }

    /// <summary>
    /// нажатие на кнопку включения, выключения музыки
    /// </summary>
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

        _saves.SavesData.musicOn = _isMusicOn;
        _saves.Save();
        _musicAudioSource.volume = _isMusicOn ? 1 : 0;
    }

    /// <summary>
    /// нажатие на кнопку включения, выключения звуков
    /// </summary>
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

        _saves.SavesData.soundOn = _isSoundOn;
        _saves.Save();
        _soundAudioSource.volume = _isSoundOn ? 1 : 0;
    }
}
