using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : Animal
{
    [SerializeField] private GameObject egg;
    private float animalSpeed = 10f;
    protected override float speed => animalSpeed;
    protected override bool isAttacking { get; set; } = false;

    private float jumpPower = 10f;
    private int jumpCount = 3;

    private bool hasLayedEgg = false;

    protected override void Jump()
    {
        if (jumpCount > 0)
        {
            animalRb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            jumpCount--;
        }
    }

    protected override void Attack()
    {
        if (!hasLayedEgg && isGrounded)
        {
            isAttacking = true;
            hasLayedEgg = true;
            Instantiate(egg, transform.position, gameObject.transform.rotation);
            StartCoroutine(ResetLayedEgg());
        }
    }

    IEnumerator ResetLayedEgg()
    {
        yield return new WaitForSeconds(1.5f);
        hasLayedEgg = false;
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
