using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : Animal
{
    // Start is called before the first frame update
    private float animalSpeed = 7f;
    protected override float speed => animalSpeed;
    protected override bool isAttacking { get; set; } = false;

    private float jumpPower = 230f;

    protected override void Jump()
    {
        if (base.isGrounded)
        {
            animalRb.AddForce(Vector3.up * jumpPower,ForceMode.Impulse);
        }
        
    }

    protected override void Attack()
    {
        //animalAnim.SetBool("Bark_b", true);
    }
}
