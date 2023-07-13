using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour //Kugelklasse -> eigenes Objekt Waffe
{
    Rigidbody2D bulletBody;
    PlayerMovementScript player;
    Animator idleChecker;
    [Header("Kugelgeschwindigkeit")]
    [SerializeField] float bulletSpeed = 20f;

    
    float xSpeed;
    void Start()
    {
        bulletBody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovementScript>();
        xSpeed = player.transform.localScale.x *bulletSpeed;
        idleChecker = player.GetComponent<Animator>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
        bulletBody.velocity = new Vector2(xSpeed,0f);
    }

    private bool checkifIdle() // animation
    {
        return idleChecker.GetBool("IdleAni");

    }

    //Löscht Kugeln beim Töten 
    private void OnTriggerEnter2D(Collider2D other) {
        
        if(other.tag == "enemy")
        {
            other.gameObject.GetComponent<ParticleSystem>().Play();
            Destroy(other.gameObject);
            
            FindObjectOfType<GameSession>().enemyKill();
            

        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "enemy")
        {
            
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }
    
}
