using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// игра пазл
/// </summary>
public class Puzzle : MonoBehaviour
{
    [SerializeField] WinWindowController _winWindow;
    [SerializeField] Sight _sight;
    [SerializeField] List<PuzzlePieceSerializable> _pieces;
    [SerializeField] Text _checkText;
    [SerializeField] GameObject _puzzleWindow;
    [SerializeField] GameObject _nonPassedWindow;
    [SerializeField] GameObject _passedWindow;

    [Serializable]
    class PuzzlePieceSerializable
    {
        public RectTransform rectTransform;
        public int index = -1;
        [NonSerialized] public bool isEmpty = true;
        public int zRotation = -1;
        public Image image;
    }

    /// <summary>
    /// сделать пазл сданным
    /// </summary>
    public void SetPassed()
    {
        _nonPassedWindow.SetActive(false);
        _passedWindow.SetActive(true);
    }

    /// <summary>
    /// показывать свободные места для размещения деталей
    /// </summary>
    /// <param name="_index"> индекс детали </param>
    public void ShowEmptyPlaces(int _index)
    {
        foreach (PuzzlePieceSerializable _piece in _pieces)
        {
            if (_piece.index == _index)
                _piece.isEmpty = true;

            if (_piece.isEmpty)
                _piece.image.color = new(0, 1, 0, 0.5f);
            else
                _piece.image.color = new(1, 0, 0, 0.5f);
        }
    }


    /// <summary>
    /// установление вращения 
    /// </summary>
    /// <param name="_index"> индекс детали </param>
    /// <param name="_zRotation"> новое вращение </param>
    public void SetRotation(int _index, int _zRotation)
    {
        foreach (PuzzlePieceSerializable _piece in _pieces)
        {
            if (_piece.index == _index)
                _piece.zRotation = _zRotation;
        }
    }

    /// <summary>
    /// убрать подсказки свободных мест
    /// </summary>
    public void HideEmptyPlaces()
    {
        foreach (PuzzlePieceSerializable _piece in _pieces)
        {
            _piece.image.color = Color.clear;
        }
    }

    /// <summary>
    /// получить ближайшую позицию
    /// </summary>
    /// <param name="_position"> текущая позиция </param>
    /// <param name="_index"> индекс детали </param>
    /// <returns> новая позиция </returns>
    public Vector2 GetClosestPosition(Vector2 _position, int _index)
    {
        float _distanceThresold = 2f;
        float _minDistance = Vector2.Distance(_position, _pieces[0].rectTransform.position);
        float _distance;
        int _indexPiece = -1;

        for (int i = 0; i < _pieces.Count; i++)
        {
            _distance = Vector2.Distance(_position, _pieces[i].rectTransform.position);
            if (_distance <= _minDistance && _pieces[i].isEmpty)
            {
                _indexPiece = i;
                _minDistance = _distance;
            }
        }

        if (_minDistance > _distanceThresold || _indexPiece == -1)
            return _position;

        for (int i = 0; i < _pieces.Count; i++)
        {
            if (i == _indexPiece)
            {
                _pieces[i].index = _index;
                _pieces[i].isEmpty = false;
                return _pieces[i].rectTransform.position;
            }
        }

        return _position;
    }

    /// <summary>
    /// проверка корректности пазла
    /// </summary>
    public void CheckPuzzle()
    {
        for (int i = 0; i < _pieces.Count; i++)
        {
            if (i != _pieces[i].index || _pieces[i].zRotation != 0)
            {
                StartCoroutine(WrongPuzzle());
                return;
            }
        }

        _checkText.text = "Пазл пройден";
        _sight.CountPassedPuzzles++;
        StartCoroutine(ClosePuzzle());
    }

    /// <summary>
    /// закрыть пазл
    /// </summary>
    /// <returns></returns>
    IEnumerator ClosePuzzle()
    {
        _winWindow.ShowWindow("Пазл пройден");
        yield return new WaitForSeconds(1.5f);
        _winWindow.HideWindow();

        SetPassed();
        Continue();
    }

    /// <summary>
    /// логика некорретного пазла 
    /// </summary>
    /// <returns></returns>
    IEnumerator WrongPuzzle()
    {
        _checkText.text = "Пазл неправильный";
        yield return new WaitForSeconds(1f);
        _checkText.text = "Проверить";
    }

    /// <summary>
    /// продолжить (в режиме просмотра)
    /// </summary>
    public void Continue()
    {
        //if (_sight.IsPuzzleEnd)
        //    _sight.StartRebus();
        //else
        //    _sight.StartPuzzle();
        _puzzleWindow.SetActive(false);
    }
}
