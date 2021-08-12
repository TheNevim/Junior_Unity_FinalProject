using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Animal : MonoBehaviour
{
    protected Rigidbody animalRb;
    protected Animator animalAnim;

    private float horizontalInput = 0;
    protected abstract float speed
    {
        get;
    }
    
    protected abstract bool isAttacking
    {
        get;
        set;
    }

    protected bool isGrounded = true;
    private bool isAlive = true;
    
    protected abstract int healt
    {
        get;
        set;
    }

    private void Awake()
    {
        animalRb = gameObject.GetComponent<Rigidbody>();
        animalAnim = gameObject.GetComponent<Animator>();
    }

    private void Start()
    {
        animalAnim.SetFloat("Speed_f", 0f);
    }

    // Update is called once per frame
    void Update()
    {
        //Jump if space is pressed
        if (Input.GetKeyDown(KeyCode.Space) && isAlive)
        {
            animalAnim.SetFloat("Speed_f", 0f);
            Jump();
            isGrounded = false;
        }
        
        if (Input.GetKeyDown(KeyCode.F) && isAlive)
        {
            Attack();
        }
        
        //Get horizontal input
        horizontalInput = Input.GetAxis("Horizontal");
        
        if (horizontalInput == 0)
        {
            // Animal is not moving
            animalAnim.SetFloat("Speed_f", 0f);
        }
        else
        {
            if (!isAttacking && isAlive)
            {
                if (horizontalInput > 0)
                {
                    // Right arrow presed -> move forward at animal speed
                    transform.Translate(Vector3.forward * Time.deltaTime * speed * horizontalInput);
                    //Look right
                    transform.rotation = Quaternion.Euler(0,90f,0);
                    //Start animation
                    animalAnim.SetFloat("Speed_f", 0.5f);
                }
                else
                {
                    // Left arrow presed -> move back at animal speed
                    transform.Translate(Vector3.back * Time.deltaTime * speed * horizontalInput);
                    //Look left
                    transform.rotation = Quaternion.Euler(0,-90f,0);
                    //Start animation
                    animalAnim.SetFloat("Speed_f", 0.5f);
                } 
            }
        }
    }

    protected virtual void Jump() { }
    protected virtual void Attack() { }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            Debug.Log("got food");
        }
        
        if (other.CompareTag("Human"))
        {
            healt--;
            if (healt == 0)
            {
                GameManager.Instance.SavePlayerName(1);
                isAlive = false;
            }
        }
        
    }
    
    
}
