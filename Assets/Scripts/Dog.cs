using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : Animal
{
    // Start is called before the first frame update
    private float animalSpeed = 7f;
    protected override float speed => animalSpeed;

    private float jumpPower = 230f;

    protected override void Jump()
    {
        animalRb.AddForce(Vector3.up * jumpPower,ForceMode.Impulse);
    }
}
