using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public static class SaveSystem 
{

    public static void saveSession(GameSession session)
    {
        BinaryFormatter formatter = new BinaryFormatter(); 
        string path = Application.persistentDataPath + "/player.johnxina";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(session);

        formatter.Serialize(stream, data); // Speichert Datei in Binärcode um Savefile editing zu behindern
        stream.Close();

    }

    public static PlayerData loadSession()
    {
        string path = Application.persistentDataPath + "/player.johnxina";
        if(File.Exists(path))
        {
            
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return data;
            

        }else
        {
            Debug.Log($"No file Found {path}" );
            return null;
            
        }
    }

    public static bool doesSaveGameExist()
    {
        string path = Application.persistentDataPath + "/player.johnxina";
        return File.Exists(path);
        
    }
    public static void deleteSaveGame()
    {
        string path = Application.persistentDataPath + "/player.johnxina";
        if(File.Exists(path))
        {
            
            File.Delete(path);

            
            

        }else
        {
            Debug.Log($"kein Savegame zum deleten {path}" );
            
            
        }
    }
}
