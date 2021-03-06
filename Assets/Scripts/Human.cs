using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Human : MonoBehaviour
{
    [SerializeField] private int healt = 5;

    [SerializeField] private int points;

    [SerializeField] private float leftBound;

    [SerializeField] private float rightBound;

    [SerializeField] private Slider healtBar;

    private float speed = 5;
    private Transform healtBarTransform;

    // Start is called before the first frame update
    
    void Start()
    {
        healtBarTransform = healtBar.transform;
        healtBar.minValue = 0;
        healtBar.maxValue = healt;
        healtBar.value = healt;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > rightBound)
        {
            transform.rotation = Quaternion.Euler(0,-90f,0);
            healtBar.transform.position = healtBarTransform.position;
        }
        else
        {
            if (transform.position.x < leftBound)
            {
                transform.rotation = Quaternion.Euler(0,90f,0);
                healtBar.transform.position = healtBarTransform.position;
            }
        }
        
        healtBar.transform.rotation = Quaternion.identity;
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        
        if (healt <= 0)
        {
            GameManager.Instance.score += points;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Egg"))
        {
            healt--;
            healtBar.value = healt;
        }
    }

    public void ApplyDamage(int damage)
    {
        healt -= damage;
        healtBar.value = healt;
    }
}
