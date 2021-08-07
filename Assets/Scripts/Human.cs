using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Human : MonoBehaviour
{
    [SerializeField] private int healt = 5;

    [SerializeField] private float leftBound;

    [SerializeField] private float rightBound;

    [SerializeField] private Slider healtBar;

    private float speed = 5;
    
    // Start is called before the first frame update
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > rightBound)
        {
            transform.rotation = Quaternion.Euler(0,-90f,0);
        }
        else
        {
            if (transform.position.x < leftBound)
            {
                
                transform.rotation = Quaternion.Euler(0,90f,0);
            }
        }
        
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
