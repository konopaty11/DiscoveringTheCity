using UnityEngine;

public class PointController : MonoBehaviour
{
    [SerializeField] ShowUIController _showUI;
    [SerializeField] RectTransform _rectTrasform;

    public void Click()
    {
        _showUI.ShowUI(_rectTrasform);
    }
}
