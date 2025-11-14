using UnityEngine;

/// <summary>
/// точка активности
/// </summary>
public class PointController : MonoBehaviour
{
    [SerializeField] ShowUIController _showUI;
    [SerializeField] RectTransform _rectTrasform;

    /// <summary>
    /// нажатие на точку
    /// </summary>
    public void Click()
    {
        _showUI.ShowUI(_rectTrasform);
    }
}
