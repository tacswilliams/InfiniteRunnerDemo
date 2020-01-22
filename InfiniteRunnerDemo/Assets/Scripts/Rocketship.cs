using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Rocketship : MonoBehaviour
{
    public GameManager gameManager;
    
    public bool isAlive = true;
    public bool isFlinching = false;

    // The rocket can only be in 1 of 3 positions
    public Vector3[] rocketPositions;
    // This stores the current index of the rocket's position
    public int currentPosIndex = 1;

    public float moveSpeed = 5;

    public TextMeshProUGUI hpText;
    public int hp = 3;

    public TextMeshProUGUI scoreText;
    public int score = 0;

    public TextMeshProUGUI bestScoreText;

    // How long the rocket will "flinch" for
    public float flinchDuration;
    // The timer for our flinching
    public float t;

    public SpriteRenderer sprite;
    private Color flinchColor = new Color(.5f, 0, 0, 0);

    public AudioSource audioSource;
    public AudioClip explosion;
    public AudioClip points;
    public AudioClip lose;

    // Start is called before the first frame update
    void Start()
    {
        currentPosIndex = 1;
        moveSpeed = 5;
        hp = 3;
        hpText.text = "HP: " + hp;
        score = 0;
        scoreText.text = "Score: " + score;
        flinchDuration = 5f;

        bestScoreText.text = "Best: " + BestScoreTracker.INSANCE.bestScore;

        // Finds the component called Sprite Renderer on the current gameobject
        sprite = GetComponent<SpriteRenderer>();

        audioSource = GetComponent<AudioSource>();
    }

    // OnTriggerEnter2D: Runs when the rocket ships triggerzone is entered
    // Parameter: Collider2D collision - a collider attached to another gameObject 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Asteroid" && isAlive && !isFlinching)
        {
            // I have hit an asteroid and I am alive and I am currently not flinching
            isFlinching = true;
            hp -= 1;
            if(hp <= 0)
            {
                hp = 0;
                isAlive = false;
            }
            hpText.text = "HP: " + hp;

            audioSource.PlayOneShot(explosion);
        }
        else if (collision.gameObject.name == "Points" && !isFlinching && isAlive) 
        {
            // I have collected a point and I am currently not flinching and I am alive
            score += 1;
            if(score > BestScoreTracker.INSANCE.bestScore)
            {
                // Current Score is better than the best score, so change the best score
                BestScoreTracker.INSANCE.SetBestScore(score);
                bestScoreText.text = "Best: " + BestScoreTracker.INSANCE.bestScore;
            }

            scoreText.text = "Score: " + score;
            audioSource.PlayOneShot(points);

        }

        if (!isAlive && !gameManager.isGameOver)
        {
            // I am no longer alive and the game is currently not over yet
            KillPlayer();
            gameManager.GameOver();

            audioSource.PlayOneShot(lose);

        }
    }

    // Kill Player: Disables the gameobject to prevent any unwanted interaction
    void KillPlayer()
    {
        sprite.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFlinching && t < flinchDuration)
        {
            // I am hit and my flinsh timer has not finished

            // Add the time between the frames to my timer
            t += Time.deltaTime;

            // Chnage the color of my sprite back and forth between
            // my current color and my flinch color
            sprite.color = Color.Lerp(Color.white, flinchColor, Mathf.PingPong(t, 0.5f));
        }
        else if (t >= flinchDuration)
        {
            // My flinch timer is past the duration

            // reset my flinch timer
            t = 0;

            // I am no longer hit
            isFlinching = false;
        }
        if (isAlive)
        {
            // Smoothly moves the rocket ship to the correct position
            transform.position = Vector3.Lerp(transform.position, rocketPositions[currentPosIndex], Time.deltaTime * moveSpeed);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // Move the ship up to the next position index
            currentPosIndex += 1;
            if(currentPosIndex > rocketPositions.Length - 1)
            {
                // Prevents out of bounds errors
                currentPosIndex = rocketPositions.Length - 1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            // Moves ship down to the next position index
            currentPosIndex -= 1;
            if(currentPosIndex < 0)
            {
                // Prevents out of bounds errors
                currentPosIndex = 0;
            }
        }
    }
}
