using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sight : MonoBehaviour
{
    [Header("Saves")]
    [SerializeField] int _savesIndex;
    [SerializeField] Saves _saves;

    [Header("Other")]
    [SerializeField] GameManager _gameManager;
    [SerializeField] ShowUIController _uiController;
    [SerializeField] List<Puzzle> _easyPuzzles;
    [SerializeField] List<Puzzle> _hardPuzzles;
    [SerializeField] RebusController _rebus;
    [SerializeField] Quiz _quiz;
    [SerializeField] Text _puzzels;
    [SerializeField] Text _quizs;
    [SerializeField] Image _background;
    [SerializeField] List<GameObject> _easyPuzzleObjects;
    [SerializeField] List<GameObject> _hardPuzzleObjects;
    [SerializeField] GameObject _rebusObject;
    [SerializeField] GameObject _quizObject;

    string _puzzelsPattern;
    string _quizsPattern;

    int _countPassedPuzzels;
    int _countPassedQuizs;

    bool _isHardPuzzle = false;

    const int _AllCountTasks = 4;
    const float _CoefficientBackgroundColor = 0.2f / _AllCountTasks;

    RectTransform _rectTransfrom;

    public bool IsPuzzleEnd { get; private set; } = false;
    public int CountPassedPuzzels { get => _countPassedPuzzels; set => SetCountPassedPuzzels(value); }
    public int CountPassedQuizs { get => _countPassedQuizs; set => SetCountPassedQuizs(value); }
    public int CountPassedTasks => CountPassedPuzzels + CountPassedQuizs;

    void OnEnable()
    {
        _puzzelsPattern = _puzzels.text;
        _quizsPattern = _quizs.text;
        Saves.DataLoad += RestoreData;
    }

    void OnDisable()
    {
        Saves.DataLoad -= RestoreData;
    }

    void Start()
    {
        _rectTransfrom = GetComponent<RectTransform>();
    }

    void Update()
    {
        _background.color = new(1 - CountPassedTasks * _CoefficientBackgroundColor, 0.8f + CountPassedTasks * _CoefficientBackgroundColor, 0.8f);
    }

    void RestoreData()
    {
        foreach (SightSaves _sightSaves in _saves.SavesData.sightsSaves)
        {
            if (_sightSaves.index == _savesIndex)
            {
                if (_sightSaves.countPassedTasks == 4)
                {
                    CountPassedQuizs = 1;
                    CountPassedPuzzels = 3;
                }
                else
                    CountPassedPuzzels = _sightSaves.countPassedTasks;
            }
        }

        if (CountPassedTasks >= 1)
        {
            foreach (Puzzle _puzzle in _easyPuzzles)
            {
                _puzzle.SetPassed();
            }
        }
        if (CountPassedTasks >= 2)
        {
            foreach (Puzzle _puzzle in _hardPuzzles)
            {
                _puzzle.SetPassed();
            }
        }
        if (CountPassedTasks >= 3)
            _rebus.SetPassed();
        if (CountPassedTasks >= 4)
            _quiz.SetPassed();

        StartPuzzle();
    }

    void SetCountPassedPuzzels(int _value)
    {
        _puzzels.text = _puzzelsPattern + _value.ToString();
        _countPassedPuzzels = _value;
        _saves.SaveCountPassedTasks(_savesIndex, CountPassedTasks);
    }

    void SetCountPassedQuizs(int _value)
    {
        _quizs.text = _quizsPattern + _value.ToString();
        _countPassedQuizs = _value;
        _saves.SaveCountPassedTasks(_savesIndex, CountPassedTasks);
    }

    public void StartPuzzle()
    {
        if (_isHardPuzzle)
        {
            _hardPuzzleObjects[Random.Range(0, _hardPuzzleObjects.Count)].SetActive(true);
            IsPuzzleEnd = true;
        }
        else
        {
            _easyPuzzleObjects[Random.Range(0, _easyPuzzleObjects.Count)].SetActive(true);
            _isHardPuzzle = true;
        }
    }

    public void StartRebus()
    {
        _rebusObject.SetActive(true);
    }

    public void StartQuiz()
    {
        _quizObject.SetActive(true);
    }

    public void CloseSight()
    {
        _gameManager.UpdateProgress();
        _uiController.HideUI(_rectTransfrom);
        _isHardPuzzle = false;
        IsPuzzleEnd = false;
        StartPuzzle();
    }
}
