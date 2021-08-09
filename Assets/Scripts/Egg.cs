using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    [SerializeField] private ParticleSystem explosion;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyEgg());
    }

    IEnumerator DestroyEgg()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Human"))
        {
            StopCoroutine(DestroyEgg());
            ParticleSystem effect = Instantiate(explosion, transform.position, Quaternion.identity);
            effect.Play();
            
            gameObject.SetActive(false);
            Destroy(effect.gameObject, 4f);
            Destroy(gameObject,4f);
        }
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Animal"))
        {
            Physics.IgnoreCollision(other.gameObject.GetComponent<BoxCollider>(), gameObject.GetComponent<CapsuleCollider>());
        }
        
        if (other.gameObject.CompareTag("Ground"))
        {
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
        
        
    }
}
