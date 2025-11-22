using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// кусочек пазла
/// </summary>
public class PiecePuzzle : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler
{
    [SerializeField] int _index;
    [SerializeField] Puzzle _puzzle;
    [SerializeField] Canvas _canvas;
    
    RectTransform _rectTransform;
    bool _isDrag = false;

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _puzzle.ShowEmptyPlaces(_index);
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta * _canvas.scaleFactor;
        _isDrag = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _isDrag = false;
        _puzzle.HideEmptyPlaces();

        Vector2 _position = _puzzle.GetClosestPosition(_rectTransform.position, _index);
        _rectTransform.position = _position;
        _puzzle.SetRotation(_index, (int)_rectTransform.eulerAngles.z);
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isDrag) return;

        _rectTransform.eulerAngles = new(_rectTransform.eulerAngles.x, _rectTransform.eulerAngles.y, _rectTransform.eulerAngles.z + 90f);
        _puzzle.SetRotation(_index, (int)_rectTransform.eulerAngles.z);
    }
}
