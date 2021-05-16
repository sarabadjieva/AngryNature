using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Level
{
    Ice = 1,
    Forest
}

public class LevelController : MonoBehaviour
{
    private void Awake()
    {
        if (!StaticData.hasMainSceneLoaded)
        {
            StaticData.hasMainSceneLoaded = true;
            SceneManager.LoadScene("MainScene");
            return;
        }

        PlayerController.PlayerData.health = 3;
    }
}
