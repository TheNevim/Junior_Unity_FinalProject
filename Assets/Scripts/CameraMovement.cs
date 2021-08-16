using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private List<GameObject> animals = new List<GameObject>();

    [SerializeField] private TextMeshProUGUI lifeText;
    [SerializeField] private TextMeshProUGUI pointText;
    

    private Transform playerTransform;
    private Vector3 spawnPosition = new Vector3(0f,0.3f,0f);

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject animal in animals)
        {
            Debug.Log(animal.name);
            if (animal.name == GameManager.Instance.playerAnimal)
            {
                var gameAnimal = Instantiate(animal, spawnPosition, animal.transform.rotation) ;
                playerTransform = gameAnimal.GetComponent<Transform>();
                gameAnimal.GetComponent<Animal>().setGameGui(lifeText,pointText);
                
                gameAnimal.name = "Player";
                break;
            }
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.Translate(new Vector3(playerTransform.position.x-transform.position.x,0,0));
    }
}
