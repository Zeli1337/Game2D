using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    public int playerLives{set;get;} = 3;

    [Header("Textelemente HUD")]
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI StatsText;

    [SerializeField] TextMeshProUGUI Ammunition;

    [SerializeField] TextMeshProUGUI levelMessage;

    [SerializeField] TextMeshProUGUI invicTimer;
    [SerializeField] TextMeshProUGUI doorHint;
    public int score {set;get;} = 0;
    public int ammo {get;set;} = 20;
    public int ammoMax {get;set;} = 20;
    public double time { set; get; }

    private float time_float;

    public int currentScene {set;get;}

    private int saveGameScene;

    

    

   public void updateIndex(int index)
    {
        currentScene = index;
        saveSession();

    }
    
    public void saveSession()
    {
        //currentScene = FindObjectOfType<LevelExit>().getCurrentScene();
        Debug.Log(currentScene);
        
        SaveSystem.saveSession(this);



    }

    private void deleteSaveFile()
    {
        SaveSystem.deleteSaveGame();
    }

    public void loadSession()
    {
       
        if(SaveSystem.loadSession() == null){return;} // Falls Spielstand nicht vorhanden skippt funktion
        Debug.Log("DataPassed");
        PlayerData data = SaveSystem.loadSession(); //Importiert daten der SaveSystem Klasse // Speicherstand
        score = data.score;
        ammo = data.ammo;
        ammoMax = data.ammoMax;
        currentScene = data.level;
        time = data.overallTime;
        SceneManager.LoadScene(currentScene);
        playerLives = data.lives;
        updataUI();



    }

    private void Update() 
    {
        //currentScene = FindObjectOfType<LevelExit>().getCurrentScene();
        //playTime();
        
        updataUI();
        

        
    }

    void Awake()
    {
        
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        invicTimer.enabled = false;
        
        hideDoorHint();
        //invicTimer.gameObject.SetActive(false);
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
        
    }

    private void Start()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();
        Ammunition.text = $"{ammo.ToString()} / {ammoMax.ToString()}";
        //Setzt Messages auf disabled damit diverse HUD Elemente nicht angezeigt werden
        levelMessage.enabled = false;
        StatsText.enabled = false;
        invicTimer.enabled = false;
        doorHint.enabled = false;
        
    }

    private void playTime() //Spielzeit versuch
    {
        if (SceneManager.GetActiveScene().buildIndex > 1 && SceneManager.GetActiveScene().buildIndex != SceneManager.sceneCountInBuildSettings) //1< x damit das Hauptmenü nicht dazu zählt
        {
            time_float += Time.deltaTime;

        }
        else
        {
            StatsText.enabled = true;
            StatsText.text = $"Your total playtime took {Convert.ToDouble(Math.Round(time_float))}";
        }
    }

    void updataUI()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();
        Ammunition.text = $"{ammo.ToString()} / {ammoMax.ToString()}";
        //levelMessage.enabled = false;
  
        if(FindObjectOfType<PlayerMovementScript>().isInvincible)
        {
            double invic = Math.Round(Convert.ToDouble(FindObjectOfType<PlayerMovementScript>().returnInvicTimer()),2);
            //invicTimer.gameObject.SetActive(true);
            invicTimer.enabled = true;
            invicTimer.text = $"Invincible for {invic}";
            return;
        }else
        {
            invicTimer.enabled = false;
            //invicTimer.gameObject.SetActive(false);
            
        }
        
        


    }
    public void showLevelTip()
    {
        levelMessage.enabled = true;

    }

    public void hideLevelTip()
    {
        levelMessage.enabled = false;
    }

    public void showDoorHint() //Door hint on > Serialized Field!!!
    {

        doorHint.enabled = true;
        


    }

    public void hideDoorHint() //Door hint off > Serialized Field!!!
    {

        doorHint.enabled = false;
        
    }

    public void ammoPickup()
    {
        this.ammo = this.ammoMax;
        
    }



 
    public void ProcessPlayerDeath()
    {
        if(playerLives > 1)
        {
            
            StartCoroutine(TakeLife());
            

        }
        else
        {
            deleteSaveFile();
            ResetGameSession();
            livesText.text = playerLives.ToString();

        }
    }

    public void coinCollected()
    {
        score += 100;
        scoreText.text = score.ToString();

    }

    public void enemyKill()
    {
        score += 25;
        scoreText.text = score.ToString();
    }

    public void heartCollected()
    {
        playerLives +=1;
        updataUI();
    }

    public void shootShot()
    {
        ammo--;
        Ammunition.text = $"{ammo.ToString()} / {ammoMax.ToString()}";

    }

    IEnumerator TakeLife()
    {
        yield return new WaitForSecondsRealtime(2);
        playerLives--;
        ammo = 20;
        Ammunition.text = $"{ammo.ToString()} / {ammoMax.ToString()}";
        currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
        livesText.text = playerLives.ToString();
    }

    public void ResetGameSession()
    {
        
        FindObjectOfType<ScenePersist>().resetPersist();
        
        SceneManager.LoadScene(0);
        
        Destroy(gameObject);
    }

    public void ResetGameSessionMenu ()
    {
        //saveSession();
        FindObjectOfType<ScenePersist>().resetPersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
        
        

        
    }

    public void ResetGameAll()
    {
        
        SaveSystem.deleteSaveGame();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }


}
