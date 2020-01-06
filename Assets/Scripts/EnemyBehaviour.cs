using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private Rigidbody2D rb2d;
    float timeBeforeChange;
    public float delay = .5f;
    public float speed = .3f;
    private Animator enemyAnimator;

    ParticleSystem enemyParticle;


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();
        enemyParticle = GameObject.Find("ParticleEnemy").GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        rb2d.velocity = Vector2.right * speed;

        if (timeBeforeChange < Time.time)
        {
            speed *= -1;
            if (speed > 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            timeBeforeChange = Time.time + delay;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (transform.position.y + .03f < collision.transform.position.y)
            {
                enemyAnimator.SetBool("isDead", true);
            }
        }
    }

    public void DisabledEnemy()
    {
        enemyParticle.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        enemyParticle.Play();
        Destroy(gameObject);
    }
}
