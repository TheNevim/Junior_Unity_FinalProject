using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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

    public List<SaveData> LoadPlayersTable()
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
                scoreTable.Add(readData);
            }
            return scoreTable;
        }

        return null;
    }
    
    
    public void SavePlayerName(int points)
    {
        List<SaveData> scoreTable = LoadPlayersTable();

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

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void RetryGame()
    {
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
