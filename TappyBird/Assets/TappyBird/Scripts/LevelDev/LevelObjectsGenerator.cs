using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// <para> This class instanciates all of the gameObjects of the scene that creates the player movement ilusion. </para>
/// <para> Instantiates the clouds and the obstacles of the level. </para>
/// </summary>
public class LevelObjectsGenerator : MonoBehaviour
{
    public delegate void SpeedDelegate();
    public static        SpeedDelegate OnSpeedChanging;
    public static float obstacleSpawnIntervalTime, objectLifeSpawnDistance;
    private readonly Vector3 obstacleSpawnStartPosition = new Vector2(5f, 0f);
    private GameObject[] backgroundObjects, obstaclesEasyMode, obstaclesNormalMode, obstaclesHardMode;
    [SerializeField] private GameObject ObjectDestroyer;


    void Awake()
    {
        // Set values
        obstacleSpawnIntervalTime = objectLifeSpawnDistance = 0;

        // Get gameobject
        backgroundObjects   = Resources.LoadAll<GameObject>("Prefabs/BackgroundObjects");
        obstaclesEasyMode   = Resources.LoadAll<GameObject>("Prefabs/Obstacles/EasyDifficultyObstacles");
        obstaclesNormalMode = Resources.LoadAll<GameObject>("Prefabs/Obstacles/NormalDifficultyObstacles");
        obstaclesHardMode   = Resources.LoadAll<GameObject>("Prefabs/Obstacles/HardDifficultyObstacles");

        // Get values
        objectLifeSpawnDistance = Mathf.Abs(obstacleSpawnStartPosition.x) + Mathf.Abs(ObjectDestroyer.transform.position.x);
    }
    void OnEnable()
    {
        GameManager.OnLevelStarted += StartSpawning;
        GameManager.OnLoseGame     += StopSpawning;
        OnSpeedChanging            += Obstacles.SetVelocity;
    }
    void OnDisable()
    {
        GameManager.OnLevelStarted -= StartSpawning;
        GameManager.OnLoseGame     -= StopSpawning;
        OnSpeedChanging            -= Obstacles.SetVelocity;
    }

    private void StartSpawning()
    {
        Obstacles.SetVelocity();
        obstacleSpawnIntervalTime = (obstacleSpawnIntervalTime < 0.5f)? 0.5f: obstacleSpawnIntervalTime;
        StartCoroutine(ObstaclesSpawning());
        StartCoroutine(BackgroundObjectsSpawning());
    }
    private void StopSpawning()
    {
        StopCoroutine(ObstaclesSpawning());
        StopCoroutine(BackgroundObjectsSpawning());
    }
    private IEnumerator ObstaclesSpawning()
    {
        // Spawn random obstacles of the current difficulty
        GameObject spawnObstacle = null;
        switch (GameManager.difficulty)
        {
            case GameManager.Difficulties.veryEasy:
            case GameManager.Difficulties.easy:
                spawnObstacle = obstaclesEasyMode[UnityEngine.Random.Range(0, obstaclesEasyMode.Length)];
                break;

            case GameManager.Difficulties.normal:
                spawnObstacle = obstaclesNormalMode[UnityEngine.Random.Range(0, obstaclesNormalMode.Length)];
                break;

            case GameManager.Difficulties.hard:
            case GameManager.Difficulties.veryHard:
                spawnObstacle = obstaclesHardMode[UnityEngine.Random.Range(0, obstaclesHardMode.Length)];
                break;
        }
        Instantiate(spawnObstacle, obstacleSpawnStartPosition, Quaternion.identity);

        // delay
        var timer = 0f;
        while (timer < obstacleSpawnIntervalTime)
        {
            yield return null;
            timer += Time.deltaTime;
        }

        // restart spawning
        StartCoroutine(ObstaclesSpawning());
    }
    private IEnumerator BackgroundObjectsSpawning()
    {
        // Delay
        var timer = 0f;
        var delay = UnityEngine.Random.Range(5f, 5.5f);
        while (timer < delay)
        {
            yield return null;
            timer += Time.deltaTime;
        }

        // Spawn random obstacles of the current difficulty
        GameObject spawnCloud   = backgroundObjects[UnityEngine.Random.Range(0, backgroundObjects.Length)];
        var cloudRandomVerticalPos = UnityEngine.Random.Range(2, 4);
        Instantiate(spawnCloud, new Vector2(obstacleSpawnStartPosition.x, cloudRandomVerticalPos), Quaternion.identity);

        // Restart spawning
        StartCoroutine(BackgroundObjectsSpawning());
    }

}