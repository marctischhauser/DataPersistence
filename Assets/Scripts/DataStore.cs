// --------------------------------------------------------------------------------
//  Copyright (C) 2023 TwoAmigos
// --------------------------------------------------------------------------------

using System;
using System.IO;
using UnityEngine;

public class DataStore : MonoBehaviour
{
    public static DataStore Instance;
    public string Name;
    public int Score;
    public string CurrentPlayerName;

    public void Save()
    {
        BestScore data = new BestScore(CurrentPlayerName, Score);
        Name = CurrentPlayerName;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/bestscores.json", json);
    }


    public void Load()
    {
        string path = Application.persistentDataPath + "/bestscores.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            BestScore data = JsonUtility.FromJson<BestScore>(json);

            Name = data.Name;
            Score = data.Score;
            CurrentPlayerName = data.Name;
        }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        Load();
    }

    [Serializable]
    private class BestScore
    {
        public string Name;
        public int Score;

        public BestScore(string name, int score)
        {
            Name = name;
            Score = score;
        }
    }
}