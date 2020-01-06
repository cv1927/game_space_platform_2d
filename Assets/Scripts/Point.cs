using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{

    ParticleSystem pointParticle;
    AudioSource pointAudio;

    private void Start()
    {
        pointParticle = GameObject.Find("ParticlePoint").GetComponent<ParticleSystem>();
        pointAudio = GetComponentInParent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pointParticle.transform.position = transform.position;
            pointParticle.Play();
            pointAudio.Play();
            Destroy(gameObject);
        }
    }
}
