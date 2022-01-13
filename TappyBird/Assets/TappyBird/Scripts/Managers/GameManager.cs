using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary> This specific 'GameManager' script works for one scene games.
/// <para> - Scene transitions: Scene loading and setting. </para> 
/// <para> - Level behaviour: Pause, win and lose behaviour. </para>
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // Transition data
    [SerializeField] private bool printTransitionStates;
    private static bool firstGameLoadSinceExecution = true;
    public delegate void LoadingDelegate();
    public static        LoadingDelegate OnStartingScene;
    public enum   LoadingStates { none, loadingScene, settingScene }
    public static LoadingStates loadingState;

    // Level management
    public static readonly float LoseToResetDelay = 0.7f;
    public static bool playing, paused;
    public delegate void LevelDelegate();
    public static        LevelDelegate OnLevelStartingSet, OnLevelStarted, OnLoseGame, OnPausingMenu, OnUnpausingMenu;
    [HideInInspector] public enum Difficulties { veryEasy, easy, normal, hard, veryHard }
    public static                 Difficulties difficulty = Difficulties.veryEasy;

    // Saving data
    private static readonly string scoreRecordValKey = "scoreRecordValKey";
   
       
    void Awake()
    {
        // Set singleton
        if (instance != null)
            Destroy(transform.parent.gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(transform.parent.gameObject);
        }
    }

    void Start()
    {
        // General settings
        #if UNITY_STANDALONE
                Screen.SetResolution(432, 768, FullScreenMode.MaximizedWindow);
        #endif
        Application.runInBackground   = false;
        Debug.developerConsoleVisible = true;

        // Set delegate events
        OnLevelStartingSet += SetLevel;
        OnLoseGame += LoseGame;

        // Load saved data
        LoadLevelData(); // Use 'ResetSavingData()' in this line instead for restarting the saving data;

        // Start game
        RestartGame();
    }

    // Behaviour of the game when minimizing. In this case we open the pause menu.
    void OnApplicationPause(bool pauseStatus)
    {
        if (!pauseStatus || !playing)
            return;
        if (!paused)
            Pause(true, true);
    }


    #region Game Restarting

    public static void RestartGame()
    {
        if (loadingState == LoadingStates.none)
        {
            loadingState = LoadingStates.loadingScene;
            instance.StartCoroutine(RestartToMainmenu());
        }
    }

    /// <summary> Load scene while waiting for some codes to set up before starting the scene. </summary>
    private static IEnumerator RestartToMainmenu()
    {
        // Dont re-load the scene if we are just opening the game
        if(firstGameLoadSinceExecution)
        {
            InputsManager.SetInputManager();
            firstGameLoadSinceExecution = false;
            goto SettingScene;
        }

        // Load desired scene
        if (instance.printTransitionStates)
            print("Loading... Entering desired scene. (1/3)");
        AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(0);
        while (!loadingOperation.isDone)
            yield return null;

        // Set the scene
        SettingScene:
        if (instance.printTransitionStates)
            print("Loading... Setting scene. (2/3)");
        loadingState = LoadingStates.settingScene;
        yield return new WaitForSecondsRealtime(0.1f); // give time to the other codes to perform the starting functions

        // Start the scene
        OnStartingScene?.Invoke();
        loadingState = LoadingStates.none;
        if (instance.printTransitionStates)
            print("Loading completed. Scene started! (3/3)");
    }

    #endregion

    #region Level set

    private static void SetLevel()
    {
        playing    = true;
        difficulty = Difficulties.veryEasy;
        OnLevelStarted?.Invoke();
        Pause(false, false);
    }

    #endregion

    #region Level events: pause and lose

    public static void Pause(bool withPanelChanging, bool pausing)
    {
        paused  = pausing;
        playing = !pausing;

        if (pausing)
        {
            InputsManager.input.PlayingInputs.Disable();
            Time.timeScale = 0;
        }
        else
        {
            InputsManager.input.PlayingInputs.Enable();
            Time.timeScale = 1;
        }
        
        if (withPanelChanging)
        {
            if (pausing)
                OnPausingMenu?.Invoke();
            else
                OnUnpausingMenu?.Invoke();
        }
    }

    public static void Pause(bool withPanelChanging) => Pause(withPanelChanging, !paused);

    private static void LoseGame()
    {
        playing = false;
        Pause(false, true);
        instance.StopAllCoroutines();
        instance.StartCoroutine(LoseDelay());
    }

    // There is sometimes a blinking animation of the new record so we wait that time before reseting the game.
    private static IEnumerator LoseDelay()
    {
        if(Score.newRecord)
            yield return new WaitForSecondsRealtime(TemporalMessages.newRecordAnimationDuration * 1.1f);
        else
            yield return new WaitForSecondsRealtime(LoseToResetDelay);
        RestartGame();
    }

    #endregion

    #region Saving data

    public static void SaveLevelData() => PlayerPrefs.SetInt(scoreRecordValKey, Score.record);
    public static void LoadLevelData() => Score.record = PlayerPrefs.GetInt(scoreRecordValKey);
    public static void ResetSavingData()
    {
        PlayerPrefs.SetInt(scoreRecordValKey, 0);
        SaveLevelData();
    }

    #endregion

}