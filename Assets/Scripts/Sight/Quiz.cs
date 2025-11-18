using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// класс викторины
/// </summary>
public class Quiz : MonoBehaviour
{
    [SerializeField] WinWindowController _winWindow;
    [SerializeField] Sight _sight;
    [SerializeField] GameObject _quizGameObject;
    [SerializeField] Text _textPassed;
    [SerializeField] List<QuizSerializable> _quizBase;
    [SerializeField] List<Text> _questions;
    [SerializeField] List<Text> _answers;
    [SerializeField] GameObject _promptBtn;
    [SerializeField] CanvasGroup _canvasGroup;
    [SerializeField] Text _timer;
    [SerializeField] GameObject _passedWindow;
    [SerializeField] GameObject _nonPassedWindow;
 
    [System.Serializable]
    class QuizSerializable
    {
        public string question;
        public string answer;
        public GameObject questionGameObject;
        public GameObject answerGameObject;
    }

    const int _countQuestions = 4;

    List<QuizSerializable> _quiz;
    List<QuizSerializable> _usedQuiz = new();

    Coroutine _timerCoroutine;

    bool _isPassed;

    void Start()
    {
        CreateQuiz();
    }

    /// <summary>
    /// начало таймера
    /// </summary>
    public void StartTimer()
    {
        if (!_isPassed)
            _timerCoroutine = StartCoroutine(Timer());
    }

    /// <summary>
    /// установление режима просмотра викторины
    /// </summary>
    public void SetPassed()
    {
        _isPassed = true;
        _nonPassedWindow.SetActive(false);
        _passedWindow.SetActive(true);
    }

    /// <summary>
    /// передвтжение ответа вверх
    /// </summary>
    /// <param name="_text"> текст ответа </param>
    public void MoveUpAnswer(Text _text)
    {
        int _index = 0;
        for (int i = 0; i < _usedQuiz.Count; i++)
        {
            if (_usedQuiz[i].answer == _text.text)
                _index = i;
        }

        for (int i = 0; i < _answers.Count; i++)
        {
            if (i != 0 && _answers[i] == _text)
            {
                string _prefAnswer = _answers[i - 1].text;
                _answers[i - 1].text = _answers[i].text;
                _answers[i].text = _prefAnswer;
                _usedQuiz[_index].answerGameObject = _answers[i].transform.parent.gameObject;
            }
        }
    }

    /// <summary>
    ///  передвижение ответа вниз 
    /// </summary>
    /// <param name="_text"> текст ответа </param>
    public void MoveDownAnswer(Text _text)
    {
        for (int i = 0; i < _answers.Count; i++)
        {
            if (i != _answers.Count - 1 && _answers[i] == _text)
            {
                string _prefAnswer = _answers[i + 1].text;
                _answers[i + 1].text = _answers[i].text;
                _answers[i].text = _prefAnswer;
            }
        }
    }

    /// <summary>
    /// логика подсказки 50 на 50
    /// </summary>
    public void Prompt()
    {
        _promptBtn.SetActive(false);

        for (int i = 0; i < 2; i++)
        {
            string _targetAnswer = "";
            foreach (QuizSerializable _quiz in _usedQuiz)
            {
                if (_quiz.question == _questions[i].text)
                    _targetAnswer = _quiz.answer;
            }

            for (int j = 0; j < _answers.Count; j++)
            {
                if (_answers[j].text == _targetAnswer)
                {
                    _answers[j].text = _answers[i].text;
                    _answers[i].text = _targetAnswer;
                }
            }
        }
    }

    /// <summary>
    /// —оздание викторины
    /// </summary>
    public void CreateQuiz()
    {
        _usedQuiz = new();
        _quiz = new();
        foreach (QuizSerializable _quizSerializable in _quizBase)
            _quiz.Add(_quizSerializable);

        for (int i = 0; i < _countQuestions; i++)
        {
            int _index = Random.Range(0, _quiz.Count);
            _usedQuiz.Add(_quiz[_index]);
            _quiz.RemoveAt(_index);
        }

        List<int> _usedQuestions = new();
        List<int> _usedAnswers = new();
        for (int i = 0; i < _questions.Count; i++)
        {
            int _indexQuestion = Random.Range(0, _usedQuiz.Count);
            while (_usedQuestions.Contains(_indexQuestion))
                _indexQuestion = Random.Range(0, _usedQuiz.Count);

            int _indexAnswer = Random.Range(0, _usedQuiz.Count);
            while (_usedAnswers.Contains(_indexAnswer))
                _indexAnswer = Random.Range(0, _usedQuiz.Count);

            _questions[i].text = _usedQuiz[_indexQuestion].question; 
            _answers[i].text = _usedQuiz[_indexAnswer].answer;

            _usedQuestions.Add(_indexQuestion);
            _usedAnswers.Add(_indexAnswer);
        }
    }

    /// <summary>
    /// проверка правильности викторины
    /// </summary>
    public void CheckQuiz()
    {
        for (int i = 0; i < _usedQuiz.Count; i++)
        {
            foreach (QuizSerializable _quiz in _usedQuiz)
                if (_quiz.question == _questions[i].text && _quiz.answer != _answers[i].text)
                {
                    StartCoroutine(WrongAnswer());
                    return;
                }
        }

        _sight.CountPassedQuizs++;
        StartCoroutine(CloseQuiz());
    }

    /// <summary>
    /// корутина таймера
    /// </summary>
    /// <returns></returns>
    IEnumerator Timer()
    {
        int time = 30;
        while (time > 0)
        {
            time -= 1;
            _timer.text = time.ToString();
            yield return new WaitForSeconds(1f);
        }
        _textPassed.text = "¬икторина провалена";
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;

        yield return new WaitForSeconds(1f);
        _textPassed.text = "ѕроверить";
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
        CreateQuiz();
        StartCoroutine(Timer());
        _promptBtn.SetActive(true);
    }

    /// <summary>
    /// Ћогика неправильного ответа викторины
    /// </summary>
    /// <returns></returns>
    IEnumerator WrongAnswer()
    {
        _textPassed.text = "Ќеправильна€ последовательность ответов";
        yield return new WaitForSeconds(1f);
        _textPassed.text = "ѕроверить";
    }

    /// <summary>
    /// закрытие викторины
    /// </summary>
    /// <returns></returns>
    IEnumerator CloseQuiz()
    {
        _winWindow.ShowWindow("¬икторина пройдена");
        StopCoroutine(_timerCoroutine);
        _textPassed.text = "¬икторина пройдена";
        _textPassed.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        _winWindow.HideWindow();
        SetPassed();
        Continue();
    }

    /// <summary>
    /// продолжить (в режиме просмотра)
    /// </summary>
    public void Continue()
    {
        _quizGameObject.SetActive(false);
        //_sight.CloseSight();
    }
}
