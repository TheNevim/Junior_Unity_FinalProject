using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : Animal
{
    private float animalSpeed = 15f;
    protected override float speed => animalSpeed;
    
    private float jumpPower = 200f;

    protected override void Jump()
    {
        if (base.isGrounded)
        {
            animalRb.AddForce(Vector3.up * jumpPower,ForceMode.Impulse);
        }
        
    }
}
