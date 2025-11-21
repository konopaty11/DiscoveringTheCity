using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// достопримечательность
/// </summary>
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
    [SerializeField] List<RebusController> _rebuses;
    [SerializeField] List<Quiz> _quizs;
    [SerializeField] Text jigsawText;
    [SerializeField] Text _quizText;
    [SerializeField] Image _background;
    [SerializeField] List<GameObject> _easyPuzzleObjects;
    [SerializeField] List<GameObject> _hardPuzzleObjects;
    [SerializeField] List<GameObject> _rebusObjects;
    [SerializeField] List<GameObject> _quizObjects;
    [SerializeField] List<GameObject> buttons;

    string _puzzelsPattern;
    string _quizsPattern;

    Puzzle _easyPuzzle;
    Puzzle _hardPuzzle;
    GameObject _easyPuzzleObject;
    GameObject _hardPuzzleObject;

    int _countPassedPuzzles;
    int _countPassedRebuses;
    int _countPassedQuizs;

    bool _isHardPuzzle = false;

    const int _AllCountTasks = 4;
    const float _CoefficientBackgroundColor = 0.2f / _AllCountTasks;

    RectTransform _rectTransfrom;

    public bool IsPuzzleEnd { get; private set; } = false;

    public int CountPassedPuzzles 
    { 
        get => _countPassedPuzzles; 
        set
        {
            _countPassedPuzzles = value;
            SetCountPassedJigsaws();
        } 
    }
    public int CountPassedRebuses
    {
        get => _countPassedRebuses;
        set
        {
            _countPassedRebuses = value;
            SetCountPassedJigsaws();
        }
    }
    public int CountPassedQuizs 
    { 
        get => _countPassedQuizs;
        set
        {
            _countPassedQuizs = value;
            SetCountPassedQuizs();
        }
    }
    public int CountPassedJigsaws => CountPassedPuzzles + CountPassedRebuses;
    public int CountPassedTasks => _countPassedPuzzles + _countPassedRebuses + CountPassedQuizs;

    void OnEnable()
    {
        _easyPuzzle = _easyPuzzles[Random.Range(0, _easyPuzzles.Count)];
        _easyPuzzleObject = _easyPuzzleObjects[Random.Range(0, _easyPuzzleObjects.Count)];
        if (_hardPuzzles.Count != 0)
            _hardPuzzle = _hardPuzzles[Random.Range(0, _hardPuzzles.Count)];

        if (_hardPuzzleObjects.Count != 0)
            _hardPuzzleObject = _hardPuzzleObjects[Random.Range(0, _hardPuzzleObjects.Count)];

        _puzzelsPattern = jigsawText.text;
        _quizsPattern = _quizText.text;

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

    /// <summary>
    /// восстановление данных
    /// </summary>
    void RestoreData()
    {
        foreach (SightSaves _sightSaves in _saves.SavesData.sightsSaves)
        {
            if (_sightSaves.index == _savesIndex)
            {
                _countPassedPuzzles = _sightSaves.countPassedPuzzles;
                _countPassedRebuses = _sightSaves.countPassedRebuses;
                _countPassedQuizs = _sightSaves.countPassedQuizs;
            }
        }

        if (CountPassedPuzzles == 1)
            _easyPuzzle.SetPassed();
        else if (CountPassedPuzzles == 2)
        {
            Debug.Log("sdf");
            _easyPuzzle.SetPassed();
            _hardPuzzle.SetPassed();
        }

        for (int i = 0; i < CountPassedRebuses; i++)
        {
            _rebuses[i].SetPassed();
        }

        for (int i = 0; i < CountPassedQuizs; i++)
            _quizs[i].SetPassed();

        SetCountPassedJigsaws();
        SetCountPassedQuizs();
    }

    public void VisibilityButtonsControl(bool _visible)
    {
        foreach (GameObject _button in buttons)
            _button.SetActive(_visible);
    }

    /// <summary>
    /// установить число сданных головоломок
    /// </summary>
    void SetCountPassedJigsaws()
    {
        jigsawText.text = _puzzelsPattern + CountPassedJigsaws.ToString();
        //Debug.Log(CountPassedPuzzles + "puzz save");
        _saves.SaveCountPassedTasks(_savesIndex, CountPassedPuzzles, CountPassedRebuses, CountPassedQuizs);
    }

    /// <summary>
    /// установить число сданных викторин
    /// </summary>
    void SetCountPassedQuizs()
    {
        //Debug.Log(CountPassedPuzzles + "puzz save");
        _quizText.text = _quizsPattern + CountPassedQuizs.ToString();
        _saves.SaveCountPassedTasks(_savesIndex, CountPassedPuzzles, CountPassedRebuses, CountPassedQuizs);
    }

    /// <summary>
    /// начало пазла
    /// </summary>
    public void StartPuzzle(bool _isEasy)
    {
        if (_isEasy)
            _easyPuzzleObject.SetActive(true);
        else
            _hardPuzzleObject.SetActive(true);

        VisibilityButtonsControl(false);
    }

    /// <summary>
    /// начало ребуса
    /// </summary>
    public void StartRebus(int _index)
    {
        _rebusObjects[_index].SetActive(true);
        VisibilityButtonsControl(false);
    }

    /// <summary>
    /// начало головоломки
    /// </summary>
    public void StartQuiz(int _index)
    {
        _quizs[_index].StartTimer();
        _quizObjects[_index].SetActive(true);
        VisibilityButtonsControl(false);
    }

    /// <summary>
    /// закрытие окна достопримечательности
    /// </summary>
    public void CloseSight()
    {
        _gameManager.UpdateProgress();
        _uiController.HideUI(_rectTransfrom);
        _isHardPuzzle = false;
        IsPuzzleEnd = false;
    }
}
