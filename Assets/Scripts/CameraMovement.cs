using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private List<GameObject> animals = new List<GameObject>();

    private Transform playerTransform;
    private Vector3 spawnPosition = new Vector3(-3f,0.3f,0f);
    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject animal in animals)
        {
            Debug.Log(animal.name);
            if (animal.name == GameManager.Instance.playerAnimal)
            {
                var gameAnimal = Instantiate(animal, spawnPosition, animal.transform.rotation);
                playerTransform = gameAnimal.GetComponent<Transform>();
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
