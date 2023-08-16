using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 20f;
    [SerializeField] AudioClip BulletSFX;
    [SerializeField] AudioClip EnemyDieSFX;
    Rigidbody2D myRigidBody;
    PlayerMovement player;
    float xSpeed;
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        xSpeed = player.transform.localScale.x * bulletSpeed;
        PlayBulletSound();
    }

    void Update()
    {
        myRigidBody.velocity = new Vector2(xSpeed, 0f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            PlayEnemyDieSound();
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }

    void PlayBulletSound()
    {
        AudioSource.PlayClipAtPoint(BulletSFX, transform.position);
    }

    void PlayEnemyDieSound()
    {
        AudioSource.PlayClipAtPoint(EnemyDieSFX, transform.position);
    }
}
