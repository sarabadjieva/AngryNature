using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUIManager : MonoBehaviour
{
    private static GUIManager instance;

    private readonly Vector2 transitionMainMenuShown =new Vector2(0f, 30f);
    private readonly Vector2 transitionMainMenuHidden = new Vector2(2000f, 30f);

    [Header("Main Menu")]
    public GameObject goMainMenu;
    public RectTransform rectMainButtons;
    public RectTransform rectSubmenu;
    public GameObject goSubmenuLevels;
    public GameObject goSubmenuSettings;

    [Header("Game Over Menu")]
    public GameObject goGameOverMenu;

    [Header("HUD")]
    public GameObject goHUD;
    public Text txtTimer;
    public Text txtGameInfoLeft;
    public Text txtGameInfoRight;

    [Header("Pause Menu")]
    public GameObject goPauseMenu;

    [Header("Transitions")]
    public GameObject goLevelTransition;
    public TextMeshProUGUI txtLevel;

    [Space]
    public List<ButtonEffects> soundButtons = new List<ButtonEffects>();

    private bool isSubmenuShown = false;
    private float timePaused = 0f;
    private int currentSoundButtonSpriteIndex;

    public static GUIManager Instance
    {
        get => instance;
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

        OpenMenu(MenuType.Main);
        goSubmenuLevels.SetActive(false);
        goSubmenuSettings.SetActive(false);

        DeleteGameInfo();
    }

    private void Update()
    {
        if (GameManager.Instance.paused)
        {
            timePaused += Time.deltaTime;
            return;
        }

        if (Time.timeSinceLevelLoad <1f)
        {
            timePaused = 0f;
        }

        if (goHUD.activeInHierarchy)
        {
            txtTimer.text =  (Time.timeSinceLevelLoad - timePaused).ToString();
        }
    }

    private void OpenMenu(MenuType type)
    {
        goMainMenu.SetActive(type == MenuType.Main);
        goGameOverMenu.SetActive(type == MenuType.GameOver);
        goHUD.SetActive(type == MenuType.HUD);
        goPauseMenu.SetActive(type == MenuType.Pause);
         
        GameManager.Instance.paused = true;

        if (type == MenuType.HUD || type == MenuType.None)
        {
            GameManager.Instance.paused = false;
        }
    }

    #region MAIN MENU
    public void OpenMainMenu()
    {
        rectMainButtons.anchoredPosition = transitionMainMenuShown;
        rectSubmenu.anchoredPosition = transitionMainMenuHidden;
        isSubmenuShown = false;

        OpenMenu(MenuType.Main);
    }

    public void OnClickChooseLevel()
    {
        goSubmenuLevels.SetActive(true);
        goSubmenuSettings.SetActive(false);

        StartCoroutine(ToggleSubmenu());
    }

    public void OnClickLevel(int levelNum)
    {
        StartCoroutine(LevelTransitionAnim(levelNum));
    }

    public void OnClickSettings()
    {
        goSubmenuLevels.SetActive(false);
        goSubmenuSettings.SetActive(true);

        StartCoroutine(ToggleSubmenu());
    }

    public void OnClickSound()
    {
        foreach (ButtonEffects effects in soundButtons)
        {
            effects.SwitchSprites(currentSoundButtonSpriteIndex == 0 ? 1 : 0);
        }
        currentSoundButtonSpriteIndex = currentSoundButtonSpriteIndex == 0 ? 1 : 0;

        AudioManager.Instance.Mute = !AudioManager.Instance.Mute;
    }

    public void OnClickInfo()
    {

    }

    public void OnClickCloseSubmenu()
    {
        StartCoroutine(ToggleSubmenu());
    }

    private IEnumerator ToggleSubmenu()
    {
        RectTransform rectShown;
        RectTransform rectHidden;

        if (!isSubmenuShown)
        {
            rectShown = rectMainButtons;
            rectHidden = rectSubmenu;
        }
        else
        {
            rectShown = rectSubmenu;
            rectHidden = rectMainButtons;
        }

        float totalTime = 0.5f;
        float currentTime = 0;
        while (currentTime < totalTime)
        {
            rectShown.anchoredPosition = Vector2.Lerp(transitionMainMenuShown, transitionMainMenuHidden, currentTime / totalTime);
            currentTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        currentTime = 0f;
        while (currentTime < totalTime)
        {
            rectHidden.anchoredPosition = Vector2.Lerp(transitionMainMenuHidden, transitionMainMenuShown, currentTime / totalTime);
            currentTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        rectShown.anchoredPosition = transitionMainMenuHidden;
        rectHidden.anchoredPosition = transitionMainMenuShown;
        isSubmenuShown = !isSubmenuShown;
    }
    #endregion

    #region GAME OVER
    public void OpenGameOverMenu()
    {
        OpenMenu(MenuType.GameOver);
    }

    public void OnClickRetry()
    {
        OpenMenu(MenuType.HUD);
        GameManager.Instance.ReloadLevel();
    }
    #endregion

    #region HUD AND PAUSE MENU

    public void AddGameInfoLeft(string info)
    {
        txtGameInfoLeft.text = info;
        txtGameInfoLeft.gameObject.SetActive(true);
    } 
    
    public void AddGameInfoRight(string info)
    {
        txtGameInfoRight.text = info;
        txtGameInfoRight.gameObject.SetActive(true);
    }

    public void DeleteGameInfo()
    {
        txtGameInfoLeft.text = string.Empty;
        txtGameInfoLeft.gameObject.SetActive(false);

        txtGameInfoRight.text = string.Empty;
        txtGameInfoRight.gameObject.SetActive(false);
    }

    public void TogglePauseMenu(bool open)
    {
        if (open)
        {
            OpenMenu(MenuType.Pause);
        }
        else
        {
            OpenMenu(MenuType.HUD);

        }
    }
    #endregion

    public void OnClickExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
        return;
    }
    private IEnumerator LevelTransitionAnim(int levelNum)
    {
        goLevelTransition.SetActive(true);
        goLevelTransition.transform.localScale = Vector3.zero;
        txtLevel.text = ((Level)levelNum).ToString();

        float totalTime = 0.25f;
        float currentTime = 0;
        while (currentTime < totalTime)
        {
            goLevelTransition.transform.localScale = Vector2.Lerp(Vector2.zero, Vector2.one, currentTime/totalTime);
            currentTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        goLevelTransition.transform.localScale = Vector3.one;

        yield return new WaitForSeconds(1.5f);
        GameManager.Instance.LoadLevel((Level)levelNum);
        OpenMenu(MenuType.HUD);

        currentTime = 0;
        while (currentTime < totalTime)
        {
            goLevelTransition.transform.localScale = Vector2.Lerp(Vector2.one, Vector2.zero, currentTime / totalTime);
            currentTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        goLevelTransition.SetActive(false);
        goLevelTransition.transform.localScale = Vector3.zero;
    }

    private void Transition(Action onFinish)
    {

    }

    private enum MenuType
    {
        None,
        Main,
        GameOver,
        HUD,
        Pause
    }
}
