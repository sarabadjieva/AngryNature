using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private static PlayerData data;

    public bool paused;

    public static PlayerData PlayerData
    {
        get => data;
    }

    public static GameManager Instance
    {
        get
        { 
            if (instance == null)
            {
                GameObject container = new GameObject("GameManager");
                instance = container.AddComponent<GameManager>();
            }

            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);

        if (SaveSystem.FileExists())
        {
            SaveSystem.DeleteFile();
            data = new PlayerData();
            //data = SaveSystem.LoadData();
        }
        else
        {
            data = new PlayerData();
        }

        if (!StaticData.hasMainSceneLoaded && SceneManager.GetActiveScene().name == "MainScene")
        {
            StaticData.hasMainSceneLoaded = true;
        }
    }

    public void LoadLevel(Level level)
    {
        PlayerData.currentLevel = level.ToString();

        AudioManager.Instance.PlaySpawn();
        SceneManager.LoadScene(level.ToString());
    }

    public void ReloadLevel()
    {
        AudioManager.Instance.PlaySpawn();
        SceneManager.LoadScene(PlayerData.currentLevel);
    }
}
