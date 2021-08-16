using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIScript : MonoBehaviour
{
    
    List<String> animalPlayer = new List<string>() { "Dog", "Chicken", "Fox" };

    [SerializeField] private InputField playerName;
    [SerializeField] private TextMeshProUGUI animalField;
    [SerializeField] private TextMeshProUGUI topScoreTable;
    
    private int topTableNumber = 5;
    
    // Start is called before the first frame update
    void Start()
    {
        List<GameManager.SaveData> scoreTable = GameManager.Instance.LoadPlayersTable(0);
        if (scoreTable.Count < topTableNumber)
        {
            topTableNumber = scoreTable.Count;
        }
        for (int i = 0; i < topTableNumber; i++)
        {
            GameManager.SaveData readData = scoreTable.ElementAt(i);
            topScoreTable.text += readData.playerName + "\t\t" + readData.animal + "\t\t" + readData.level + "\t" +
                                  readData.score + "\n";
        }
        animalField.text = animalPlayer.First();

        if (GameManager.Instance.playerName != null)
        {
            playerName.text = GameManager.Instance.playerName;
        }
    }
    
    public void StartGame()
    {
        if (string.IsNullOrEmpty(playerName.text))
        {
            EditorUtility.DisplayDialog("Player name not set",
                "Please enter player name", "Ok");
            return;
        }
        GameManager.Instance.playerAnimal = animalField.text;
        GameManager.Instance.playerName = playerName.text;
        GameManager.Instance.level = 1;
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
       
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }

    public void ChooseNextAnimal()
    {
        var playerIndex = animalPlayer.IndexOf(animalField.text);
       
        if ( playerIndex == animalPlayer.Count-1)
        {
            animalField.text = animalPlayer.First();
        }
        else
        {
            animalField.text = animalPlayer.ElementAt(playerIndex+1);
        }
        
    }
    
    public void ChoosePrevAnimal()
    {
        var playerIndex = animalPlayer.IndexOf(animalField.text);
        
        if ( playerIndex == 0)
        {
            animalField.text = animalPlayer.Last();
        }
        else
        {
            animalField.text = animalPlayer.ElementAt(playerIndex-1);
        }
    }
    
}
