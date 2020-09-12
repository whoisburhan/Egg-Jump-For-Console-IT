using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyEggScript : MonoBehaviour
{
    Rigidbody2D rb2d;
    private float jumpForce = 400f;

    private void Start() => rb2d = GetComponent<Rigidbody2D>();

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Ground")
        {
            print("Egg is Landed");
            //   isGrounded = true;
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0f);
            rb2d.AddForce(Vector2.up * jumpForce);

        }
    }
}
