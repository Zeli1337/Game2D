using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int level;
    public int lives; 
    public int ammo;
    public int ammoMax;
    public int score;
    public double overallTime;


    public PlayerData(GameSession session)
    {
        lives = session.playerLives;
        ammo = session.ammo;
        ammoMax = session.ammoMax;
        score = session.score;
        level = session.currentScene;
        overallTime = session.time;


    }
    

}
