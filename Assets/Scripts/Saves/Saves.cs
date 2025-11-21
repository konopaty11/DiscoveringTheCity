using System.IO;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Логика сохранения
/// </summary>
public class Saves : MonoBehaviour
{
    public SavesData SavesData;

    string _fileName = "UserData.json";
    string _path;

    public static UnityAction DataLoad;

    void Start()
    {
        Load();
    }

    /// <summary>
    /// загрузка данных
    /// </summary>
    void Load()
    {
        _path = Path.Combine(Application.persistentDataPath, _fileName);
        if (File.Exists(_path))
        {
            SavesData = JsonUtility.FromJson<SavesData>(File.ReadAllText(_path)) ?? new SavesData();
        }
        else
        {
            Directory.CreateDirectory(Application.persistentDataPath);
            File.Create(_path).Dispose();
            SavesData = new SavesData();
        }
        foreach (SightSaves _sightSaves in SavesData.sightsSaves)
        {
            Debug.Log($"LOAD puzz - {_sightSaves.countPassedPuzzles}; rebus - {_sightSaves.countPassedRebuses}; quiz - {_sightSaves.countPassedQuizs}");
        }
        DataLoad?.Invoke();
    }

    /// <summary>
    /// сохранение сделанных задач
    /// </summary>
    /// <param name="_index"></param>
    /// <param name="_countPassedTasks"></param>
    public void SaveCountPassedTasks(int _index, int _countPassedPuzzles, int _countPassedRebuses, int _countPassedQuizs)
    {
        foreach (SightSaves _sightSaves in SavesData.sightsSaves)
        {
            if (_sightSaves.index == _index)
            {
                _sightSaves.countPassedPuzzles = _countPassedPuzzles;
                _sightSaves.countPassedRebuses = _countPassedRebuses;
                _sightSaves.countPassedQuizs = _countPassedQuizs;

                Save();
                return;
            }
        }

        SavesData.sightsSaves.Add(new SightSaves(_index, _countPassedPuzzles, _countPassedRebuses, _countPassedQuizs));
        Save();
    }

    /// <summary>
    /// общее сохранение
    /// </summary>
    public void Save()
    {
        foreach (SightSaves _sightSaves in SavesData.sightsSaves)
        {
            Debug.Log($"SAVE puzz - {_sightSaves.countPassedPuzzles}; rebus - {_sightSaves.countPassedRebuses}; quiz - {_sightSaves.countPassedQuizs}");
        }
        string _jsonData = JsonUtility.ToJson(SavesData);
        //Debug.Log(_jsonData);
        File.WriteAllText(_path, _jsonData);
    }
}
