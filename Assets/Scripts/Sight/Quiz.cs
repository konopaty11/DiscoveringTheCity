using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [SerializeField] Sight _sight;
    [SerializeField] GameObject _quizGameObject;
    [SerializeField] Text _textPassed;
    [SerializeField] List<QuizSerializable> _quiz;
    [SerializeField] List<Text> _questions;
    [SerializeField] List<Text> _answers;
    [SerializeField] GameObject _promptBtn;

    [System.Serializable]
    class QuizSerializable
    {
        public string question;
        public string answer;
        public GameObject questionGameObject;
        public GameObject answerGameObject;
    }

    const int _countQuestions = 4;
    List<QuizSerializable> _usedQuiz = new();

    void Start()
    {
        CreateQuiz();
    }

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

    public void Prompt()
    {
        _promptBtn.SetActive(false);

        for (int i = 0; i < 2; i++)
        {
            string _tarhetAnswer = "";
            foreach (QuizSerializable _quiz in _usedQuiz)
            {
                if (_quiz.question == _questions[i].text)
                    _tarhetAnswer = _quiz.answer;
            }

            for (int j = 0; j < _answers.Count; j++)
            {
                if (_answers[j].text == _tarhetAnswer)
                {
                    _answers[j].text = _answers[i].text;
                    _answers[i].text = _tarhetAnswer;
                }
            }
        }
    }

    public void CreateQuiz()
    {
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

    IEnumerator WrongAnswer()
    {
        _textPassed.text = "Неправильная последовательность ответов";
        yield return new WaitForSeconds(1f);
        _textPassed.text = "Проверить";
    }

    IEnumerator CloseQuiz()
    {
        _textPassed.text = "Викторина пройдена";
        _textPassed.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        _quizGameObject.SetActive(false);
    }
}
