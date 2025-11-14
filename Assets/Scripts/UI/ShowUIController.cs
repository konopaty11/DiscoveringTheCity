using System.Collections;
using UnityEngine;

/// <summary>
/// логика анимации появления окна
/// </summary>
public class ShowUIController : MonoBehaviour
{
    float _startOffset = -Screen.height;
    float _finishOffset = 0f;

    /// <summary>
    /// показ ui
    /// </summary>
    /// <param name="_rectTransform"> rectTransform ui </param>
    public void ShowUI(RectTransform _rectTransform)
    {
        StartCoroutine(ShowUIControl(_rectTransform, _startOffset, _finishOffset));
    }

    /// <summary>
    /// скрытие ui
    /// </summary>
    /// <param name="_rectTransform"> rectTransform ui </param>
    public void HideUI(RectTransform _rectTransform)
    {
        StartCoroutine(ShowUIControl(_rectTransform, _finishOffset, _startOffset));
    }

    /// <summary>
    /// корутина 
    /// </summary>
    /// <param name="_rectTransform"> rectTransform ui </param>
    /// <param name="_startY"> начальный отсуп сверху </param>
    /// <param name="_finishY"> конечный отступ сверху </param>
    /// <returns></returns>
    IEnumerator ShowUIControl(RectTransform _rectTransform, float _startY, float _finishY)
    {
        float _duration = 0.5f;
        float _elapsed = 0f;
        float _y = _startY;

        while (_elapsed < _duration)
        {
            _elapsed += Time.deltaTime;
            _y = Mathf.Lerp(_startY, _finishY, _elapsed / _duration);
            _rectTransform.anchoredPosition = new(_rectTransform.anchoredPosition.x, _y);

            yield return null;
        }
    }
}
