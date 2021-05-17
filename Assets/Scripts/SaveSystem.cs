using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


class SaveSystem
{
    private readonly static string path_player_data = Application.persistentDataPath + "/player.bin";

    public static void SavePlayer()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        //player data
        FileStream stream = new FileStream(path_player_data, FileMode.Create);
        //PlayerData data = new PlayerData(tamagotchi);
        formatter.Serialize(stream, GameManager.PlayerData);
        stream.Close();
    }

    public static PlayerData LoadData()
    {
        if (File.Exists(path_player_data))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path_player_data, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path_player_data);
            return null;
        }
    }

    public static bool FileExists()
    {
        return File.Exists(path_player_data);
    }

    public static void DeleteFile()
    {
        File.Delete(path_player_data);
    }
}