using UnityEngine;
using UnityEngine.UI;

public class Sight : MonoBehaviour
{
    [SerializeField] Text _puzzels;
    [SerializeField] Text _quizs;

    string _puzzelsPattern;
    string _quizsPattern;

    int _countPassedPuzzels;
    int _countPassedQuizs;

    void Start()
    {
        _puzzelsPattern = _puzzels.text;
        _quizsPattern = _quizs.text;
    }

    public int CountPassedPuzzels { get => _countPassedPuzzels; set => SetCountPassedPuzzels(value); }
    public int CountPassedQuizs { get => _countPassedQuizs; set => SetCountPassedQuizs(value); }

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
}
