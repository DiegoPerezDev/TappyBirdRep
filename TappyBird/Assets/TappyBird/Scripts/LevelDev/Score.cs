using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Score : MonoBehaviour
{
    public const int difficultyChangingIntervalPointsValue = 5;
    public static int score, record, lastScore;
    public static bool firstNewRecordInThisPlaytime, newRecord;
    public static readonly float newRecordAnimationDuration = 2.25f;
    public static AudioClip tenPointsAudioClip, newScoreAudioClip;
    [SerializeField] private TextMeshProUGUI hudScoreText, pauseScoreText, pauseRecordText, mainMenuLastScoreText, mainMenuRecordText;
    private static TextMeshProUGUI hudScoreTMP, pauseScoreTMP, pauseRecordTMP;
    private const int maximumPointForDifficultyChanging = difficultyChangingIntervalPointsValue * 5;


    void Awake()
    {
        // Set data
        score = 0;
        firstNewRecordInThisPlaytime = newRecord = false;

        // Get components
        tenPointsAudioClip = Resources.Load<AudioClip>("Audio/LevelDev_SFX/10th cross");
        newScoreAudioClip  = Resources.Load<AudioClip>("Audio/LevelDev_SFX/new record");
        hudScoreTMP        = hudScoreText;
        pauseScoreTMP      = pauseScoreText;
        pauseRecordTMP     = pauseRecordText;

        // Set main menu score data
        mainMenuLastScoreText.text = $"Last Score: {lastScore}";
        mainMenuRecordText.text    = $"Record: {record}";
        lastScore = 0;
    }
    void OnEnable()  => GameManager.OnPausingMenu += OnPausing;
    void OnDisable() => GameManager.OnPausingMenu -= OnPausing;
    

    private void OnPausing()
    {
        pauseScoreTMP.text  = $"Score: {score}";
        pauseRecordTMP.text = $"Record: {record}";
    }
    public static void GatherPoint()
    {
        score++;
        lastScore++;
        hudScoreTMP.text = $"Score: {score}";

        // Avoid posible bugs in extreme case of never losing
        if (score == 99)
            GameManager.OnLoseGame?.Invoke();

        // Difficulty changing
        if ( (score % difficultyChangingIntervalPointsValue == 0) && (score < maximumPointForDifficultyChanging + 1) )
        {
            switch (score)
            {
                case difficultyChangingIntervalPointsValue:
                    TemporalMessages.levelDifficultyMessage = "Difficulty: Easy";
                    GameManager.difficulty = GameManager.Difficulties.easy;
                    LevelObjectsGenerator.OnSpeedChanging?.Invoke();
                    break;

                case difficultyChangingIntervalPointsValue * 2:
                    TemporalMessages.levelDifficultyMessage = "Difficulty: Normal";
                    GameManager.difficulty = GameManager.Difficulties.normal;
                    break;

                case difficultyChangingIntervalPointsValue * 3:
                    TemporalMessages.levelDifficultyMessage = "Difficulty: Hard";
                    GameManager.difficulty = GameManager.Difficulties.hard;
                    break;

                case difficultyChangingIntervalPointsValue * 4:
                    goto HighScore;

                case maximumPointForDifficultyChanging:
                    TemporalMessages.levelDifficultyMessage = "Difficulty: Very Hard";
                    GameManager.difficulty = GameManager.Difficulties.veryHard;
                    LevelObjectsGenerator.OnSpeedChanging?.Invoke();
                    break;
            }
            AudioManager.PlayAudio(ref AudioManager.GameAudioSource, tenPointsAudioClip, AudioManager.GameAudioGO);
            TemporalMessages.ShowNewLevelDifficulty();
        }
         // New high score
         HighScore:
        if(score > record)
        {
            newRecord = true;
            record = score;
            GameManager.SaveLevelData();
            if(!firstNewRecordInThisPlaytime)
            {
                firstNewRecordInThisPlaytime = true;
                AudioManager.PlayAudio(ref AudioManager.GameAudioSource, newScoreAudioClip, AudioManager.GameAudioGO);
                TemporalMessages.ShowNewRecordMessage();
            }
            

        }
    }

}