using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour  //Siehe Ammo Pickup

{
    [Header("AudioClip Muenze")]
    [SerializeField] AudioClip coinClip;
    CircleCollider2D collidor;

    bool wasCollected = false;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        

        if (collision.gameObject.tag == "Player" && !wasCollected)
        {
            AudioSource.PlayClipAtPoint(coinClip, Camera.main.transform.position);
            FindObjectOfType<GameSession>().coinCollected();
            wasCollected = true;
            Destroy(gameObject);
            
            

        }

    }
    



}
