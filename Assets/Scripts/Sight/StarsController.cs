using System.Collections.Generic;
using UnityEngine;

public class StarsController : MonoBehaviour
{
    [SerializeField] List<GameObject> stars;

    public void UpdateStars(bool _puzzlePassed, bool _rebusPassed, bool _quizPassed)
    {
        int _countStars = 0;

        if (_puzzlePassed)
            _countStars++;
        if (_rebusPassed)
            _countStars++;
        if (_quizPassed)
            _countStars++;

        for (int i = 0; i < _countStars; i++)
            stars[i].SetActive(true);
    }
}
