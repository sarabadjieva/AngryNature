using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.Rendering.Universal;

public class LevelController : MonoBehaviour
{
    public GameObject goMainLevel;
    public GameObject goSecretLevel;

    private List<Light2D> sceneLights;

    private void Awake()
    {
        if (!StaticData.hasMainSceneLoaded)
        {
            StaticData.hasMainSceneLoaded = true;
            SceneManager.LoadScene("MainScene");
            return;
        }

        goMainLevel.SetActive(true);
        goSecretLevel.SetActive(false);

        
    }

    public void OpenSecretLevel(bool open)
    {
        goMainLevel.SetActive(!open);
        goSecretLevel.SetActive(open);
    }

    private void LightTransition(bool show)
    {

    }
}
