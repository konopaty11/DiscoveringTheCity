using System;
using System.Collections.Generic;

/// <summary>
/// класс сериализации данных
/// </summary>
[Serializable]
public class SavesData
{
    public bool musicOn = true;
    public bool soundOn = true;

    public List<SightSaves> sightsSaves = new();
}

/// <summary>
/// дополнительный класс сериализации выполненных заданий для достопримечательности
/// </summary>
[Serializable]
public class SightSaves
{
    public int index;
    public int countPassedTasks;

    public SightSaves (int _index, int _countPassedTasks)
    {
        index = _index;
        countPassedTasks = _countPassedTasks;
    }
}