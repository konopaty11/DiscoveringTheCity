using UnityEngine;
using UnityEngine.UI;

public class Sight : MonoBehaviour
{
    [SerializeField] Text _puzzels;
    [SerializeField] Text _quizs;

    int _countPassedPuzzels;
    int _countPassedQuizs;

    public int CountPassedPuzzels { get => _countPassedPuzzels; set => SetCountPassedPuzzels(value); }
    public int CountPassedQuizs { get => _countPassedQuizs; set => SetCountPassedQuizs(value); }

    void SetCountPassedPuzzels(int _value)
    {
        _puzzels.text = _value.ToString();
        _countPassedPuzzels = _value;
    }

    void SetCountPassedQuizs(int _value)
    {
        _quizs.text = _value.ToString();
        _countPassedQuizs = _value;
    }
}
