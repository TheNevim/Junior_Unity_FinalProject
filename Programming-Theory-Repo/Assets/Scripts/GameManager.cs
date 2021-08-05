using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public string playerAnimal
    {
        set
        {
            if (value != null && value is string )
            {
                playerAnimal = value;
            }
        }
    }
    
    public string playerName
    {
        set
        {
            if (value != null && value is string )
            {
                playerName = value;
            }
        }
    }
    
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
    
    
    /*
     * public void LoadPlayerName()
    {
        string path = Application.persistentDataPath + "/savename.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            MainManager.SaveData data = JsonUtility.FromJson<MainManager.SaveData>(json);
            bestPlayer.text = "Best Score : " + data.playerName + " : " + data.score;
        }
    }
    
    
    
    
    public void SavePlayerName(int points)
    {
        SaveData data = new SaveData();
        data.playerName = GameManager.Instance.playerName;
        data.score = points;
        
        string json = JsonUtility.ToJson(data);
        File.WriteAllText( Application.persistentDataPath + "/savename.json", json);
    }
    
     */

}
