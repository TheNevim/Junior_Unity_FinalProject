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
    
    // Start is called before the first frame update
    void Start()
    {
        animalField.text = animalPlayer.First();
    }
    
    public void StartGame()
    {
        GameManager.Instance.playerAnimal = animalField.text;
        GameManager.Instance.playerName = playerName.text;
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
