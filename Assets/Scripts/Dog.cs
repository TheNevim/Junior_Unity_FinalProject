using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class Dog : Animal
{
    // Start is called before the first frame update
    private float animalSpeed = 7f;
    protected override float speed => animalSpeed;
    protected override bool isAttacking { get; set; } = false;
    protected override int healt { get; set; } = 3;

    private float jumpPower = 260f;

    //POLYMORPHISM
    protected override void Jump()
    {
        if (base.isGrounded)
        {
            animalRb.AddForce(Vector3.up * jumpPower,ForceMode.Impulse);
        }
        
    }

    IEnumerator ResetBarkAnimation()
    {
        //Reset animation after2 seconds
        yield return new WaitForSeconds(2);
        animalAnim.SetBool("Bark_b", false);
        isAttacking = false;
    }

    //POLYMORPHISM
    protected override void Attack()
    {
        if (isGrounded && !isAttacking)
        {
            animalAnim.SetBool("Bark_b", true);
            isAttacking = true;
            StartCoroutine(ResetBarkAnimation());
            
            //Get all colider within in radius
            Collider[] colliders = Physics.OverlapSphere(transform.position + new Vector3(3,0,0), 5);
            
            foreach (Collider hit in colliders) {
                if (hit.gameObject.CompareTag("Human"))
                {
                    hit.gameObject.GetComponent<Human>().ApplyDamage(2);
                }
            }
            //animalRb.AddExplosionForce(500, transform.position + new Vector3(5,0,0), 550f , 0f ,ForceMode.Impulse);
        }
    }
}
