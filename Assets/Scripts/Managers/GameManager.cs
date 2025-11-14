using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// основной менеджер
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] List<Sight> _sights;
    [SerializeField] Slider _sliderProgress;
    [SerializeField] Text _textProgress;

    const int _AllCountTasks = 4;

    void Start()
    {
        UpdateProgress();
    }

    /// <summary>
    /// обновление прогресса
    /// </summary>
    public void UpdateProgress()
    {
        float _newProgress = 0f;
        foreach (Sight _sight in _sights)
        {
            _newProgress += _sight.CountPassedTasks;
        }
        _newProgress /= _AllCountTasks * _sights.Count;

        StartCoroutine(InterpolateToNewProgress(_newProgress));
    }

    /// <summary>
    /// корутина анимации прогресса
    /// </summary>
    /// <param name="_newProgress"> новое значение прогресса </param>
    /// <returns></returns>
    IEnumerator InterpolateToNewProgress(float _newProgress)
    {
        float _duration = 0.5f;
        float _elapsed = 0f;
        float _startProgress = _sliderProgress.value;

        float _currentProgress;
        while (_elapsed < _duration)
        {
            _elapsed += Time.deltaTime;

            _currentProgress = Mathf.Lerp(_startProgress, _newProgress, _elapsed / _duration);
            _sliderProgress.value = _currentProgress;
            _textProgress.text = $"{_currentProgress * 100f:F0}%";

            yield return null;
        }
    }
}
