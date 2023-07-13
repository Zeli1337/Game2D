using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorScript : MonoBehaviour
{

    //Hinweis für Türen
    private void OnCollisionEnter2D(Collision2D collision)
    {

        FindObjectOfType<GameSession>().showDoorHint();

        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
        FindObjectOfType<GameSession>().hideDoorHint();

            
    }
}
