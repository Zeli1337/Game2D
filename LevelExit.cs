using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelExit : MonoBehaviour
{

    [SerializeField] int time = 1;





    private void OnTriggerEnter2D(Collider2D collision)

    {
        // Message einfügen lol
        
        
        FindObjectOfType<GameSession>().showLevelTip();

        

        

    }

    

    private void OnTriggerExit2D(Collider2D other) 
    {
        FindObjectOfType<GameSession>().hideLevelTip();    
    }

    public void goNext()
    {
        FindObjectOfType<ScenePersist>().resetPersist();
        
        StartCoroutine(LoadNextLevel());
        
    }

    public int getCurrentScene()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    IEnumerator LoadNextLevel()
    {
        
        yield return new WaitForSecondsRealtime(time);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        
        FindObjectOfType<GameSession>().hideLevelTip();

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;    
        }
        SceneManager.LoadScene(nextSceneIndex);
        FindObjectOfType<GameSession>().updateIndex(nextSceneIndex);
        
        
        
        




    }
}
