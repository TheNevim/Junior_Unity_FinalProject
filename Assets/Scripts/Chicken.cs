using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : Animal
{
    private float animalSpeed = 10f;
    protected override float speed => animalSpeed;
    
    private float jumpPower = 10f;
    private int jumpCount = 3;

    protected override void Jump()
    {
        if (jumpCount > 0)
        {
            animalRb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            jumpCount--;
        }
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpCount = 3;
        }
    }
}
