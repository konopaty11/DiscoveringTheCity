using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sight : MonoBehaviour
{
    [SerializeField] GameManager _gameManager;
    [SerializeField] ShowUIController _uiController;
    [SerializeField] List<GameObject> _easyPuzzles;
    [SerializeField] List<GameObject> _hardPuzzles;
    [SerializeField] GameObject _rebus;
    [SerializeField] GameObject _quiz;
    [SerializeField] Text _puzzels;
    [SerializeField] Text _quizs;
    [SerializeField] Image _background;

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

    void Start()
    {
        _puzzelsPattern = _puzzels.text;
        _quizsPattern = _quizs.text;
        _rectTransfrom = GetComponent<RectTransform>();

        StartPuzzle();
    }

    void Update()
    {
        _background.color = new(1 - CountPassedTasks * _CoefficientBackgroundColor, 0.8f + CountPassedTasks * _CoefficientBackgroundColor, 0.8f);
    }

    void SetCountPassedPuzzels(int _value)
    {
        _puzzels.text = _puzzelsPattern + _value.ToString();
        _countPassedPuzzels = _value;
    }

    void SetCountPassedQuizs(int _value)
    {
        _quizs.text = _quizsPattern + _value.ToString();
        _countPassedQuizs = _value;
    }

    public void StartPuzzle()
    {
        if (_isHardPuzzle)
        {
            _hardPuzzles[Random.Range(0, _hardPuzzles.Count)].SetActive(true);
            IsPuzzleEnd = true;
        }
        else
        {
            _easyPuzzles[Random.Range(0, _easyPuzzles.Count)].SetActive(true);
            _isHardPuzzle = true;
        }
    }

    public void StartRebus()
    {
        _rebus.SetActive(true);
    }

    public void StartQuiz()
    {
        _quiz.SetActive(true);
    }

    public void CloseSight()
    {
        _gameManager.UpdateProgress();
        _uiController.HideUI(_rectTransfrom);
    }
}
