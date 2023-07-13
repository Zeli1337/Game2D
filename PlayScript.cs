using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayScript : MonoBehaviour


{


    
    

    private void Awake() 
    {
        FindObjectOfType<GameSession>().ResetGameSession();    
    }

    public void Play()
    {

        if(SaveSystem.doesSaveGameExist())
        {
            FindObjectOfType<GameSession>().loadSession();

        } else
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex + 1);

        }     
        

        
       

    }

    public void replay()
    {
        //FindObjectOfType<GameSession>().ResetGameSessionMenu();
        //SaveSystem.deleteSaveGame();
        //SceneManager.LoadScene(0);
        FindObjectOfType<GameSession>().ResetGameAll();
        Destroy(this);
        
        

    }

    public void quitGame()
    {
        Application.Quit();
    }
}
