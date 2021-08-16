using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class Animal : MonoBehaviour
{
    protected Rigidbody animalRb;
    protected Animator animalAnim;

    private TextMeshProUGUI lifeText;
    private TextMeshProUGUI pointsText;

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
    
    private int pointsPerLevel = 50;
    
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

        if (transform.position.y < -10 && isAlive)
        {
            GameOver();
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

    public void setGameGui(TextMeshProUGUI life, TextMeshProUGUI points)
    {
        this.lifeText = life;
        this.pointsText = points;

        lifeText.text = "Live: " + healt;
        pointsText.text = "Points " + GameManager.Instance.score;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void GameOver()
    {
        GameManager.Instance.SavePlayerName(GameManager.Instance.score);
        isAlive = false;
                
        GameObject[] humans = GameObject.FindGameObjectsWithTag("Human");

        for (int i = 0; i < humans.Length; i++)
        {
            humans[i].GetComponent<Human>().enabled = false;
            humans[i].GetComponent<Animator>().SetFloat("Speed_f", 0);
        }

        GameObject filenamefld = GameManager.Instance.FindGameobject("Main Camera", "GameOver");
        filenamefld.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            GameManager.Instance.score += 50;

            if (GameManager.Instance.level == 2)
            {
                GameOver();
                GameManager.Instance.WonGame();
                return;
            }
            GameManager.Instance.NextLevel();
        }
        
        if (other.CompareTag("Human"))
        {
            healt--;
            lifeText.text = "Live: " + healt;
            
            if (healt == 0)
            {
                GameOver();
            }
        }
    }
}
