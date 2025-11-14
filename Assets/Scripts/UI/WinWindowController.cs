using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Логика окна победы
/// </summary>
public class WinWindowController : MonoBehaviour
{
    [SerializeField] GameObject _window;
    [SerializeField] Text _text;

    /// <summary>
    /// показ окна победы
    /// </summary>
    /// <param name="_content"></param>
    public void ShowWindow(string _content)
    {
        _window.SetActive(true);
        _text.text = _content;
    }

    /// <summary>
    /// скрытие окна победы
    /// </summary>
    public void HideWindow() => _window.SetActive(false);
}
