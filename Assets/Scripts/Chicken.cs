using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : Animal
{
    [SerializeField] private GameObject egg;
    private float animalSpeed = 10f;
    protected override float speed => animalSpeed;
    protected override bool isAttacking { get; set; } = false;
    protected override int healt { get; set; } = 2;

    private float jumpPower = 10f;
    private int jumpCount = 3;

    private bool hasLayedegg = false;
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
        if (!isAttacking && isGrounded && !hasLayedegg)
        {
            isAttacking = true;
            hasLayedegg = true;
            Instantiate(egg, gameObject.transform.FindChild("Cloaka").position, gameObject.transform.rotation);
            StartCoroutine(ResetLayedEgg());
        }
    }

    IEnumerator ResetLayedEgg()
    {
        yield return new WaitForSeconds(1f);
        isAttacking = false;
        yield return new WaitForSeconds(1f);
        hasLayedegg = false;
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
