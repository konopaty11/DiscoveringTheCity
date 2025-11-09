using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// логика ребуса
/// </summary>
public class RebusController : MonoBehaviour
{
    [SerializeField] Sight _sight;
    [SerializeField] GameObject _rebus;
    [SerializeField] string _rightAnswer;
    [SerializeField] InputField _inputField;

    bool _isDecided = false;
    Image _imageInputField;

    void Start()
    {
        _imageInputField = _inputField.GetComponent<Image>();
    }

    /// <summary>
    /// сравнить ответ
    /// </summary>
    public void CheckAnswer()
    {
        if (_isDecided) return;
        _isDecided = true;

        if (_rightAnswer != _inputField.text)
        {
            _imageInputField.color = Color.red;
            return;
        }

        _imageInputField.color = Color.green;
        _inputField.text = "Ребус завершен";
        _sight.CountPassedPuzzels++;

        StartCoroutine(CloseRebus());
    }

    IEnumerator CloseRebus()
    {
        yield return new WaitForSeconds(1.5f);
        _rebus.SetActive(false);
    }
}
