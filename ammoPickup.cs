using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammoPickup : MonoBehaviour
{
    [Header("Schusssound")]
    [SerializeField] AudioClip sound;
    bool collected = false;


    private void OnCollisionEnter2D(Collision2D other) { //Checkt Kollision 

        if(other.gameObject.tag.Equals("Player") && !collected){ 
            FindObjectOfType<GameSession>().ammoPickup(); //Erhöht Munition in Gamesession und aktualisiert HUD
            AudioSource.PlayClipAtPoint(sound, Camera.main.transform.position); // Spielt Sound @ Kamera
            Destroy(gameObject);
            collected = true;
            

        }
        
    }
}
