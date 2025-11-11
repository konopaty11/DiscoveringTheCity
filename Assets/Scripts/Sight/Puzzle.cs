using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puzzle : MonoBehaviour
{
    [SerializeField] List<PuzzlePieceSerializable> _pieces;
    [SerializeField] Text _checkText;

    [System.Serializable]
    class PuzzlePieceSerializable
    {
        public RectTransform rectTransform;
        [NonSerialized] public int index = -1;
        [NonSerialized] public bool isEmpty = true;
        [NonSerialized] public int zRotation;
        public Image image;
    }

    public void ShowEmptyPlaces(int _index)
    {
        foreach (PuzzlePieceSerializable _piece in _pieces)
        {
            if (_piece.index == _index)
                _piece.isEmpty = true;

            if (_piece.isEmpty)
                _piece.image.color = new(0, 1, 0, 0.3f);
            else
                _piece.image.color = new(1, 0, 0, 0.3f);
        }
    }

    public void SetRotation(int _index, int _zRotation)
    {
        foreach (PuzzlePieceSerializable _piece in _pieces)
        {
            if (_piece.index == _index)
                _piece.zRotation = _zRotation;
        }
    }

    public void HideEmptyPlaces()
    {
        foreach (PuzzlePieceSerializable _piece in _pieces)
        {
            _piece.image.color = Color.clear;
        }
    }

    public Vector2 GetClosestPosition(Vector2 _position, int _index)
    {
        float _distanceThresold = 2f;
        float _minDistance = Vector2.Distance(_position, _pieces[0].rectTransform.position);
        float _distance;
        int _indexPiece = 0;

        for (int i = 0; i < _pieces.Count; i++)
        {
            _distance = Vector2.Distance(_position, _pieces[i].rectTransform.position);
            if (_distance < _minDistance && _pieces[i].isEmpty)
            {
                _indexPiece = i;
                _minDistance = _distance;
            }
        }

        if (_minDistance > _distanceThresold)
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
    }

    IEnumerator WrongPuzzle()
    {
        _checkText.text = "Пазл неправильный";
        yield return new WaitForSeconds(1f);
    }
}
