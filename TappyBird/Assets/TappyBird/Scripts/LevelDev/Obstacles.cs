using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    [SerializeField] private bool notSpawend;
    private bool destroyed;
    private Rigidbody2D rigidBody;
    public static float obstacleVelocity;
    private static readonly float startVelocity = -1.8f, velocityIncrement = -0.3f;


    void Awake() => rigidBody = GetComponent<Rigidbody2D>();
    void OnEnable()
    {
        GameManager.OnLoseGame += StopMovement;
        // Add movement to the objects that were present at the beggining of the level, those that were not spawned.
        if (notSpawend)
            GameManager.OnLevelStarted += AddMovement;
        LevelObjectsGenerator.OnSpeedChanging += AddMovement;
    }
    void OnDisable()
    {
        GameManager.OnLoseGame -= StopMovement;
        if (notSpawend)
            GameManager.OnLevelStarted -= AddMovement;
        LevelObjectsGenerator.OnSpeedChanging -= AddMovement;
    }
    void Start()
    {
        // Add movement to the spawned objects
        if (!notSpawend)
            AddMovement();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (destroyed)
            return;

        if (collision.CompareTag("ObstacleDestroyer"))
        {
            destroyed = true;
            Destroy(this.gameObject);
        }
    }

    public static void SetVelocity()
    {
        switch (GameManager.difficulty)
        {
            case GameManager.Difficulties.veryEasy:
                obstacleVelocity = startVelocity;
                break;
            case GameManager.Difficulties.easy:
            case GameManager.Difficulties.veryHard:
                obstacleVelocity += velocityIncrement;
                break;
        }
        // Get time  of spawn with the velocity formula with a small variation of the division.
        LevelObjectsGenerator.obstacleSpawnIntervalTime = (LevelObjectsGenerator.objectLifeSpawnDistance / Mathf.Abs(obstacleVelocity)) / 2.6f;
    }
    private void AddMovement()  => rigidBody.velocity = new Vector2(obstacleVelocity, 0f);
    private void StopMovement() => rigidBody.velocity = Vector2.zero;

}