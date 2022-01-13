using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // General data
    public static readonly string playerPath = "Player";
    private Animator animator;
    private Rigidbody2D rigidBody;

    // Jumping data
    [Header("Movement:", order = 0)]
    [SerializeField] private float jumpForce = 200f;
    [SerializeField] private float minimumVelocityBeforeFalling = 13f;
    private bool jumping;
    private IEnumerator checkFallCoroutine;

    // Collisions data
    [Header("Life/Collisions:", order = 1)]
    [SerializeField] private bool invulnerability;


    void Awake()
    {
        // Get components
        rigidBody = GetComponent<Rigidbody2D>();
        animator  = gameObject.GetComponent<Animator>();

        // Set data
        rigidBody.simulated = false;
#if UNITY_ANDROID
        // Move the player to the center of the screen if the phone screen is larger than usual.
        if ( (1 / Camera.main.aspect) * 9 > 16 ) 
                transform.position += new Vector3(0.4f,0,0);
        #endif

    }
    void OnEnable()
    {
        GameManager.OnLevelStarted += LevelStart;
        GameManager.OnLoseGame     += GameLost;
    }
    void OnDisable() 
    {
        GameManager.OnLevelStarted -= LevelStart;
        GameManager.OnLoseGame     -= GameLost;
    }
    private void Update() => transform.eulerAngles = new Vector3(0, 0, rigidBody.velocity.y * 3f); // Rotate bird as it jumps and falls
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            if (invulnerability)
                return;
            invulnerability = true;
            animator.Play("Player_hurt");
            GameManager.OnLoseGame?.Invoke();
            Destroy(this);
        }
        else if (collision.CompareTag("Point"))
        {
            Destroy(collision);
            Score.GatherPoint();
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
        {
            if (invulnerability)
                return;
            invulnerability = true;
            animator.Play("Player_hurt");
            GameManager.OnLoseGame?.Invoke();
            Destroy(this);
        }
    }


    private void LevelStart()
    {
        rigidBody.simulated = true;
        animator.Play("Player_fly");
    }
    private void GameLost()
    {
        rigidBody.velocity  = Vector2.zero;
        rigidBody.simulated = false;
    }
    public void Jump()
    {
        if (jumping)
            return;
        jumping = true;

        if (checkFallCoroutine != null)
            StopCoroutine(checkFallCoroutine);
        checkFallCoroutine = WaitForNextJumpl();
        StartCoroutine(checkFallCoroutine);
    }
    private IEnumerator WaitForNextJumpl()
    {
        // Stop the vertical movement and add the jumping force after its comepletely still
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0f);
        while (rigidBody.velocity.y > 0)
            yield return null;
        rigidBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);

        // Wait for the player to start falling for ending the jumping
        while (rigidBody.velocity.y > minimumVelocityBeforeFalling)
        {
            float timer = 0f, performanceDelay = 0.1f;
            while (timer < performanceDelay)
            {
                yield return null;
                timer += Time.deltaTime;
            }
        }
        jumping = false;
    }

}