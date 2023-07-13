using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D myRigidBody;
    [SerializeField] float movespeed = 1f;
    [SerializeField] ParticleSystem deathParticlesEnemy;
    BoxCollider2D myBoxCol;

    private void Awake()
    {
        //deathParticlesEnemy.Stop();
    }
    void Start()
    {
        
        myRigidBody = GetComponent<Rigidbody2D>();
        myBoxCol = GetComponent<BoxCollider2D>();
        deathParticlesEnemy.Stop();

    }
    


    public void death()
    {
        deathParticlesEnemy.Play();
        return;
    }
    
    void Update()
    {
        myRigidBody.velocity = new Vector2(movespeed, 0);

        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        movespeed = -movespeed;
        FlipEnemyFacing();
        
    }

    private void FlipEnemyFacing()
    {
        transform.localScale = new Vector2(-(Math.Sign(myRigidBody.velocity.x)),1f); //Dreht Charakter über Sign
    }
}
