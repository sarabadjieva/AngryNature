using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUIManager : MonoBehaviour
{
    public static GUIManager instance;

    [Header("Main Menu")]
    public GameObject goMainMenu;
    public GameObject goGridChooseLevel;

    [Header("Game Over Menu")]
    public GameObject goGameOverMenu;


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

        OpenMenu(MenuType.Main);
    }

    private void OpenMenu(MenuType type)
    {
        goMainMenu.SetActive(type == MenuType.Main);
        goGameOverMenu.SetActive(type == MenuType.GameOver);
    }

    //MAIN MENU
    public void OpenMainMenu()
    {
        OpenMenu(MenuType.Main);
    }

    public void OnClickChooseLevel()
    {
        goGridChooseLevel.SetActive(true);
    }

    public void OnClickLevel(int levelNum)
    {
        switch (levelNum)
        {
            case 1:
                SceneManager.LoadScene("IceIceBaby");
                break;

            default:
                break;
        }

        OpenMenu(MenuType.None);
    }

    public void OnClickSettings()
    {

    }

    //GAME OVER MENU
    public void OpenGameOverMenu()
    {
        OpenMenu(MenuType.GameOver);
    }

    public void OnClickRetry()
    {

    } 
    
    public void OnClickMainMenu()
    {

    } 
    
    public void OnClickExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
        return;
    }

    private enum MenuType
    {
        None,
        Main,
        GameOver
    }
}
