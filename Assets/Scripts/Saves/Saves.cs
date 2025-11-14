using System.IO;
using UnityEngine;
using UnityEngine.Events;

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

    void Load()
    {
        _path = Path.Combine(Application.persistentDataPath, _fileName);
        if (File.Exists(_path))
        {
            SavesData = JsonUtility.FromJson<SavesData>(File.ReadAllText(_path)) ?? new SavesData();
            Debug.Log(SavesData);
        }
        else
        {
            Directory.CreateDirectory(Application.persistentDataPath);
            File.Create(_path).Dispose();
            SavesData = new SavesData();
        }

        DataLoad?.Invoke();
    }

    public void SaveCountPassedTasks(int _index, int _countPassedTasks)
    {
        foreach (SightSaves _sightSaves in SavesData.sightsSaves)
        {
            if (_sightSaves.index == _index)
            {
                _sightSaves.countPassedTasks = _countPassedTasks;
                Save();
                return;
            }
        }

        SavesData.sightsSaves.Add(new SightSaves(_index, _countPassedTasks));
        Save();
    }

    public void Save()
    {
        string _jsonData = JsonUtility.ToJson(SavesData);
        File.WriteAllText(_path, _jsonData);
    }
}
