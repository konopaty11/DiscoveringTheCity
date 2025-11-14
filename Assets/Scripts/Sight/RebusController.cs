using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// логика ребуса
/// </summary>
public class RebusController : MonoBehaviour
{
    [SerializeField] WinWindowController _winWindow;
    [SerializeField] Sight _sight;
    [SerializeField] GameObject _rebus;
    [SerializeField] string _rightAnswer;
    [SerializeField] InputField _inputField;
    [SerializeField] GameObject _passedWindow;
    [SerializeField] GameObject _nonPassedWindow;
    [SerializeField] Image _imageInputField;

    bool _isDecided = false;

    /// <summary>
    /// установить режим просмотра для ребуса
    /// </summary>
    public void SetPassed()
    {
        _nonPassedWindow.SetActive(false);
        _passedWindow.SetActive(true);
    }

    /// <summary>
    /// сравнить ответ
    /// </summary>
    public void CheckAnswer()
    {
        if (_isDecided) return;

        if (_rightAnswer != _inputField.text)
        {
            _imageInputField.color = Color.red;
            return;
        }

        _isDecided = true;
        _imageInputField.color = Color.green;
        _inputField.text = "Ребус завершен";
        _sight.CountPassedPuzzels++;

        StartCoroutine(CloseRebus());
    }

    /// <summary>
    /// закрытие ребуса
    /// </summary>
    /// <returns></returns>
    IEnumerator CloseRebus()
    {
        _winWindow.ShowWindow("Ребус пройден");
        yield return new WaitForSeconds(1.5f);
        _winWindow.HideWindow();
        SetPassed();
        Continue();
    }

    /// <summary>
    /// продолжить (в режиме просмотра)
    /// </summary>
    public void Continue()
    {
        _rebus.SetActive(false);
        _sight.StartQuiz();
    }
}
