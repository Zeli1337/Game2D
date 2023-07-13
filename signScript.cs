using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class signScript : MonoBehaviour
{
    
    [SerializeField] TextMeshProUGUI signText;
    int Scene; 
    private void Start()
    {
        signText.enabled = false;
    }

    void Update()
    {
        Scene = SceneManager.GetActiveScene().buildIndex;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision enter");

        if (collision.gameObject.tag.Equals("Player"))
        {

            signText.enabled = true;
            signText.text = findText();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Debug.Log("Collision enter");
            signText.enabled = false;

        }
    }



    string findText()
    {
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 1:
                return "Try moving using WASD and the spacebar";
            case 2:
                return "enemies can be shot by pressing Mouse1\n but only while standing still.";
            default:
                return "";
                break;
        }
    }
}
