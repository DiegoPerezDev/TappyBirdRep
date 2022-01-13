using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour
{
    [SerializeField] private bool notSpawend;
    private bool destroyed;
    private Rigidbody2D rigidBody;


    void Awake() => rigidBody = GetComponent<Rigidbody2D>();
    void OnEnable()
    {
        GameManager.OnLoseGame += StopMovement;
        // Add movement to the objects that were present at the beggining of the level, those that were not spawned.
        if (notSpawend)
            GameManager.OnLevelStarted += AddMovement;
    }
    void OnDisable()
    {
        GameManager.OnLoseGame -= StopMovement;
        if (notSpawend)
            GameManager.OnLevelStarted -= AddMovement;
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

    private void AddMovement()
    {
        if(rigidBody != null)
            rigidBody.velocity = new Vector2(UnityEngine.Random.Range(-0.6f, -0.8f), 0f);
        else
            print("tried to add movement but there is no rigidbody attached");
    }
    private void StopMovement() => rigidBody.velocity = Vector2.zero;

}