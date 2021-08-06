using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = player.GetComponent<Transform>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.Translate(new Vector3(playerTransform.position.x-transform.position.x,0,0));
    }
}
