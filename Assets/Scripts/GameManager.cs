using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public string playerAnimal;
    /*{
        get => playerAnimal;
        set
        {
            if (value != null && value is string )
            {
                playerAnimal = value;
            }
        }
    }*/
    public string playerName;

    public int level;

    public int score = 0;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public List<SaveData> LoadPlayersTable(int points)
    {
        string path = Application.persistentDataPath + "/players.json";
        if (File.Exists(path))
        {
            List<SaveData> scoreTable = new List<SaveData>();
            string json = "";

            string[] lines = File.ReadAllLines(path);

            for (int i = 0; i < lines.Length; i++)
            {
                SaveData readData = JsonUtility.FromJson<SaveData>(lines[i]);
                if (readData.playerName == GameManager.Instance.playerName &&
                    readData.animal == GameManager.Instance.playerAnimal && readData.score <= points)
                {
                    continue;
                }
                scoreTable.Add(readData);
            }
            return scoreTable;
        }

        return null;
    }
    
    
    public void SavePlayerName(int points)
    {
        List<SaveData> scoreTable = LoadPlayersTable(points);

        if (scoreTable == null)
        { 
            scoreTable = new List<SaveData>();
        }

        SaveData data = new SaveData(playerName, playerAnimal, level, points);
        scoreTable.Add(data);
        
        scoreTable.Sort((SaveData data1, SaveData data2) =>
        {
            int result = data1.score > data1.score ? -1 : 
                data1.score < data2.score ? 1 : 
                data1.score == data2.score ? 0 : 0;
            return result;
        });

        string json = "";
        foreach (SaveData playerScore in scoreTable)
        {
             json += JsonUtility.ToJson(playerScore) + "\n";
        }
        
        File.WriteAllText( Application.persistentDataPath + "/players.json", json);
    }


    [System.Serializable]
    public class SaveData
    {
        public string playerName;
        public string animal;
        public int level;
        public int score;

        public SaveData(string playerName, string animal, int level, int score)
        {
            this.playerName = playerName;
            this.animal = animal;
            this.level = level;
            this.score = score;
        }
    }

    public void WonGame()
    {
        bool decision = EditorUtility.DisplayDialog("Player name not set",
            "Please enter player name", "Menu");
        if (decision)
        {
            Invoke(nameof(ReturnToMenu),2f);
        }
    }

    public void ReturnToMenu()
    {
        GameManager.Instance.score = 0;
        SceneManager.LoadScene(0);
    }

    public void RetryGame()
    {
        SceneManager.LoadScene(GameObject.Find("GameManager").GetComponent<GameManager>().level);
    }
    
    public void NextLevel()
    {
        level++;
        SceneManager.LoadScene(GameObject.Find("GameManager").GetComponent<GameManager>().level);
    }

    public GameObject FindGameobject(string parent, string child)
    {
        GameObject filenamefld = null;
        Transform[] trans = GameObject.Find(parent).GetComponentsInChildren<Transform>(true);
        foreach (Transform t in trans) {
            if (t.gameObject.name == child) {
                filenamefld = t.gameObject;
                break;
            }
        }

        return filenamefld;
    }
    
}
