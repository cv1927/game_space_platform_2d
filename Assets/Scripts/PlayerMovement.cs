using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float speed = 2.5f;
    public float jumpSpeed = 300;

    public bool isGrounded = true;

    private Animator playerAnimator;

    private int points = 0;

    public Text pointsText;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rb2d.velocity = new Vector2(Input.GetAxis("Horizontal") * speed,rb2d.velocity.y);

        AnimationController();

        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GetComponent<AudioSource>().Play();
                rb2d.AddForce(Vector2.up * jumpSpeed);
                isGrounded = false;
                playerAnimator.SetTrigger("Jump");
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Point"))
        {
            points++;
            pointsText.text = "x" + points;
        }   
    }

    private void AnimationController()
    {
        if (Input.GetAxis("Horizontal") == 0 && isGrounded)
        {
            playerAnimator.SetBool("isWalking", false);
        }
        else if (Input.GetAxis("Horizontal") > 0.1 && isGrounded)
        {
            playerAnimator.SetBool("isWalking", true);
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (Input.GetAxis("Horizontal") < 0.1 && isGrounded)
        {
            playerAnimator.SetBool("isWalking", true);
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }
}
