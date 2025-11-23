using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// логика окна сюжета
/// </summary>
public class PlotController : MonoBehaviour
{
    [SerializeField] List<PlotSightSerializable> _sights;
    [SerializeField] List<Text> _texts;

    [Serializable]
    class PlotSightSerializable
    {
        public Sight sight;
        public string title;
    }

    void Update()
    {
        for (int i = 0; i < _texts.Count; i++)
        {
            if (i == _sights.Count) break;
            _texts[i].text = $"{_sights[i].title}. Решено головоломок: {_sights[i].sight.CountPassedJigsaws}, викторин: {_sights[i].sight.CountPassedQuizs}";
        }
    }
}
