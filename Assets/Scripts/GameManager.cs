using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private static PlayerData data;

    public static PlayerData PlayerData
    {
        get => data;
    }

    public static GameManager Instance
    {
        get => instance;
    }

    private void Awake()
    {
        if (instance != null)
        {
            this.gameObject.SetActive(false);
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
    }

    public void LoadLevel(Level level)
    {
        SceneManager.LoadScene(level.ToString());
    }
}
